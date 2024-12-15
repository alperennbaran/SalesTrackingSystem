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

namespace SalesManagementSystem.Forms
{
    public partial class FormCategory : Form
    {
        DBHelper dBHelper;
        string query;
        public FormCategory()
        {
            InitializeComponent();
        }

        private void listCategory()
        {
            try
            {   //SQL
                query = "SELECT categoryid AS category_id, name as category_name " +
                        "FROM Category " +
                        "ORDER BY categoryid";

                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl1.DataSource = dataSet.Tables[0];
                }
                /* EF
                db = new SalesTrackingSystemEntities();
                var values = db.product.ToList(); // SELECT * FROM PRODUCT
                gridControl1.DataSource = values;
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormCategory_Load(object sender, EventArgs e)
        {
            listCategory();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             try
            {   //SQL
                query = "INSERT INTO Category (name) VALUES (@name)";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@name", txtCategoryName.Text);  
                    dBHelper.command.ExecuteNonQuery();
                }
                listCategory();
                MessageBox.Show("The product has been successfully saved!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            listCategory();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Eğer grid'de veri yoksa veya odaklanılan satır geçersizse işlem yapma
            if (gridView1.DataRowCount == 0 || gridView1.FocusedRowHandle < 0)
                return;

            // Eğer sütun adları doğru değilse null dönebilir
            txtID.Text = gridView1.GetFocusedRowCellValue("category_id")?.ToString();
            txtCategoryName.Text = gridView1.GetFocusedRowCellValue("category_name")?.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            try
            {
                query = "DELETE FROM Category WHERE categoryid = @id";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@id", id);
                    dBHelper.command.ExecuteNonQuery();

                }
                listCategory();
                MessageBox.Show("The product has been successfully deleted!", "Information",
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
            int id = int.Parse(txtID.Text);
            try
            {   //SQL
                query = "UPDATE Category SET name = @name " +
                        "WHERE categoryid = @id";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    dBHelper.command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                    dBHelper.command.Parameters.AddWithValue("@name", txtCategoryName.Text);
                    dBHelper.command.ExecuteNonQuery();
                }
                listCategory();
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
