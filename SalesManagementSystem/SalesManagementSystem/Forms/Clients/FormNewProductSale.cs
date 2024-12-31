using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Clients
{
    public partial class FormNewProductSale : Form
    {
        public FormNewProductSale()
        {
            InitializeComponent();
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            int productID = int.Parse(txtProductID.Text);
            int customerID = int.Parse(txtClient.Text);
            short staffID = short.Parse(txtStaff.Text);
            DateTime date = DateTime.Parse(txtDate.Text);
            short quantity = short.Parse(txtCount.Text);
            decimal price = decimal.Parse(txtSellingPrice.Text);
            string serialNo = txtSerialNo.Text;

            // SQL 
            string query = @"
                INSERT INTO ProductMovement 
                (ProductID, CustomerID, StaffID, MovementDate, Quantity, Price, ProductSerialNo)
                VALUES (@productID, @customerID, @staffID, @date, @quantity, @price, @serialNo);";

            try
            {
                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@productID", productID);
                    dbHelper.command.Parameters.AddWithValue("@customerID", customerID);
                    dbHelper.command.Parameters.AddWithValue("@staffID", staffID);
                    dbHelper.command.Parameters.AddWithValue("@date", date);
                    dbHelper.command.Parameters.AddWithValue("@quantity", quantity);
                    dbHelper.command.Parameters.AddWithValue("@price", price);
                    dbHelper.command.Parameters.AddWithValue("@serialNo", serialNo);

                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("The product has been successfully sold.", "Information",
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
    }
}
