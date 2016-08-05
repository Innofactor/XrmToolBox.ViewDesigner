namespace Cinteros.XTB.ViewDesigner
{
    using Forms;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Reflection;
    using System.Windows.Forms;
    using XrmToolBox.Extensibility;
    using XrmToolBox.Extensibility.Interfaces;

    public partial class MainControl : PluginControlBase, IGitHubPlugin, IMessageBusHost, IHelpPlugin
    {
        #region Private Fields

        private Control control;

        #endregion Private Fields

        #region Public Constructors

        public MainControl()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<MessageBusEventArgs> OnOutgoingMessage;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets or sets control, that would be seen as current page
        /// </summary>
        public Control CurrentPage
        {
            get
            {
                return control;
            }
            set
            {
                value.Size = Size;
                value.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

                Controls.Remove(control);
                Controls.Add(value);

                control = value;
            }
        }

        string IGitHubPlugin.RepositoryName
        {
            get
            {
                return "XrmToolBox.Plugins";
            }
        }

        string IGitHubPlugin.UserName
        {
            get
            {
                return "Cinteros";
            }
        }

        public string HelpUrl
        {
            get { return "http://cinteros.xrmtoolbox.com/?src=VDhelp"; }
        }

        #endregion Public Properties

        #region Public Methods

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            if (message.SourcePlugin == "FetchXML Builder" &&
                message.TargetArgument is string)
            {
                UpdateFetch(message.TargetArgument);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbEditFetch_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to CRM.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var view = ViewEditor != null ? ViewEditor.ToEntity() : null;
            if (view == null || string.IsNullOrEmpty(view.LogicalName))
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var messageBusEventArgs = new MessageBusEventArgs("FetchXML Builder");
                messageBusEventArgs.TargetArgument = ViewEditor.FetchXml.OuterXml;
                OnOutgoingMessage(this, messageBusEventArgs);
            }
            catch (System.IO.FileNotFoundException)
            {
                if (MessageBox.Show("FetchXML Builder is not installed.\nDownload latest version from\n\nhttp://fxb.xrmtoolbox.com", "FetchXML Builder",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    DownloadFXB();
                }
            }
            catch (PluginNotFoundException)
            {
                var xtbver = ((System.Windows.Forms.Control)(((System.Windows.Forms.ContainerControl)(this)).ParentForm)).ProductVersion;
                if (xtbver == "1.2015.7.6")
                {
                    MessageBox.Show("XrmToolBox version " + xtbver + " has a minor problem integrating plugins.\nHang in there - new version will be released soon!", "Launching FetchXML Builder",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (MessageBox.Show("FetchXML Builder was not found.\nInstall it from the XrmToolBox Plugin Store or visit\n\nhttp://fxb.xrmtoolbox.com", "FetchXML Builder",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    DownloadFXB();
                }
            }
        }

        internal static void DownloadFXB()
        {
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            System.Diagnostics.Process.Start("http://fxb.xrmtoolbox.com/?src=VD." + currentVersion);
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to CRM.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var select = new SelectViewDialog(this);
            select.StartPosition = FormStartPosition.CenterParent;
            if (select.ShowDialog() == DialogResult.OK)
            {
                tsbSnap.Checked = true;

                ViewEditor.Enabled = true;
                ViewEditor.Set(select.View);
                tsbSnap.Checked = ViewEditor.Snapped;
            }
        }

        private void tsbPublish_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to CRM.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string entity = ViewEditor != null ? ViewEditor.ViewEntityName : null;
            if (string.IsNullOrEmpty(entity))
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WorkAsync("Publishing changes",
                a =>
                {
                    var pubRequest = new PublishXmlRequest();
                    pubRequest.ParameterXml = string.Format("<importexportxml><entities><entity>{0}</entity></entities><nodes/><securityroles/><settings/><workflows/></importexportxml>", entity);
                    Service.Execute(pubRequest);
                },
                a =>
                {
                    if (a.Error != null)
                    {
                        MessageBox.Show(a.Error.Message, ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to CRM.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var view = ViewEditor != null ? ViewEditor.ToEntity() : null;
            if (view == null || string.IsNullOrEmpty(view.LogicalName))
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WorkAsync("Saving changes",
                a =>
                {
                    Service.Update(view);
                },
                a =>
                {
                    if (a.Error != null)
                    {
                        MessageBox.Show(a.Error.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
        }

        private void tsbSnap_Click(object sender, EventArgs e)
        {
            ViewEditor.Snap(((ToolStripButton)sender).Checked);
        }

        private void UpdateFetch(string fetchxml)
        {
            ViewEditor.FetchXml.LoadXml(fetchxml);
            ViewEditor.IsFetchXmlChanged = true;
        }

        #endregion Private Methods

        private void tsbEditColumns_Click(object sender, EventArgs e)
        {
            if (ViewEditor == null || ViewEditor.FetchXml == null)
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var select = new SelectColumnsDialog(ViewEditor.FetchXml, ViewEditor.LayoutXml);
            select.StartPosition = FormStartPosition.CenterParent;
            if (select.ShowDialog() == DialogResult.OK)
            {
                var entity = new Entity();
                entity.Attributes.Add("layoutxml", select.LayoutXml.OuterXml);

                //tsbSnap.Checked = true;

                ViewEditor.Set(entity);
                ViewEditor.IsLayoutXmlChanged = true;
            }
        }

        private void tsbEditXml_Click(object sender, EventArgs e)
        {
            if (ViewEditor == null || ViewEditor.LayoutXml == null)
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var xcdDialog = new XmlContentDisplayDialog(ViewEditor.LayoutXml.OuterXml, "LayoutXml", true, true);
            xcdDialog.StartPosition = FormStartPosition.CenterParent;
            if (xcdDialog.ShowDialog() == DialogResult.OK)
            {
                var entity = new Entity();
                entity.Attributes.Add("layoutxml", xcdDialog.result.OuterXml);

                ViewEditor.Set(entity);
                ViewEditor.IsLayoutXmlChanged = true;
            }
        }

        private void tsbLivePreview_Click(object sender, EventArgs e)
        {
        }
    }
}