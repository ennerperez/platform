using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Examples
{
    public partial class FormDemo : Form //Presentation.Windows.Forms.Patterns.MVP.Form< Entities.Software>
    {
        public FormDemo()
        {
            InitializeComponent();
        }

        public class ProfessionalColorTable2 : ProfessionalColorTable
        {
            public override Color MenuItemBorder { get { return Color.Red; } }


        }


    }


   

}
