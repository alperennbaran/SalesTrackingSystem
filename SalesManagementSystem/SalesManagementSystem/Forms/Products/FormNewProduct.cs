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
    public partial class FormNewProduct : Form
    {
        public FormNewProduct()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // EF
            SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();
            product p = new product();
            p.name = txtProductName.Text;
            p.brand = txtBrand.Text;
            p.purchase = decimal.Parse(txtPurchase.Text);
            p.sale = decimal.Parse(txtSale.Text);
            p.stock = short.Parse(txtStock.Text);
            p.categoryid = Int32.Parse(txtCategory.Text);
            db.product.Add(p);
            db.SaveChanges();
            MessageBox.Show("The Product Has Been Successfully Saved!");
        }
    }
}
