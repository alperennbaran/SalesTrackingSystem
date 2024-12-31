using DevExpress.XtraCharts;
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

namespace SalesManagementSystem.Forms.Products
{
    public partial class FormDefectiveProductList : Form
    {
        DBHelper dBHelper;
        string query;
        SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();

        public FormDefectiveProductList()
        {
            InitializeComponent();
            
            // The Current Number of Defective Products
            query = "SELECT COUNT(productstatus) FROM ProductAcceptance WHERE productstatus = True";
            labelControl7.Text = ExecuteScalarQuery(query);

            // The Number of Repaired Products
            query = "SELECT COUNT(productstatus) FROM ProductAcceptance WHERE productstatus = False";
            labelControl5.Text = ExecuteScalarQuery(query);

            // The Number of Products Awaiting Parts
            query = "SELECT COUNT(*) FROM ProductAcceptance WHERE productstatusdetail = 'Parça bekliyor'";
            labelControl17.Text = ExecuteScalarQuery(query);

            // The number of customer awaiting message
            query = "SELECT COUNT(*) FROM ProductAcceptance WHERE productstatusdetail = 'Mesaj bekliyor'"; 
            labelControl2.Text = ExecuteScalarQuery(query);

            // Total Number of Products
            query = "SELECT COUNT(*) FROM Product";
            labelControl12.Text = ExecuteScalarQuery(query);

            // The number of processes that have cancelled
            query = "SELECT COUNT(*) FROM ProductAcceptance WHERE productstatusdetail = 'İptal bekliyor'";
            labelControl10.Text = ExecuteScalarQuery(query);






        }

        private void FormDefectiveProductList_Load(object sender, EventArgs e)
        {
            // SQL
            string query = @"
            SELECT 
                pa.AcceptanceID AS id,
                c.Name || ' ' || c.Surname AS client_name,
                s.Name || ' ' || s.Surname AS staff_name,
                pa.ArrivalDate AS arrival_date,
                pa.DepartureDate AS departure_date,
                pa.ProductSerialNo AS serial_no
            FROM ProductAcceptance pa
            JOIN Customer c ON pa.CustomerID = c.CustomerID
            JOIN Staff s ON pa.StaffID = s.StaffID
            ORDER BY pa.AcceptanceID;

            ";

            try
            {
                using (var dbHelper = new DBHelper(query))
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
            LoadDefectiveProductChart();
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

        private void LoadDefectiveProductChart()
        {
            try
            {
                string query = @"
                SELECT ProductSerialNo, COUNT(*) AS DefectCount
                FROM ProductAcceptance
                WHERE ProductStatus = 'True'
                GROUP BY ProductSerialNo;";

                List<SeriesPoint> seriesPoints = new List<SeriesPoint>();

                dBHelper = new DBHelper(query);
                using (dBHelper.connection)
                {
                    using (var reader = dBHelper.command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string serialNo = reader["ProductSerialNo"].ToString();
                            int defectCount = Convert.ToInt32(reader["DefectCount"]);
                            seriesPoints.Add(new SeriesPoint(serialNo, defectCount));
                        }
                    }
                }

                // ChartControl ve Seri oluştur
                chartControl1.Series.Clear(); // Eski serileri temizle
                Series series = new Series("Defective Products", ViewType.Pie);

                // Verileri seriye ekle
                foreach (var point in seriesPoints)
                {
                    series.Points.Add(point);
                }

                series.LegendTextPattern = "{A} - {V}"; // {A}: Argüman (Serial No), {V}: Değer (Defect Count)

                chartControl1.Series.Add(series);

                // Legend'i etkinleştir ve konumlandır
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
                chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.Top;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
