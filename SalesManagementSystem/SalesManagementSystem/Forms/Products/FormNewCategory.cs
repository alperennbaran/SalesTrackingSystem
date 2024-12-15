using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms
{
    public partial class FormNewCategory : Form
    {
        public FormNewCategory()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text.Trim() != "" && !txtCategoryName.Text.Trim().Equals("Category Name", StringComparison.OrdinalIgnoreCase))
            {
                // EF
                SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();
                category c = new category();
                c.name = txtCategoryName.Text;
                db.category.Add(c);
                db.SaveChanges();
                MessageBox.Show("The New Category Has Been Successfull Saved!");
            }
            else
            {
                MessageBox.Show("Please Enter a Category Name in True Form!");
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
