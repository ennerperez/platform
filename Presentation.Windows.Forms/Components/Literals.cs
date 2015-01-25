using System.Collections.Generic;
using System.ComponentModel;

namespace Presentation.Windows.Forms.Components
{
    [System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.Label))]
    public class Literals : System.ComponentModel.Component
    {

        private List<string> _Items = new List<string>();
        public string this[int index]
        {
            get { return this._Items[index]; }
        }

        [ListBindable(true)]
        [Localizable(true)]
        [Browsable(true)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public List<string> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public Literals()
            : base()
        {
        }

        public Literals(IContainer parentContainer)
            : this()
        {
            if (parentContainer != null)
            {
                parentContainer.Add(this);
            }
        }

    }
}
