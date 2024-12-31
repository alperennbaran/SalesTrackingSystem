using DevExpress.Xpo.DB.Helpers;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SalesManagementSystem.Forms.Staff
{
    public partial class FormDepartmentList : Form
    {
        DBHelper dBHelper;
        string query;
        SalesTrackingSystemEntities db;
        public FormDepartmentList()
        {
            InitializeComponent();
        }

        private void FormDepartmentList_Load(object sender, EventArgs e)
        {
            listDeparment();
            // Total Number of Departments
            query = "SELECT COUNT(departmentid) FROM Department";
            labelControl18.Text = ExecuteScalarQuery(query);

            // Total Number of Staffs
            query = "SELECT COUNT(staffid) FROM Staff";
            labelControl12.Text = ExecuteScalarQuery(query);

            // The Department With the Most Staffs
            query = @"
            SELECT d.name 
            FROM Department d
            JOIN Staff s ON d.departmentid = s.departmentid
            GROUP BY d.name
            ORDER BY COUNT(s.staffid) DESC
            LIMIT 1;";
            labelControl14.Text = ExecuteScalarQuery(query);


            // The Department With the Least Staffs
            query = @"
            SELECT d.name
            FROM Department d
            JOIN Staff s ON d.departmentid = s.departmentid
            GROUP BY d.name
            ORDER BY COUNT(s.staffid) ASC
            LIMIT 1;";
            labelControl16.Text = ExecuteScalarQuery(query);

        }
        private void listDeparment()
        {
            try
            {   //SQL
                query = "SELECT departmentid AS department_id, name, comment " +
                    "FROM Department " +
                    "ORDER BY departmentid";
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
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Length <= 50 && txtName.Text != "" && txtBoxComment.Text.Length >= 1)
            {
                // EF
                db = new SalesTrackingSystemEntities();
                department d = new department();
                d.name = txtName.Text;
                d.comment = txtBoxComment.Text;
                db.department.Add(d);
                db.SaveChanges();
                MessageBox.Show("The Department Has Been Successfully Saved!", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Failed to Save Department!", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            //SQL
            try
            {
                query = "DELETE FROM Department WHERE departmentid = @id";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@id", id);
                    dBHelper.command.ExecuteNonQuery();

                }
                listDeparment();
                MessageBox.Show("The Department Has Been Successfully Deleted!", "Information",
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
            listDeparment();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Eğer odaklanılan satır negatifse veya sütun boşsa, işlemi durdur
            if (gridView1.FocusedRowHandle < 0 || gridView1.GetFocusedRowCellValue("department_id") == null)
                return;

            // Verileri güvenli bir şekilde atama
            txtID.Text = gridView1.GetFocusedRowCellValue("department_id")?.ToString();
            txtName.Text = gridView1.GetFocusedRowCellValue("name")?.ToString();
            txtBoxComment.Text = gridView1.GetFocusedRowCellValue("comment")?.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //SQL
            query = "UPDATE Department SET name = @name, comment = @comment " +
                    "WHERE departmentid = @id";
            dBHelper = new DBHelper(query);
            using (dBHelper.connection)
            {
                dBHelper.command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                dBHelper.command.Parameters.AddWithValue("@name", txtName.Text);
                dBHelper.command.Parameters.AddWithValue("@comment", txtBoxComment.Text);
                dBHelper.command.ExecuteNonQuery();
            }
            listDeparment();
            MessageBox.Show("The product has been successfully updated!", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
