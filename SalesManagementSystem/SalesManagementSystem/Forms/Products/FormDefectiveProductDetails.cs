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
    public partial class FormDefectiveProductDetails : Form
    {
        public FormDefectiveProductDetails()
        {
            InitializeComponent();
        }

        private void FormDefectiveProductDetails_Load(object sender, EventArgs e)
        {
            // SQL query
            string query = @"
            SELECT 
                TrackingID AS tracking_id, 
                SerialNo AS serial_number, 
                TrackingDate AS tracking_date, 
                Comment AS description 
            FROM ProductTracking;";

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
