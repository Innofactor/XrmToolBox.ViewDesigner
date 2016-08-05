namespace Cinteros.XTB.ViewDesigner.Controls
{
    using Forms;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;
    using XrmToolBox.Extensibility.Interfaces;

    public partial class ViewEditor : UserControl
    {
        #region Private Fields

        private static List<int> snapWidths = new List<int>(new int[] { 25, 50, 75, 100, 125, 150, 200, 300 });
        private bool isTitleChanged;

        #endregion Private Fields

        #region Public Constructors

        public ViewEditor()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Properties

        public XmlDocument FetchXml
        {
            get;
            private set;
        }

        public Guid Id
        {
            get;
            private set;
        }

        public bool IsFetchXmlChanged
        {
            get;
            set;
        }

        public bool IsLayoutXmlChanged
        {
            get;
            set;
        }

        public XmlDocument LayoutXml
        {
            get;
            private set;
        }

        public bool Live
        {
            get;
            private set;
        }

        public string LogicalName
        {
            get;
            set;
        }

        public bool Snapped
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            set;
        }

        public string ViewEntityName { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Preview(bool allow)
        {
            if (allow)
            {
                Live = true;
            }
            else
            {
                Live = false;
            }
        }

        /// <summary>
        /// Updates view designer with most recent definition of the view
        /// </summary>
        /// <param name="view"></param>
        public void Set(Entity view)
        {
            lvDesign.ColumnWidthChanged -= lvDesign_ColumnWidthChanged;
            lvDesign.ColumnReordered -= lvDesign_ColumnReordered;

            UpdateTitle(view);
            UpdateId(view);
            UpdateLogicalName(view);
            UpdateFetchXml(view);
            UpdateLayoutXml(view);
            UpdateLive(view);

            lvDesign.ColumnReordered += lvDesign_ColumnReordered;
            lvDesign.ColumnWidthChanged += lvDesign_ColumnWidthChanged;
        }

        /// <summary>
        /// Snaps columns width for standard values used in CRM (25..300)
        /// </summary>
        /// <param name="allow"></param>
        public void Snap(bool allow)
        {
            if (allow)
            {
                Snapped = true;

                for (var i = 0; i < lvDesign.Columns.Count; i++)
                {
                    lvDesign.Columns[i].Width += 1;
                }
            }
            else
            {
                Snapped = false;
            }
        }

        #endregion Public Methods

        #region Internal Methods

        /// <summary>
        /// Combines all information about design to CRM entity representation. Only changed
        /// attributes will be added
        /// </summary>
        /// <returns></returns>
        internal Entity ToEntity()
        {
            var entity = new Entity(LogicalName);
            entity.Id = Id;

            if (isTitleChanged)
            {
                entity.Attributes["name"] = Title;
            }

            if (IsFetchXmlChanged)
            {
                entity.Attributes["fetchxml"] = FetchXml.OuterXml;
            }

            if (IsLayoutXmlChanged)
            {
                entity.Attributes["layoutxml"] = LayoutXml.OuterXml;
            }

            return entity;
        }

        #endregion Internal Methods

        #region Private Methods

        private void lvDesign_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var column = ((ListView)sender).Columns[e.Column];

            var setSizeDialog = new SetSizeDialog(column.Name, column.Width);
            setSizeDialog.StartPosition = FormStartPosition.CenterParent;
            setSizeDialog.OnSet += (o, size) =>
            {
                column.Width = size;
            };

            setSizeDialog.ShowDialog();
        }

        private void lvDesign_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            var layout = XDocument.Parse(LayoutXml.OuterXml);

            var cells = layout.Descendants().First().Descendants().First().Descendants();

            var source = cells.ElementAt(e.OldDisplayIndex);
            var target = cells.ElementAt(e.NewDisplayIndex);

            if (e.OldDisplayIndex > e.NewDisplayIndex)
            {
                target.AddBeforeSelf(source);
            }
            else
            {
                target.AddAfterSelf(source);
            }

            source.Remove();

            LayoutXml.LoadXml(layout.ToString());

            IsLayoutXmlChanged = true;
        }

        private void lvDesign_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            lvDesign.ColumnWidthChanged -= lvDesign_ColumnWidthChanged;

            var layout = ((ListView)sender);

            var column = layout.Columns[e.ColumnIndex];
            var definition = (XmlNode)column.Tag;

            if (Snapped)
            {
                column.Width = NormalizeWidth(column.Width);
            }

            var attribute = definition.Attributes["width"];
            var width = column.Width.ToString();

            if (!attribute.Value.Equals(width))
            {
                attribute.Value = width;

                var pattern = string.Format("//cell[@name=\"{0}\"]", definition.Attributes["name"].Value);
                var cell = LayoutXml.SelectNodes(pattern).Cast<XmlNode>().FirstOrDefault();
                cell = definition;

                IsLayoutXmlChanged = true;
            }

            lvDesign.ColumnWidthChanged += lvDesign_ColumnWidthChanged;
        }

        private int NormalizeWidth(int width)
        {
            if (width < 25)
            {
                width = 25;
            }
            else if (width > 25 && width < 150)
            {
                width = (int)Math.Round((decimal)(width / 25)) * 25;
            }
            else if (width > 150 && width < 200)
            {
                width = (int)Math.Round((decimal)((width - 150) / 50)) * 50 + 150;
            }
            else if (width > 200 && width < 300)
            {
                width = (int)Math.Round((decimal)((width - 200) / 100)) * 100 + 200;
            }
            else if (width > 300)
            {
                width = 300;
            }

            return width;
        }

        private void Set(DataCollection<Entity> entities)
        {
            lvDesign.Items.Clear();

            foreach (var entity in entities)
            {
                var row = new List<string>();

                foreach (var name in lvDesign.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToArray())
                {
                    var item = (entity.Attributes.ContainsKey(name)) ? entity.Attributes[name].ToString() : string.Empty;
                    row.Add(item);
                }

                Invoke(new Action(() =>
                {
                    lvDesign.Items.Add(new ListViewItem(row.ToArray()));
                }));
            }
        }

        private void UpdateFetchXml(Entity view)
        {
            if (view.Attributes.ContainsKey("fetchxml"))
            {
                FetchXml = new XmlDocument();
                FetchXml.LoadXml((string)view.Attributes["fetchxml"]);
                var entity = FetchXml.SelectSingleNode("fetch/entity");
                ViewEntityName = entity != null && entity.Attributes["name"] != null ? entity.Attributes["name"].Value : "";
            }
        }

        private void UpdateId(Entity view)
        {
            if (!view.Id.Equals(Guid.Empty))
            {
                Id = view.Id;
                tbId.Text = Id.ToString();
            }
        }

        private void UpdateLayoutXml(Entity view)
        {
            if (view.Attributes.ContainsKey("layoutxml"))
            {
                LayoutXml = new XmlDocument();
                LayoutXml.LoadXml((string)view.Attributes["layoutxml"]);

                Snapped = true;

                var columns = LayoutXml.SelectNodes("//cell");

                lvDesign.Columns.Clear();

                foreach (XmlNode definition in columns)
                {
                    var column = new ColumnHeader();
                    column.Name = definition.Attributes["name"].Value;
                    column.Text = definition.Attributes["name"].Value;
                    column.Width = int.Parse(definition.Attributes["width"].Value);
                    column.Tag = definition;
                    if (!snapWidths.Contains(column.Width))
                    {
                        Snapped = false;
                    }

                    lvDesign.Columns.Add(column);
                }
            }
        }

        private void UpdateLive(Entity view)
        {
            if (Live && Parent != null)
            {
                var service = ((IXrmToolBoxPluginControl)Parent).Service;

                if (service != null)
                {
                    new Task(() =>
                    {
                        var query = FetchXml;
                        var queryAttributes = query.FirstChild.Attributes;

                        XmlAttribute count;

                        // Search for 'count' attribute, if any
                        count = queryAttributes.Cast<XmlAttribute>().Where(x => x.Name == "count").FirstOrDefault();

                        if (count != null)
                        {
                            queryAttributes.Remove(count);
                        }

                        // Adding 'count' attribute to restrict output
                        count = FetchXml.CreateAttribute("count");
                        count.Value = "50";

                        // Appending new attribute to query
                        queryAttributes.Append(count);

                        var result = service.RetrieveMultiple(new FetchExpression(query.InnerXml));

                        if (result != null)
                        {
                            Set(result.Entities);
                        }
                    }).Start();
                }
            }
        }

        private void UpdateLogicalName(Entity view)
        {
            if (view.LogicalName != null && !view.LogicalName.Equals(string.Empty))
            {
                LogicalName = view.LogicalName;
            }
        }

        private void UpdateTitle(Entity view)
        {
            if (view.Attributes.ContainsKey("name"))
            {
                Title = (string)view.Attributes["name"];
                tbName.Text = Title;
            }
        }

        private void ViewDesigner_TextChanged(object sender, EventArgs e)
        {
            var title = (TextBox)sender;
            Title = title.Text;
            isTitleChanged = true;
        }

        #endregion Private Methods
    }
}