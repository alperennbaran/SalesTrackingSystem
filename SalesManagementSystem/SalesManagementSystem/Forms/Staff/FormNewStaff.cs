using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Staff
{
    public partial class FormNewStaff : Form
    {
        public FormNewStaff()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Textbox ve lookupEdit'ten gelen değerleri al
                string name = txtName.Text.Trim();
                string surname = txtSurname.Text.Trim();
                string mail = txtMail.Text.Trim();
                string phone = txtPhone.Text.Trim();
                int departmentID = int.Parse(lookUpEdit1.EditValue.ToString());

                // Eksik veri kontrolü
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) ||
                    string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(phone) || departmentID == 0)
                {
                    MessageBox.Show("All fields must be filled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // SQL sorgusu
                string query = @"
                INSERT INTO Staff (Name, Surname, DepartmentID, Mail, Phone)
                VALUES (@name, @surname, @departmentID, @mail, @phone);";

                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@name", name);
                    dbHelper.command.Parameters.AddWithValue("@surname", surname);
                    dbHelper.command.Parameters.AddWithValue("@departmentID", departmentID);
                    dbHelper.command.Parameters.AddWithValue("@mail", mail);
                    dbHelper.command.Parameters.AddWithValue("@phone", phone);

                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("New staff has been successfully added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Alanları temizle
                txtName.Text = string.Empty;
                txtSurname.Text = string.Empty;
                txtMail.Text = string.Empty;
                txtPhone.Text = string.Empty;
                lookUpEdit1.EditValue = null;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the staff: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void loadlookup()
        {
            try
            {
                // SQL sorgusu
                string query = "SELECT DepartmentID, Name FROM Department";

                using (var dbHelper = new DBHelper(query))
                {
                    DataTable dt = new DataTable();
                    dbHelper.dataAdapter.Fill(dt);  // Veriyi DataTable'a dolduruyoruz

                    // lookUpEdit1'e veri kaynağını bağlıyoruz
                    lookUpEdit1.Properties.DataSource = dt;
                    lookUpEdit1.Properties.DisplayMember = "name";  // Görüntülenecek alan (Departman Adı)
                    lookUpEdit1.Properties.ValueMember = "departmentid";  // Değer olarak kullanılacak alan (Departman ID)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading departments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormNewStaff_Load(object sender, EventArgs e)
        {
            loadlookup();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
