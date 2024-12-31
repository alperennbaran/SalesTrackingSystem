using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.MainPage
{
    public partial class FormMainPage : Form
    {
        DBHelper dBHelper;
        string query;
        public FormMainPage()
        {
            InitializeComponent();
        }
        SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();

        private void FormMainPage_Load(object sender, EventArgs e)
        {
            listCriticalLevel();
            listIndex();
            listCategoryProduct();
            listThingstoDoToday();
            listContactData();
        }
        private void listCriticalLevel()
        {
            try
            {   //SQL
                query = "SELECT name , stock " +
                    "FROM Product " +
                    "WHERE stock < 30 " +
                    "ORDER BY productid";
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
        private void listIndex()
        {
            try
            {   //SQL
                query = "SELECT name , surname, cityname AS city " +
                    "FROM Customer cu, City ct " +
                    "WHERE ct.cityid = cu.cityid";
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
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listCategoryProduct()
        {
            try
            {
                //SQL
                query = "SELECT c.name, COUNT(p.*) AS quantity " +
                    "FROM product p, category c " +
                    "WHERE c.categoryid = p.categoryid " +
                    "GROUP BY c.name;";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl3.DataSource = dataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listThingstoDoToday()
        {
            try
            {
                DateTime today = DateTime.Today;
                //SQL
                query = "SELECT title, content " +
                    "FROM mynotes " +
                    "WHERE date = CURRENT_DATE " +
                    "ORDER BY noteid;";
                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet);
                    gridControl4.DataSource = dataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listContactData()
        {
            try
            {
                // SQL
                query = "SELECT FullName, Title FROM Contact LIMIT 9"; 
                dBHelper = new DBHelper(query); 

                using (dBHelper.connection)
                {
                    DataSet dataSet = new DataSet();
                    dBHelper.dataAdapter.Fill(dataSet); 

                    // Eğer kayıtlar varsa, etiketlere yazdır
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        // Sırasıyla label'lara FullName ve Title değerlerini atama
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            DataRow row = dataSet.Tables[0].Rows[i];
                            string fullNameAndTitle = $"{row["FullName"]} - {row["Title"]}";

                            // Dinamik olarak labelControl1, labelControl2, ..., labelControl9'ı form üzerinden bulma
                            DevExpress.XtraEditors.LabelControl labelControl = this.Controls.Find($"labelControl{i + 1}", true).FirstOrDefault()
                        as DevExpress.XtraEditors.LabelControl;

                            if (labelControl != null)
                            {
                                labelControl.Text = fullNameAndTitle;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No contact data found.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
