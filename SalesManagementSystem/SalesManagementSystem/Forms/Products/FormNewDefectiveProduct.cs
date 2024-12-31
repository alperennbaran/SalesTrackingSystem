using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Products
{
    
    public partial class FormNewDefectiveProduct : Form
    {
        SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();
        public FormNewDefectiveProduct()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(lookUpEdit1.EditValue.ToString(), out int customerID) ||
                    !DateTime.TryParse(txtDate.Text, out DateTime arrivalDate) ||
                    !short.TryParse(lookUpEdit2.EditValue.ToString(), out short staffID) ||
                    string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    MessageBox.Show("Please fill in all fields correctly!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                // SQL
                string query = @"
                INSERT INTO ProductAcceptance 
                (CustomerID, ArrivalDate, StaffID, ProductSerialNo)
                VALUES (@customerID, @arrivalDate, @staffID, @serialNo);";

                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@customerID", customerID);
                    dbHelper.command.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    dbHelper.command.Parameters.AddWithValue("@staffID", staffID);
                    dbHelper.command.Parameters.AddWithValue("@serialNo", txtSerialNo.Text);

                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("The product acceptance process has been successfully recorded.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormNewDefectiveProduct_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Properties.DataSource = (from x in db.customer
                                                 select new
                                                 {
                                                     x.customerid,
                                                     name = x.name + " " + x.surname
                                                 }).ToList();
            lookUpEdit2.Properties.DataSource = (from x in db.staff
                                                 select new
                                                 {
                                                     x.staffid,
                                                     name = x.name + " " + x.surname
                                                 }).ToList();
        }
        


    }
}
