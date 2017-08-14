using Platform.Presentation.Reports.Windows.Forms;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private RazorReportViewer Report;

        private void FormMain_Shown(object sender, System.EventArgs e)
        {

            foreach (var item in System.IO.Directory.GetFiles("templates", "*.cshtml"))
            {
                var fileinfo = new System.IO.FileInfo(item);
                var child = new ToolStripMenuItem(fileinfo.Name);
                child.Click += Child_Click;
                templatesToolStripMenuItem.DropDownItems.Add(child);
            }

            Report = new RazorReportViewer()
            {
                Dock = DockStyle.Fill,
            };
            this.Controls.Add(Report);

            Report.BringToFront();
            Report.Refresh();
            Report.Update();
        }

        private void Child_Click(object sender, System.EventArgs e)
        {
            Report.Template = $"templates\\{(sender as ToolStripMenuItem).Text}";
        }
    }
}