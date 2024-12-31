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
    public partial class FormViewDetailedInvoice : Form
    {
        public FormViewDetailedInvoice()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                DataSet ds = new DataSet();

                // Sadece InvoiceID'ye göre arama
                if (!string.IsNullOrWhiteSpace(txtInvoiceID.Text))
                {
                    int id = Convert.ToInt32(txtInvoiceID.Text);

                    // SQL sorgusu
                    query = @"
                    SELECT 
                        InvoiceDetailID AS invoice_detail_id,
                        Product AS product,
                        Quantity AS quantity,
                        Price AS price,
                        Total AS total,
                        InvoiceID AS invoice_id
                    FROM InvoiceDetail
                    WHERE InvoiceID = @invoiceID;";

                    using (var dbHelper = new DBHelper(query))
                    {
                        dbHelper.command.Parameters.AddWithValue("@invoiceID", id);
                        dbHelper.dataAdapter.Fill(ds);
                    }

                    // Series ve SerialNo alanlarını devre dışı bırak
                    txtSeries.Enabled = false;
                    txtSerialNumber.Enabled = false;
                }
                // Series ve SerialNo'ya göre arama
                else if (!string.IsNullOrWhiteSpace(txtSeries.Text) && !string.IsNullOrWhiteSpace(txtSerialNumber.Text))
                {
                    string series = txtSeries.Text;
                    string serialNo = txtSerialNumber.Text;

                    // SQL sorgusu
                    query = @"
                        SELECT 
                            id.InvoiceDetailID AS invoice_detail_id,
                            id.Product AS product,
                            id.Quantity AS quantity,
                            id.Price AS price,
                            id.Total AS total,
                            id.InvoiceID AS invoice_id
                        FROM InvoiceDetail id
                        JOIN InvoiceInfo ii ON id.InvoiceID = ii.InvoiceID
                        WHERE ii.Series = @series AND ii.SerialNo = @serialNo;
                        ";

                    using (var dbHelper = new DBHelper(query))
                    {
                        dbHelper.command.Parameters.AddWithValue("@series", series);
                        dbHelper.command.Parameters.AddWithValue("@serialNo", serialNo);
                        dbHelper.dataAdapter.Fill(ds);
                    }

                    // InvoiceID alanını devre dışı bırak
                    txtInvoiceID.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please enter either InvoiceID or both Series and Serial Number for the search.", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                gridControl1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtSeries.Enabled = string.IsNullOrWhiteSpace(txtInvoiceID.Text);
                txtSerialNumber.Enabled = string.IsNullOrWhiteSpace(txtInvoiceID.Text);
                txtInvoiceID.Enabled = string.IsNullOrWhiteSpace(txtSeries.Text) && string.IsNullOrWhiteSpace(txtSerialNumber.Text);
            }
        }

        private void txtInvoiceID_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInvoiceID.Text))
            {
                txtSeries.Enabled = false;
                txtSerialNumber.Enabled = false;
            }
            else
            {
                txtSeries.Enabled = true;
                txtSerialNumber.Enabled = true;
            }
        }


        private void txtSeries_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSeries.Text) || !string.IsNullOrWhiteSpace(txtSerialNumber.Text))
            {
                txtInvoiceID.Enabled = false;
            }
            else
            {
                txtInvoiceID.Enabled = true;
            }
        }


        private void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSeries.Text) || !string.IsNullOrWhiteSpace(txtSerialNumber.Text))
            {
                txtInvoiceID.Enabled = false;
            }
            else
            {
                txtInvoiceID.Enabled = true;
            }
        }

        private void FormViewDetailedInvoice_Load(object sender, EventArgs e)
        {
            txtInvoiceID.TextChanged += txtInvoiceID_TextChanged;
            txtSeries.TextChanged += txtSeries_TextChanged;
            txtSerialNumber.TextChanged += txtSerialNo_TextChanged;
        }
    }
}
