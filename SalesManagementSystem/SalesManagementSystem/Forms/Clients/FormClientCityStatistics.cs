using DevExpress.Xpo.DB.Helpers;
using Npgsql;
using NpgsqlTypes;
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
    public partial class FormClientCityStatistics : Form
    {
        SalesTrackingSystemEntities db;
        DBHelper dBHelper;
        string query;
        public FormClientCityStatistics()
        {
            InitializeComponent();
        }

        private void FormClientCityStatistics_Load(object sender, EventArgs e)
        {
            
            try
            {   //SQL
                query = "SELECT ct.cityname AS city_name, COUNT(*) AS Total " +
                    "FROM city ct, customer cu " +
                    "WHERE ct.cityid = cu.cityid " +
                    "GROUP BY cityname " +
                    "ORDER BY COUNT(*) DESC";
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
            try
            {
                query = "SELECT ct.cityname AS city_name, COUNT(*) AS Total " +
                        "FROM city ct, customer cu " +
                        "WHERE ct.cityid = cu.cityid " +
                        "GROUP BY cityname ";

                dBHelper = new DBHelper(query);

                using (dBHelper.connection)
                {
                    DataTable dataTable = new DataTable();
                    dBHelper.dataAdapter.Fill(dataTable); 

                    foreach (DataRow row in dataTable.Rows) 
                    {
                        chartControl1.Series["Series 1"].Points.AddPoint(
                            row["city_name"].ToString(),       
                            Convert.ToInt32(row["Total"])       
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
