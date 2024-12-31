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
        SalesTrackingSystemEntities db = new SalesTrackingSystemEntities();
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
            
            product p = new product();
            p.name = txtProductName.Text;
            p.brand = txtBrand.Text;
            p.purchase = decimal.Parse(txtPurchase.Text);
            p.sale = decimal.Parse(txtSale.Text);
            p.stock = short.Parse(txtStock.Text);
            p.categoryid = Int32.Parse(lookUpEdit1.EditValue.ToString());
            db.product.Add(p);
            db.SaveChanges();
            MessageBox.Show("The Product Has Been Successfully Saved!");
        }

        private void FormNewProduct_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Properties.DataSource = (from x in db.category
                                                 select new
                                                 {
                                                     x.categoryid,
                                                     x.name
                                                 }).ToList();
        }
    }
}
