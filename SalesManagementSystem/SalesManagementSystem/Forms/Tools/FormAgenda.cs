using DevExpress.XtraEditors;
using SalesManagementSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Tools
{
    public partial class FormAgenda : Form
    {

        DBHelper dBHelper;
        string query;

        SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();
        public FormAgenda()
        {
            InitializeComponent();
        }

        private void FormNewNote_Load(object sender, EventArgs e)
        {
            try
            {
                // Status = false olan kayıtlar için sorgu
                query = "SELECT NoteID AS note_id, Title, Content, Status " +
                        "FROM MyNotes " +
                        "WHERE Status = FALSE " +
                        "ORDER BY NoteID";

                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl1.DataSource = dataSet.Tables[0];
                }

                // Status = true olan kayıtlar için sorgu
                query = "SELECT NoteID AS note_id, Title, Content, Status " +
                        "FROM MyNotes " +
                        "WHERE Status = TRUE " +
                        "ORDER BY NoteID";

                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl2.DataSource = dataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string content = txtContent.Text.Trim();
            bool status = false;

            // Boşluk kontrolü
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("The title cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("The content cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            query = "INSERT INTO MyNotes (Title, Content, Status) VALUES (@title, @content, @status);";

            try
            {
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@title", title);
                    dBHelper.command.Parameters.AddWithValue("@content", content);
                    dBHelper.command.Parameters.AddWithValue("@status", status);

                    dBHelper.command.ExecuteNonQuery();
                }

                MessageBox.Show("The note has been successfully saved.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Alanları temizle
                txtTitle.Text = string.Empty;
                txtContent.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkEditStatus.Checked)
            {
                int id = int.Parse(txtNoteID.Text);               
                string query = "UPDATE MyNotes SET Status = TRUE WHERE NoteID = @id";
                try
                {
                    using (var dbHelper = new DBHelper(query))
                    {
                        dbHelper.command.Parameters.AddWithValue("@id", id);
                        dbHelper.command.ExecuteNonQuery();
                    }
                    MessageBox.Show("The note status has been changed.", "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                // Status = false olan kayıtlar için sorgu
                query = "SELECT NoteID AS note_id, Title, Content, Status " +
                        "FROM MyNotes " +
                        "WHERE Status = FALSE " +
                        "ORDER BY NoteID";

                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl1.DataSource = dataSet.Tables[0];
                }

                // Status = true olan kayıtlar için sorgu
                query = "SELECT NoteID AS note_id, Title, Content, Status " +
                        "FROM MyNotes " +
                        "WHERE Status = TRUE " +
                        "ORDER BY NoteID";

                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl2.DataSource = dataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtNoteID.Text = gridView1.GetFocusedRowCellValue("note_id")?.ToString();
        }
    }
}

