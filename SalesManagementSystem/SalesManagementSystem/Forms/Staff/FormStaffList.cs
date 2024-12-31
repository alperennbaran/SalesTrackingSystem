using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SalesManagementSystem.Forms.Staff
{
    public partial class FormStaffList : Form
    {
        DBHelper dBHelper;
        string query;
        SalesTrackingSystemEntities db;
        public FormStaffList()
        {
            InitializeComponent();
        }

        private void FormStaffList_Load(object sender, EventArgs e)
        {
            listStaff();
            LoadStaffDetails();
            try
            {
                // SQL
                string query = "SELECT DepartmentID, Name FROM Department";

                using (var dbHelper = new DBHelper(query))
                {

                    DataTable dt = new DataTable();
                    dbHelper.dataAdapter.Fill(dt);  // Veriyi DataTable'a dolduruyoruz

                    // lookupEdit1'e veri kaynağını bağlıyoruz
                    lookUpEdit1.Properties.DataSource = dt;
                    lookUpEdit1.Properties.DisplayMember = "name";  // Görüntülenecek alan (Departman Adı)
                    lookUpEdit1.Properties.ValueMember = "departmentid";  // Değer olarak kullanılacak alan (Departman ID)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }


        private void listStaff()
        {
            try
            {
                // SQL Sorgusu
                string query = "SELECT staffid, s.name AS name, s.surname AS surname, d.name as department, s.phone as phone_number, s.mail as email " +
                    "FROM staff s, department d " +
                    "WHERE s.departmentid = d.departmentid";

                using (var dbHelper = new DBHelper(query))
                {
                    DataSet dataSet = new DataSet();
                    dbHelper.dataAdapter.Fill(dataSet);
                    gridControl1.DataSource = dataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int staffID = int.Parse(txtID.Text);

                // Prosedürü çağırma
                string query = "CALL DeleteStaffCascade(@staffID);";

                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@staffID", staffID);
                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("Staff record has been successfully deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listStaff(); // Listeyi yenile
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnList_Click(object sender, EventArgs e)
        {
            listStaff();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int staffID = int.Parse(txtID.Text);
                string name = txtName.Text;
                string surname = txtSurname.Text;
                int departmentID = int.Parse(lookUpEdit1.EditValue.ToString());
                string email = txtMail.Text;
                string phone = txtPhoneNumber.Text;

                // Prosedürü çağırma
                string query = "CALL UpdateStaff(@staffID, @name, @surname, @departmentID, @mail, @phone);";


                using (var dbHelper = new DBHelper(query))
                {
                    dbHelper.command.Parameters.AddWithValue("@staffID", staffID);
                    dbHelper.command.Parameters.AddWithValue("@name", name);
                    dbHelper.command.Parameters.AddWithValue("@surname", surname);
                    dbHelper.command.Parameters.AddWithValue("@departmentID", departmentID);
                    dbHelper.command.Parameters.AddWithValue("@mail", email);
                    dbHelper.command.Parameters.AddWithValue("@phone", phone);

                    dbHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("Staff record has been successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listStaff(); // Listeyi yenile
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                // Eğer odaklanılan satır negatifse veya bazı sütunlar boşsa, işlemi durdur
                if (gridView1.FocusedRowHandle < 0 || gridView1.GetFocusedRowCellValue("staffid") == null)
                    return;

                // Verileri forma aktar
                txtID.Text = gridView1.GetFocusedRowCellValue("staffid")?.ToString();
                txtName.Text = gridView1.GetFocusedRowCellValue("name")?.ToString();
                txtSurname.Text = gridView1.GetFocusedRowCellValue("surname")?.ToString();
                lookUpEdit1.Text = gridView1.GetFocusedRowCellValue("department").ToString();
                //txtPhotograph.Text = gridView1.GetFocusedRowCellValue("Photograph")?.ToString();
                txtMail.Text = gridView1.GetFocusedRowCellValue("email")?.ToString();
                txtPhoneNumber.Text = gridView1.GetFocusedRowCellValue("phone_number")?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStaffDetails()
        {
            try
            {
                // Prosedürü çağırma
                string callProcedureQuery = "CALL GetFirstThreeStaff();";

                using (var dbHelper = new DBHelper(callProcedureQuery))
                {
                    dbHelper.command.ExecuteNonQuery(); // Prosedürü çağır
                }

                // Geçici tablodan sonuçları çekme
                string query = "SELECT * FROM TempStaffDetails;";

                List<string[]> staffDetails = new List<string[]>();

                using (var dbHelper = new DBHelper(query))
                {
                    using (var reader = dbHelper.command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullName = reader["fullname"].ToString();
                            string department = reader["department"].ToString();
                            string email = reader["email"].ToString();

                            staffDetails.Add(new string[] { fullName, department, email });
                        }
                    }
                }

                // İlk üç personelin bilgilerini ilgili labellara set etme
                if (staffDetails.Count > 0) lblFullName1.Text = staffDetails[0][0];
                lblDepartment1.Text = staffDetails[0][1];
                lblEMail1.Text = staffDetails[0][2];

                if (staffDetails.Count > 1) lblFullName2.Text = staffDetails[1][0];
                lblDepartment2.Text = staffDetails[1][1];
                lblEMail2.Text = staffDetails[1][2];

                if (staffDetails.Count > 2) lblFullName3.Text = staffDetails[2][0];
                lblDepartment3.Text = staffDetails[2][1];
                lblEMail3.Text = staffDetails[2][2];
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
