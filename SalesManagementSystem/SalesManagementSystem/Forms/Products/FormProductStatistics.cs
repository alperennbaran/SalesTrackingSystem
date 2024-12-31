using DevExpress.Xpo.DB.Helpers;
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
    public partial class FormProductStatistics : Form
    {
        DBHelper dbHelper;
        string query;
        public FormProductStatistics()
        {
            InitializeComponent();
        }

        private void FormProductStatistics_Load(object sender, EventArgs e)
        {
            try
            {   // SQL
                // Total Product Count
                query = "SELECT COUNT(*) FROM Product";
                labelControl2.Text = ExecuteScalarQuery(query);

                // Total Brand Count
                query = "SELECT COUNT(*) FROM Category";
                labelControl3.Text = ExecuteScalarQuery(query);

                // Total Stock Count
                query = "SELECT SUM(stock) FROM Product";
                labelControl5.Text = ExecuteScalarQuery(query);

                // Critical Level
                labelControl7.Text = "10";

                // The Most Stocked Product
                query = "SELECT name FROM Product ORDER BY stock DESC LIMIT 1";
                labelControl9.Text = ExecuteScalarQuery(query);

                // The Least Stocked Product
                query = "SELECT name FROM Product ORDER BY stock ASC LIMIT 1";
                labelControl11.Text = ExecuteScalarQuery(query);

                // The Product With the Highest Sale Price
                query = "SELECT name FROM Product ORDER BY sale DESC LIMIT 1";
                labelControl15.Text = ExecuteScalarQuery(query);

                // The Product With the Lowest Sale Price
                query = "SELECT name FROM Product ORDER BY sale ASC LIMIT 1";
                labelControl23.Text = ExecuteScalarQuery(query);

                // Total Number of Electronics Products
                query = "SELECT COUNT(*) FROM Product WHERE categoryid = 1";
                labelControl35.Text = ExecuteScalarQuery(query);

                // Total Number of Furniture Products
                query = "SELECT COUNT(*) FROM Product WHERE categoryid = 7";
                labelControl39.Text = ExecuteScalarQuery(query);

                // Total Number of Home Appliences Products
                query = "SELECT COUNT(*) FROM Product WHERE categoryid = 3";
                labelControl25.Text = ExecuteScalarQuery(query);

                // Total Number of Brands
                query = "SELECT COUNT(DISTINCT brand) FROM Product";
                labelControl19.Text = ExecuteScalarQuery(query);

                // Total Number of Defective Products
                query = "SELECT COUNT(productserialno) FROM ProductAcceptance WHERE productstatus = True";
                labelControl33.Text = ExecuteScalarQuery(query);

                // The Category That Has the Most Products
                query = "SELECT c.name " +
                    "FROM Category c, Product p " +
                    "WHERE c.categoryid = p.categoryid " +
                    "GROUP BY c.Name " +
                    "ORDER BY COUNT(p.ProductID) DESC " +
                    "LIMIT 1;";
                labelControl13.Text = ExecuteScalarQuery(query);
               
                // The Brand With the Most Products
                query = "SELECT brand FROM Product GROUP BY brand ORDER BY COUNT(*) DESC LIMIT 1";
                labelControl37.Text = ExecuteScalarQuery(query);

                // The Number of Repaired Products
                query = "SELECT COUNT(productstatus) FROM ProductAcceptance WHERE productstatus = False";
                labelControl21.Text = ExecuteScalarQuery(query);

                // The Number of Toys in Stock
                query = "SELECT COUNT(*) FROM Product WHERE categoryid = 5";
                labelControl31.Text = ExecuteScalarQuery(query);

                // Total Number of Staff
                query = "SELECT COUNT(*) FROM Staff";
                labelControl29.Text = ExecuteScalarQuery(query);

                //en çok işlem yapan personel
                query = "SELECT CONCAT(s.Name, ' ', s.Surname) AS FullName, COUNT(*) AS TotalMovements FROM Staff s JOIN ProductMovement pm ON s.StaffID = pm.StaffID GROUP BY s.StaffID, s.Name, s.Surname ORDER BY TotalMovements DESC LIMIT 1";
                labelControl27.Text = ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /* LINQ
            db = new SalesTrackingSystemEntities();
            labelControl2.Text = db.product.Count().ToString();
            labelControl3.Text = db.category.Count().ToString();
            labelControl5.Text = db.product.Sum(x => x.stock).ToString();
            labelControl7.Text = "10";
            labelControl9.Text = (from x in db.product
                                   orderby x.stock descending
                                   select x.name).FirstOrDefault();
            labelControl11.Text = (from x in db.product
                                   orderby x.stock ascending
                                   select x.name).FirstOrDefault();
            labelControl15.Text = (from x in db.product
                                   orderby x.sale descending
                                   select x.name).FirstOrDefault();
            labelControl23.Text = (from x in db.product
                                   orderby x.sale ascending
                                   select x.name).FirstOrDefault();
            labelControl35.Text = db.product.Count(x => x.categoryid == 1).ToString();
            labelControl39.Text = db.product.Count(x => x.categoryid == 7).ToString();
            labelControl25.Text = db.product.Count(x => x.categoryid == 3).ToString();
            labelControl19.Text = (from x in db.product
                                   select x.brand).Distinct().Count().ToString();
            */
        }
        
        // Helper method: Executes SQL queries that return a single value.

        private string ExecuteScalarQuery(string sqlQuery)
        {
            string result = "0";
            dbHelper = new DBHelper(sqlQuery);
            using (dbHelper.connection)
            {
                var scalarResult = dbHelper.command.ExecuteScalar();
                if (scalarResult != null)
                {
                    result = scalarResult.ToString();
                }
            }
            return result;
        }
    }
}
