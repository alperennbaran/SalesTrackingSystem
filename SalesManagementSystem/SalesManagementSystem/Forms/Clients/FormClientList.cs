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

namespace SalesManagementSystem.Forms.Clients
{
    public partial class FormClientList : Form
    {

        DBHelper dBHelper;
        string query;
        public FormClientList()
        {
            InitializeComponent();
        }

        private void FormClientList_Load(object sender, EventArgs e)
        {
            // SQL
            string query = "SELECT cityid, cityname FROM City";

            using (var dbHelper = new DBHelper(query))
            {

                DataTable dt = new DataTable();
                dbHelper.dataAdapter.Fill(dt);  // Veriyi DataTable'a dolduruyoruz

                lookUpEdit1.Properties.DataSource = dt;
                lookUpEdit1.Properties.DisplayMember = "cityname";
                lookUpEdit1.Properties.ValueMember = "cityid";
            }
            listCustomer();
            // Total Client Count
            query = "SELECT COUNT(*) FROM Customer";
            labelControl18.Text = ExecuteScalarQuery(query);

            // Total Number of Positions
            query = "SELECT COUNT(DISTINCT position) FROM Customer";
            labelControl12.Text = ExecuteScalarQuery(query);

            // Total Number of Cities
            query = "SELECT COUNT(DISTINCT cityname) " +
                "FROM Customer cu, City ct " +
                "WHERE cu.cityid = ct.cityid";
            labelControl14.Text = ExecuteScalarQuery(query);

            //  The City With the Most Client
            query = "SELECT cityname " +
                "FROM Customer cu, City ct " +
                "WHERE cu.cityid = ct.cityid " +
                "GROUP BY cityname " +
                "ORDER BY COUNT(DISTINCT cityname) DESC " +
                "LIMIT 1";
            labelControl16.Text = ExecuteScalarQuery(query);
        }
        private void listCustomer()
        {
            try
            {   //SQL
                query = "SELECT customerid AS customer_id, name, surname, phonenumber AS phone_number, mail AS email, ct.cityname AS city, bank, taxoffice AS tax_office, taxnumber AS tax_number, position, address " +
                    "FROM Customer cu, City ct " +
                    "WHERE cu.cityid = ct.cityid " +
                    "ORDER BY customerid";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl1.DataSource = dataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            // Eğer odaklanılan satır negatifse veya sütun boşsa, işlemi durdur
            if (gridView1.FocusedRowHandle < 0 || gridView1.GetFocusedRowCellValue("customer_id") == null)
                return;

            // Verileri güvenli bir şekilde atama
            txtClientID.Text = gridView1.GetFocusedRowCellValue("customer_id")?.ToString();
            txtName.Text = gridView1.GetFocusedRowCellValue("name")?.ToString();
            txtSurname.Text = gridView1.GetFocusedRowCellValue("surname")?.ToString();
            txtPhone.Text = gridView1.GetFocusedRowCellValue("phone_number")?.ToString();
            txtmail.Text = gridView1.GetFocusedRowCellValue("email")?.ToString();
            lookUpEdit1.Text = gridView1.GetFocusedRowCellValue("city")?.ToString();
            txtBank.Text = gridView1.GetFocusedRowCellValue("bank")?.ToString();
            txtTaxOffice.Text = gridView1.GetFocusedRowCellValue("tax_office")?.ToString();
            txtTaxNumber.Text = gridView1.GetFocusedRowCellValue("tax_number")?.ToString();
            txtPosition.Text = gridView1.GetFocusedRowCellValue("position")?.ToString();
            txtAddress.Text = gridView1.GetFocusedRowCellValue("address")?.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtClientID.Text);
            //SQL
            try
            {
                query = "DELETE FROM Customer WHERE customerid = @id";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@id", id);
                    dBHelper.command.ExecuteNonQuery();

                }
                listCustomer();
                MessageBox.Show("The Customer has been successfully deleted!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtClientID.Text);
            try
            {   //SQL
                query = "UPDATE customer SET name = @name, surname = @surname, phonenumber = @phone, mail = @mail, cityid = @cityid, bank = @bank, taxoffice = @taxoffice, taxnumber = @taxnumber, position = @position, address = @address " +
                        "WHERE customerid = @id";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@id", int.Parse(txtClientID.Text));
                    dBHelper.command.Parameters.AddWithValue("@name", txtName.Text);
                    dBHelper.command.Parameters.AddWithValue("@surname", txtSurname.Text);
                    dBHelper.command.Parameters.AddWithValue("@phone", txtPhone.Text);
                    dBHelper.command.Parameters.AddWithValue("@mail", txtmail.Text);
                    dBHelper.command.Parameters.AddWithValue("@cityid", short.Parse(lookUpEdit1.EditValue.ToString()));
                    dBHelper.command.Parameters.AddWithValue("@bank", txtBank.Text);
                    dBHelper.command.Parameters.AddWithValue("@taxoffice", txtTaxOffice.Text);
                    dBHelper.command.Parameters.AddWithValue("@taxnumber", txtTaxNumber.Text);
                    dBHelper.command.Parameters.AddWithValue("@position", txtPosition.Text);
                    dBHelper.command.Parameters.AddWithValue("@address", txtAddress.Text);

                    dBHelper.command.ExecuteNonQuery();
                }

                listCustomer();
                MessageBox.Show("The Customer has been successfully updated!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            listCustomer();
        }
    }

}