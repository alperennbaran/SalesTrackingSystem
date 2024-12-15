using System;
using System.Collections.Generic;
using System.ComponentModel;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;

namespace SalesManagementSystem.Forms
{
    public partial class FormProductList : Form
    {
        DBHelper dBHelper;
        string query;
        public FormProductList()
        {
            InitializeComponent();
        }

        SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();
        private void FormProductList_Load(object sender, EventArgs e)
        {
            listProduct();
            lookUpEdit1.Properties.DataSource = db.category.ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            listProduct();
        }

        private void listProduct()
        {
            try
            {   //SQL
                query = "SELECT productid AS product_id, p.name AS product_name, brand, c.name AS category_name, purchase, sale, stock, status " +
                    "FROM Product p, Category c " +
                    "WHERE p.categoryid = c.categoryid " +
                    "ORDER BY productid";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl1.DataSource = dataSet.Tables[0];
                }
                /* EF
                var values = db.product.ToList(); // SELECT * FROM PRODUCT
                gridControl1.DataSource = values;
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Eğer odaklanılan satır negatifse veya sütun boşsa, işlemi durdur
            if (gridView1.FocusedRowHandle < 0 || gridView1.GetFocusedRowCellValue("product_id") == null)
                return;

            // Verileri güvenli bir şekilde atama
            txtID.Text = gridView1.GetFocusedRowCellValue("product_id")?.ToString();
            txtProductName.Text = gridView1.GetFocusedRowCellValue("product_name")?.ToString();
            txtBrand.Text = gridView1.GetFocusedRowCellValue("brand")?.ToString();
            txtPurchasePrice.Text = gridView1.GetFocusedRowCellValue("purchase")?.ToString();
            txtSalePrice.Text = gridView1.GetFocusedRowCellValue("sale")?.ToString();
            txtStock.Text = gridView1.GetFocusedRowCellValue("stock")?.ToString();
            lookUpEdit1.EditValue = gridView1.GetFocusedRowCellValue("category_name")?.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            //SQL
            try
            {
                query = "DELETE FROM Product WHERE productid = @id";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@id", id);
                    dBHelper.command.ExecuteNonQuery();
                
                }
                listProduct();
                MessageBox.Show("The product has been successfully deleted!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            /*EF
            var value = db.product.Find(id);
            db.product.Remove(value); // DELETE FROM Product WHERE productid
            db.SaveChanges();
            */

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            try
            {   //SQL
                query = "UPDATE Product SET name = @name, brand = @brand, purchase = @purchase, sale = @sale, stock = @stock, categoryid = @categoryid " +
                        "WHERE productid = @id";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                    dBHelper.command.Parameters.AddWithValue("@name", txtProductName.Text);
                    dBHelper.command.Parameters.AddWithValue("@brand", txtBrand.Text);
                    dBHelper.command.Parameters.AddWithValue("@purchase", decimal.Parse(txtPurchasePrice.Text));
                    dBHelper.command.Parameters.AddWithValue("@sale", decimal.Parse(txtSalePrice.Text));
                    dBHelper.command.Parameters.AddWithValue("@stock", short.Parse(txtStock.Text));
                    dBHelper.command.Parameters.AddWithValue("@categoryid", Int32.Parse(lookUpEdit1.EditValue.ToString()));
                    dBHelper.command.ExecuteNonQuery();
                }
                /* EF
                var value = db.product.Find(id);
                value.name = txtProductName.Text;
                value.brand = txtBrand.Text;
                value.purchase = decimal.Parse(txtPurchasePrice.Text);
                value.sale = decimal.Parse(txtSalePrice.Text);
                value.stock = short.Parse(txtStock.Text);
                value.categoryid = Int32.Parse(lookUpEdit1.EditValue.ToString()));
                db.SaveChanges();
                */
                listProduct();
                MessageBox.Show("The product has been successfully updated!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

            }

        }
    }
}
