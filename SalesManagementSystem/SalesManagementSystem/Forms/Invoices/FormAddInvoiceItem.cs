using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Invoices
{
    public partial class FormAddInvoiceItem : Form
    {
        public FormAddInvoiceItem()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                string product = txtProduct.Text;
                short quantity = short.Parse(txtQuantity.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                decimal total = decimal.Parse(txtTotal.Text);
                int invoiceID = int.Parse(txtInvoiceID.Text);

                // SQL
                string query = @"
                INSERT INTO InvoiceDetail (Product, Quantity, Price, Total, InvoiceID)
                VALUES (@product, @quantity, @price, @total, @invoiceID);";

                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@product", product);
                    dbHelper.command.Parameters.AddWithValue("@quantity", quantity);
                    dbHelper.command.Parameters.AddWithValue("@price", price);
                    dbHelper.command.Parameters.AddWithValue("@total", total);
                    dbHelper.command.Parameters.AddWithValue("@invoiceID", invoiceID);

                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("Invoice item has been successfully added.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormAddInvoiceItem_Load(object sender, EventArgs e)
        {
            LoadInvoiceDetails();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            LoadInvoiceDetails();
        }


        private void LoadInvoiceDetails()
        {
            try
            {
                //burayı veritabanına fonksiyon olarak ekle
                // Function call
                string query = "SELECT * FROM GetInvoiceDetails();";

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
