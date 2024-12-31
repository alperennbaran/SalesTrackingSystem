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
    public partial class FormSalesList : Form
    {
        public FormSalesList()
        {
            InitializeComponent();
        }

        private void FormSalesList_Load(object sender, EventArgs e)
        {

            // SQL 
            string query = @"
                    SELECT 
                        pm.MovementID AS MovementID,
                        p.Name AS ProductName,
                        c.Name || ' ' || c.Surname AS Client,
                        s.Name || ' ' || s.Surname AS Staff,
                        pm.MovementDate AS Date,
                        pm.Quantity AS Quantity,
                        pm.Price AS Price,
                        pm.ProductSerialNo AS Product_Serial_No
                    FROM ProductMovement pm
                    JOIN Product p ON pm.ProductID = p.ProductID
                    JOIN Customer c ON pm.CustomerID = c.CustomerID
                    JOIN Staff s ON pm.StaffID = s.StaffID
                    ORDER BY pm.MovementID";
                  
            try
            {
                using (var dbHelper = new DBHelper(query))
                {
                    DataSet ds = new DataSet();
                    dbHelper.dataAdapter.Fill(ds);
                    gridControl1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}
