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

namespace SalesManagementSystem.Forms.Clients
{
    public partial class FormNewClient : Form
    {
        private SalesTrackingSystemEntities db;
        private DBHelper dBHelper;
        private string query;
        public FormNewClient()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // SQL sorgusu: müşteri ekleme
                string query = "INSERT INTO customer (name, surname, phonenumber, mail, cityid, bank, taxoffice, taxnumber, position, address) " +
                               "VALUES (@name, @surname, @phone, @mail, (SELECT cityid FROM city WHERE cityname = @cityname), @bank, @taxoffice, @taxnumber, @position, @address)";

                // DBHelper kullanarak bağlantı oluştur
                dBHelper = new DBHelper(query);

                // Parametreler
                dBHelper.command.Parameters.AddWithValue("@name", txtName.Text);
                dBHelper.command.Parameters.AddWithValue("@surname", txtSurname.Text);
                dBHelper.command.Parameters.AddWithValue("@phone", txtPhoneNumber.Text);
                dBHelper.command.Parameters.AddWithValue("@mail", txtEMail.Text);
                dBHelper.command.Parameters.AddWithValue("@cityname", txtCity.Text);
                dBHelper.command.Parameters.AddWithValue("@bank", txtBank.Text);
                dBHelper.command.Parameters.AddWithValue("@taxoffice", txtTaxOffice.Text);
                dBHelper.command.Parameters.AddWithValue("@taxnumber", txtTaxNumber.Text);
                dBHelper.command.Parameters.AddWithValue("@position", txtPosition.Text);
                dBHelper.command.Parameters.AddWithValue("@address", txtAddress.Text);

                // Sorguyu çalıştır
                dBHelper.command.ExecuteNonQuery();
                MessageBox.Show("The New Customer Has Been Successfully Added!", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        /* EF
        db = new SalesTrackingSystemEntities();
        customer c = new customer();
        c.name = txtName.Text;
        c.surname = txtSurname.Text;
        c.phonenumber = txtPhoneNumber.Text;
        c.mail = txtEMail.Text;
        c.cityid = txtCi;
        c.bank = txtBank.Text;
        c.taxoffice = txtTaxOffice.Text;
        c.taxnumber = txtTaxNumber.Text;
        c.position = txtPosition.Text;
        c.address = txtAddress.Text;
        db.customer.Add(c);
        db.SaveChanges();
        MessageBox.Show("The New Customer Has Been Successfull Saved!");
        */
    }
}
}
