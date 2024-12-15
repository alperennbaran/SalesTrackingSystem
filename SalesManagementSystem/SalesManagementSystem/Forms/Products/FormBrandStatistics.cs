using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
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
    public partial class FormBrandStatistics : Form
    {
        DBHelper dBHelper;
        string query;
        public FormBrandStatistics()
        {
            InitializeComponent();
        }
        private void listBrand()
        {
            try
            {   // SQL
                query = "SELECT brand, COUNT(*) AS total " +
                        "FROM Product " +
                        "GROUP BY brand " +
                        "ORDER BY brand";

                dBHelper = new DBHelper(query);
                DataTable dataTable = new DataTable();

                using (dBHelper.connection)
                {
                    using (var reader = dBHelper.command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
                gridControl1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /* EF
            db = new SalesTrackingSystemEntities();
            var values = db.product.OrderBy(x => x.brand).GroupBy(y => y.brand).Select(z => new
            {
                brand = z.Key,
                total = z.Count()
            });
            gridControl1.DataSource = values.ToList();
            */
        }
        private void FormBrandStatistics_Load(object sender, EventArgs e)
        {   
           listBrand();

            // Total Number of Products
            query = "SELECT COUNT(DISTINCT name) FROM Product";
            labelControl3.Text = ExecuteScalarQuery(query);

            // The Brand With the Highest Sale Price
            query = "SELECT brand FROM Product ORDER BY sale DESC LIMIT 1";
            labelControl7.Text = ExecuteScalarQuery(query);

            // Total Number of Brands
            query = "SELECT COUNT(DISTINCT brand) FROM Product";
            labelControl5.Text = ExecuteScalarQuery(query);

            // The Brand With the Most Products
            query = "SELECT brand FROM Product GROUP BY brand ORDER BY COUNT(*) DESC LIMIT 1";
            labelControl17.Text = ExecuteScalarQuery(query);

            chartControl1.Series["Series 1"].Points.AddPoint("Siemens", 4);
            chartControl1.Series["Series 1"].Points.AddPoint("Arçelik", 2);
            chartControl1.Series["Series 1"].Points.AddPoint("Beko", 3);
            chartControl1.Series["Series 1"].Points.AddPoint("Toshiba", 1);
            chartControl1.Series["Series 1"].Points.AddPoint("Lenovo", 5);

            chartControl2.Series["Categories"].Points.AddPoint("Electronics", 4);
            chartControl2.Series["Categories"].Points.AddPoint("Toys", 3);
            chartControl2.Series["Categories"].Points.AddPoint("Home Appliences", 6);
        }
        private string ExecuteScalarQuery(string sqlQuery)
        {
            string result = "0";
            dBHelper = new DBHelper(sqlQuery);
            using (dBHelper.connection)
            {
                var scalarResult = dBHelper.command.ExecuteScalar();
                if (scalarResult != null)
                {
                    result = scalarResult.ToString();
                }
            }
            return result;
        }
    }
}
