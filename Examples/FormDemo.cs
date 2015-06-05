using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Presentation.Windows.Forms;

namespace Examples
{
    public partial class FormDemo : Form, Platform.Model.MVC.IView<Entities.Software> //Presentation.Windows.Forms.Patterns.MVP.Form< Entities.Software>
    {
        public FormDemo()
        {
            InitializeComponent();

            this.pictureBox1.Image = Platform.Support.Drawing.FontAwesome.Helpers.GetIcon(Platform.Support.Drawing.FontAwesome.IconType.Archive, this.pictureBox1.Width, Color.Azure);
            this.toolStripButton1.SetIcon(Platform.Support.Drawing.FontAwesome.IconType.Apple);

        }


#if !PORTABLE

        public class ProfessionalColorTable2 : ProfessionalColorTable
        {
            public override Color MenuItemBorder { get { return Color.Red; } }


        }

#endif

        Platform.Model.MVC.Controller<Entities.Software> _controller;

        public void SetController(Platform.Model.MVC.Controller<Entities.Software> controller)
          {
              _controller = controller;
          }
  
          public void ClearGrid()
          {
              // Define columns in grid
              this.listView1.Columns.Clear();

              this.listView1.Columns.Add("Name", 150, HorizontalAlignment.Left);
              //this.listView1.Columns.Add("First Name", 150, HorizontalAlignment.Left);
              //this.listView1.Columns.Add("Lastst Name", 150, HorizontalAlignment.Left);
              //this.listView1.Columns.Add("Department", 150, HorizontalAlignment.Left);
              //this.listView1.Columns.Add("Sex", 50, HorizontalAlignment.Left);
  
              // Add rows to grid
              this.listView1.Items.Clear();
          }
  
         

        public void AddItemToGrid(Entities.Software usr)
         {
             ListViewItem parent;
             parent = this.listView1.Items.Add(usr.Name);
             //parent.SubItems.Add(usr.Name);
                         
         }

        public void UpdateGridWithChangedItem(Entities.Software usr)
         {
             ListViewItem rowToUpdate = null;

             foreach (ListViewItem row in this.listView1.Items)
             {
                 if (row.Text == usr.Name)
                 {
                     rowToUpdate = row;
                 }
             }
 
             if (rowToUpdate != null)
             {
                 rowToUpdate.Text = usr.Name;
                 //rowToUpdate.SubItems[1].Text = usr.Name;
                 //rowToUpdate.SubItems[2].Text = usr.LastName;
                 //rowToUpdate.SubItems[3].Text = usr.Department;
                 //rowToUpdate.SubItems[4].Text = Enum.GetName(typeof(User.SexOfPerson), usr.Sex);
             }
         }

        public void RemoveItemFromGrid(Entities.Software usr)
         {
 
             ListViewItem rowToRemove = null;

             foreach (ListViewItem row in this.listView1.Items)
             {
                 if (row.Text == usr.Name)
                 {
                     rowToRemove = row;
                 }
             }
 
             if (rowToRemove != null)
             {
                 this.listView1.Items.Remove(rowToRemove);
                 this.listView1.Focus();
             }
         }

        public string GetIdOfSelectedItemInGrid()
         {
             if (this.listView1.SelectedItems.Count > 0)
                 return this.listView1.SelectedItems[0].Text;
             else
                 return "";
         }

         public void SetSelectedItemInGrid(Entities.Software usr)
         {
             foreach (ListViewItem row in this.listView1.Items)
             {
                 if (row.Text == usr.Name)
                 {
                     row.Selected = true;
                 }
             }
         }

    }


   

}
