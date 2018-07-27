namespace Cinteros.XTB.ViewDesigner.AppCode
{
    using Cinteros.Xrm.XmlEditorUtils;
    using Microsoft.Xrm.Sdk;

    public class ViewItem : IComboBoxItem
    {
        #region Private Fields

        private Entity view = null;

        #endregion Private Fields

        #region Public Constructors

        public ViewItem(Entity View)
        {
            view = View;
        }

        #endregion Public Constructors

        #region Public Methods

        public string GetFetch()
        {
            if (view.Contains("fetchxml"))
            {
                return view["fetchxml"].ToString();
            }
            return "";
        }

        public string GetLayout()
        {
            if (view.Contains("layoutxml"))
            {
                return view["layoutxml"].ToString();
            }
            return "";
        }

        public string GetValue()
        {
            return view.Id.ToString();
        }

        public Entity GetView()
        {
            return view;
        }

        public override string ToString()
        {
            return view["name"].ToString();
        }

        #endregion Public Methods
    }
}