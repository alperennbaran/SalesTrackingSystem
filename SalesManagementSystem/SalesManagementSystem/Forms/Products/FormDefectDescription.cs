using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Products
{
    public partial class FormDefectDescription : Form
    {
        public FormDefectDescription()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string comment = richTextBox1.Text;
                string serialNo = textEdit1.Text;
                DateTime date = DateTime.Parse(textEdit2.Text);

                // SQL sorgusu
                string query = @"
            INSERT INTO ProductTracking (Comment, SerialNo, TrackingDate)
            VALUES (@comment, @serialNo, @date);";

                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@comment", comment);
                    dbHelper.command.Parameters.AddWithValue("@serialNo", serialNo);
                    dbHelper.command.Parameters.AddWithValue("@date", date);
                    dbHelper.command.ExecuteNonQuery();
                }
                MessageBox.Show("Product tracking details updated successfully.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

  

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            textEdit2.Text = DateTime.Now.ToShortDateString();
        }

      
    }
}