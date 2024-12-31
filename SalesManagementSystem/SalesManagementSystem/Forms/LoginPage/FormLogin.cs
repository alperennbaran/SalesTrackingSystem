using DevExpress.XtraBars.Ribbon;
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

namespace SalesManagementSystem.Forms.LoginPage
{
    public partial class FormLogin : Form
    {
        private SalesTrackingSystemEntities db;
        private DBHelper dBHelper;
        private string query;
        private string currentUserRole;
        private Form1 form1 = new Form1();
        public FormLogin()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            Login(textBox1.Text, textBox2.Text);


            /*
            string query = "SELECT COUNT(*) FROM AdminTable WHERE Name = @Name AND Password = @Password";

            try
            {
                dBHelper = new DBHelper(query);
                dBHelper.command.Parameters.AddWithValue("@Name", textBox1.Text);
                dBHelper.command.Parameters.AddWithValue("@Password", textBox2.Text);
                using (dBHelper.connection)
                {
                    

                    int result = Convert.ToInt32(dBHelper.command.ExecuteScalar());

                    if (result > 0)
                    {
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
                    }
                    else
                    {
                        XtraMessageBox.Show("The Login Process Have Failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }
        private void Login(string username, string password)
        {
            string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
            dBHelper = new DBHelper(query);
            dBHelper.command.Parameters.AddWithValue("@Username", username);
            dBHelper.command.Parameters.AddWithValue("@Password", password);

            using (dBHelper.connection)
            {
                var result = dBHelper.command.ExecuteScalar();
                if (result != null)
                {
                    currentUserRole = result.ToString();
                    MessageBox.Show($"The Login Process is Successful! Role: {currentUserRole}");
                    ConfigureRibbonControl();
                    form1.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password!");
                }
            }
        }
        private void ConfigureRibbonControl()
        {
            if (currentUserRole == "Standard")
            {
                // Standard kullanıcıdan belirli RibbonPage'leri gizle
                form1.ribbonPageGroup11.Visible= false;
                form1.ribbonPageGroup12.Visible = false;
                form1.ribbonPage5.Visible = false;
                form1.ribbonPage8.Visible = false;
                form1.ribbonPage2.Visible = false;
            }
            else if (currentUserRole == "Manager")
            {
                // Manager'dan bir şeyleri gizle
                form1.ribbonPageGroup11.Visible = false;
                form1.ribbonPageGroup2.Visible = false;
            }

            // Admin hepsini görür
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
