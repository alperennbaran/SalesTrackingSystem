using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProductListForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.FormProductList formProductList = new Forms.FormProductList();
            formProductList.MdiParent = this;
            formProductList.Show();
        }

        private void btnNewProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.FormNewProduct formNewProduct = new Forms.FormNewProduct();
            formNewProduct.Show();
        }

        private void btnCategoryList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.FormCategory formCategory = new Forms.FormCategory();
            formCategory.MdiParent = this;
            formCategory.Show();
        }

        private void btnNewCategory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.FormNewCategory formNewCategory = new Forms.FormNewCategory();
            formNewCategory.Show();
        }

        private void btnProductStatistics_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.FormProductStatistics formProductStatistics = new Forms.FormProductStatistics();
            formProductStatistics.MdiParent = this;
            formProductStatistics.Show();
        }

        private void btnBrandStatistics_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.FormBrandStatistics formBrandStatistics = new Forms.FormBrandStatistics();
            formBrandStatistics.MdiParent = this;
            formBrandStatistics.Show();
        }

        private void btnClientList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Clients.FormClientList formClientList = new Forms.Clients.FormClientList();
            formClientList.MdiParent = this;
            formClientList.Show();
        }

        private void btnClientCityStatistics_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Clients.FormClientCityStatistics formClientCityStatistics = new Forms.Clients.FormClientCityStatistics();
            formClientCityStatistics.MdiParent = this;
            formClientCityStatistics.Show();
        }

        private void btnNewClient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Clients.FormNewClient formNewClient = new Forms.Clients.FormNewClient();
            formNewClient.Show();
        }

        private void btnDepartmentList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Staff.FormDepartmentList formDepartmentList = new Forms.Staff.FormDepartmentList();
            formDepartmentList.MdiParent = this;
            formDepartmentList.Show();
        }

        private void btnNewDepartment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Staff.FormNewDepartment formNewDepartment = new Forms.Staff.FormNewDepartment();
            formNewDepartment.Show();
        }

        private void btnStaffList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Staff.FormStaffList formStaffList = new Forms.Staff.FormStaffList();
            formStaffList.MdiParent = this;
            formStaffList.Show();
        }
    }
}
