using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Staff
{
    public partial class FormNewDepartment : Form
    {
        SalesTrackingSystemEntities db;
        public FormNewDepartment()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDepartmentName.Text.Trim() != "" && txtComment.Text.Trim() != "" && !txtDepartmentName.Text.Trim().Equals("Category Name", StringComparison.OrdinalIgnoreCase) && !txtComment.Text.Trim().Equals("Comment", StringComparison.OrdinalIgnoreCase))
            {
                // EF
                db = new SalesTrackingSystemEntities();
                department d = new department();
                d.name = txtDepartmentName.Text;
                d.comment = txtComment.Text;
                db.department.Add(d);
                db.SaveChanges();
                MessageBox.Show("The New Department Has Been Successfull Saved!");
            }
            else
            {
                MessageBox.Show("Please Enter a Department Name in True Form!");
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
