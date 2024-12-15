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

        }
        private void listStaff()
        {
            try
            {   //SQL
                query = "SELECT staffid AS staff_id, name, surname, phonenumber AS phone_number, mail, ct.cityname AS city, bank, taxoffice AS tax_office, taxnumber AS tax_number, position, address " +
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
    }
}
