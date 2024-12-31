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
using System.Xml.Linq;

namespace SalesManagementSystem.Forms.Invoices
{
    public partial class FormInvoiceList : Form
    {
        public FormInvoiceList()
        {
            InitializeComponent();
        }

        private void FormInvoiceList_Load(object sender, EventArgs e)
        {
            try
            {
                //SQL
                string queryFaturaBilgi = @"
                SELECT 
                    fb.InvoiceID AS Invoice_ID,
                    fb.Series AS Series,
                    fb.SerialNo AS Serial_No,
                    fb.InvoiceDate AS Date,
                    fb.InvoiceTime AS Time,
                    fb.TaxOffice AS Tax_Office,
                    CONCAT(c.Name, ' ', c.Surname) AS Client,
                    CONCAT(s.Name, ' ', s.Surname) AS Staff
                FROM InvoiceInfo fb
                JOIN Customer c ON fb.CustomerID = c.CustomerID
                JOIN Staff s ON fb.StaffID = s.StaffID";
                
                using (var dbHelper = new DBHelper(queryFaturaBilgi))
                {
                    DataSet ds = new DataSet();
                    dbHelper.dataAdapter.Fill(ds);
                    gridControl1.DataSource = ds.Tables[0];
                }

                // LookupEdit için SQL 
                string queryClient = @"
                SELECT 
                    CustomerID AS id,
                    CONCAT(Name, ' ', Surname) AS name
                FROM Customer";

                using (var dbHelper = new DBHelper(queryClient))
                {
                    DataSet dsCari = new DataSet();
                    dbHelper.dataAdapter.Fill(dsCari);
                    lookUpEdit1.Properties.DataSource = dsCari.Tables[0];
                }
                // LookupEdit için SQL 
                string queryStaff = @"
                SELECT 
                    StaffID AS id,
                    CONCAT(Name, ' ', Surname) AS name
                FROM Staff";

                using (var dbHelper = new DBHelper(queryStaff))
                {
                    DataSet dsCari = new DataSet();
                    dbHelper.dataAdapter.Fill(dsCari);
                    lookUpEdit2.Properties.DataSource = dsCari.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                //SQL
                string queryFaturaBilgi = @"
                SELECT 
                    fb.InvoiceID AS Invoice_ID,
                    fb.Series AS SERI,
                    fb.SerialNo AS Serial_No,
                    fb.InvoiceDate AS Date,
                    fb.InvoiceTime AS Time,
                    fb.TaxOffice AS Tax_Office,
                    CONCAT(c.Name, ' ', c.Surname) AS Client,
                    CONCAT(s.Name, ' ', s.Surname) AS Staff
                FROM InvoiceInfo fb
                JOIN Customer c ON fb.CustomerID = c.CustomerID
                JOIN Staff s ON fb.StaffID = s.StaffID";

                using (var dbHelper = new DBHelper(queryFaturaBilgi))
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


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string series = txtSeries.Text;
                string serialNumber = txtSerialNumber.Text;
                DateTime invoiceDate = Convert.ToDateTime(txtDate.Text);
                string invoiceTime = txtTime.Text;
                string taxOffice = txtTaxOffice.Text;
                int customerID = int.Parse(lookUpEdit1.EditValue.ToString());
                short staffID = short.Parse(lookUpEdit2.EditValue.ToString());

                // SQL query
                string query = @"
                INSERT INTO InvoiceInfo 
                (Series, SerialNo, InvoiceDate, InvoiceTime, TaxOffice, CustomerID, StaffID)
                VALUES 
                (@series, @serialNumber, @invoiceDate, @invoiceTime, @taxOffice, @customerID, @staffID);";

                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@series", series);
                    dbHelper.command.Parameters.AddWithValue("@serialNumber", serialNumber);
                    dbHelper.command.Parameters.AddWithValue("@invoiceDate", invoiceDate);
                    dbHelper.command.Parameters.AddWithValue("@invoiceTime", invoiceTime);
                    dbHelper.command.Parameters.AddWithValue("@taxOffice", taxOffice);
                    dbHelper.command.Parameters.AddWithValue("@customerID", customerID);
                    dbHelper.command.Parameters.AddWithValue("@staffID", staffID);

                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("Invoice has been successfully saved. You can now enter invoice items.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Silinecek faturanın ID'sini alın
                int invoiceID = int.Parse(txtID.Text);

                // SQL sorgusu
                string query = @"
                    DELETE FROM InvoiceInfo 
                    WHERE InvoiceID = @invoiceID;";

                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@invoiceID", invoiceID);
                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("Invoice has been successfully deleted.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the invoice: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
