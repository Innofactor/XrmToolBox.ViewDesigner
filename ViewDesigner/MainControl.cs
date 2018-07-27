namespace Cinteros.XTB.ViewDesigner
{
    using System;
    using System.Windows.Forms;
    using Forms;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Sdk;
    using XrmToolBox.Extensibility;
    using XrmToolBox.Extensibility.Interfaces;

    public partial class MainControl : PluginControlBase, IGitHubPlugin, IMessageBusHost, IHelpPlugin
    {
        #region Private Fields

        private const string aiEndpoint = "https://dc.services.visualstudio.com/v2/track";

        //private const string aiKey = "cc7cb081-b489-421d-bb61-2ee53495c336";    // jonas@rappen.net tenant, TestAI
        private const string aiKey = "eed73022-2444-45fd-928b-5eebd8fa46a6";

        // jonas@rappen.net tenant, XrmToolBox
        private AppInsights ai = new AppInsights(new AiConfig(aiEndpoint, aiKey) { PluginName = "View Designer" });

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

        public string HelpUrl
        {
            get { return "http://cinteros.xrmtoolbox.com/?src=VDhelp"; }
        }

        string IGitHubPlugin.RepositoryName
        {
            get
            {
                return "XrmToolBox.ViewDesigner";
            }
        }

        string IGitHubPlugin.UserName
        {
            get
            {
                return "Innofactor";
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            if (message.SourcePlugin == "FetchXML Builder" &&
                message.TargetArgument is string)
            {
                ai.WriteEvent("Query edited in FXB");
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

            var view = ViewEditor?.ViewChanges;
            if (view == null || string.IsNullOrEmpty(view.LogicalName))
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var messageBusEventArgs = new MessageBusEventArgs("FetchXML Builder")
                {
                    TargetArgument = ViewEditor.FetchXml.OuterXml
                };
                ai.WriteEvent("Edit Query in FXB");
                OnOutgoingMessage(this, messageBusEventArgs);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("FetchXML Builder is not installed.\nDownload it from the plugins store.", "FetchXML Builder",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (PluginNotFoundException)
            {
                var xtbver = ParentForm.ProductVersion;
                if (xtbver == "1.2015.7.6")
                {
                    MessageBox.Show("XrmToolBox version " + xtbver + " has a minor problem integrating plugins.\nHang in there - new version will be released soon!", "Launching FetchXML Builder",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("FetchXML Builder is not installed.\nDownload it from the plugins store.", "FetchXML Builder",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to CRM.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var select = new SelectViewDialog(this)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            if (select.ShowDialog() == DialogResult.OK)
            {
                tsbSnap.Checked = true;

                ViewEditor.Enabled = true;
                ViewEditor.Set(select.View);
                tsbSnap.Checked = ViewEditor.Snapped;
                ai.WriteEvent("Open View");
            }
        }

        private void tsbPublish_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to CRM.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var entity = ViewEditor?.ViewEntityName;
            if (string.IsNullOrEmpty(entity))
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WorkAsync(new WorkAsyncInfo("Publishing changes",
                (a) =>
                {
                    var pubRequest = new PublishXmlRequest
                    {
                        ParameterXml = string.Format("<importexportxml><entities><entity>{0}</entity></entities><nodes/><securityroles/><settings/><workflows/></importexportxml>", entity)
                    };
                    Service.Execute(pubRequest);
                })
            {
                PostWorkCallBack = (a) =>
                {
                    if (a.Error != null)
                    {
                        MessageBox.Show(a.Error.Message, ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ai.WriteEvent("Publish");
                    }
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

            var view = ViewEditor?.ViewChanges;
            if (view == null || string.IsNullOrEmpty(view.LogicalName))
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WorkAsync(new WorkAsyncInfo("Saving changes",
                a =>
                {
                    Service.Update(view);
                })
            {
                PostWorkCallBack = a =>
                {
                    if (a.Error != null)
                    {
                        MessageBox.Show(a.Error.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ai.WriteEvent("Save View");
                    }
                }
            });
        }

        private void tsbSnap_Click(object sender, EventArgs e)
        {
            var snap = ((ToolStripButton)sender).Checked;
            ViewEditor.Snap(snap);
            ai.WriteEvent($"Snap: {snap}");
        }

        private void UpdateFetch(string fetchxml)
        {
            ViewEditor.FetchXml.LoadXml(fetchxml);
            ViewEditor.IsFetchXmlChanged = true;
        }

        #endregion Private Methods

        private void MainControl_Load(object sender, EventArgs e)
        {
            ai.WriteEvent("Load");
        }

        private void MainControl_OnCloseTool(object sender, EventArgs e)
        {
            ai.WriteEvent("Close");
        }

        private void tsbEditColumns_Click(object sender, EventArgs e)
        {
            if (ViewEditor == null || ViewEditor.FetchXml == null)
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var select = new SelectColumnsDialog(ViewEditor.FetchXml, ViewEditor.LayoutXml)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            if (select.ShowDialog() == DialogResult.OK)
            {
                var entity = new Entity();
                entity.Attributes.Add("layoutxml", select.LayoutXml.OuterXml);

                //tsbSnap.Checked = true;

                ViewEditor.Set(entity);
                ViewEditor.IsLayoutXmlChanged = true;
                ai.WriteEvent("Edit Columns");
            }
        }

        private void tsbEditXml_Click(object sender, EventArgs e)
        {
            if (ViewEditor == null || ViewEditor.LayoutXml == null)
            {
                MessageBox.Show("First select a view to design.", ((ToolStripButton)sender).Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var xcdDialog = new XmlContentDisplayDialog(ViewEditor.LayoutXml.OuterXml, "LayoutXml", true, true)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            if (xcdDialog.ShowDialog() == DialogResult.OK)
            {
                var entity = new Entity();
                entity.Attributes.Add("layoutxml", xcdDialog.result.OuterXml);

                ViewEditor.Set(entity);
                ViewEditor.IsLayoutXmlChanged = true;
                ai.WriteEvent("Edit LayoutXML");
            }
        }

        private void tsbLivePreview_Click(object sender, EventArgs e)
        {
            var preview = ((ToolStripButton)sender).Checked;
            ViewEditor.Preview(preview);
            ai.WriteEvent($"Preview: {preview}");
        }
    }
}