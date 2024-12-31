using DevExpress.CodeParser.Diagnostics;
using Npgsql;
using SalesManagementSystem.Forms.LoginPage;
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
        private FormLogin formLogin;
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            //ribbonPage5.Visible = false;
            ribbonPageGroup5.Visible = false;
            ribbonPage4.Visible = false;
            ribbonPage2.Visible = false;
            ribbonPage8.Visible = false;

            //ribbonControl1.MdiMergeStyle = 
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

        private void btnCalculator_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void btnExchangeRates_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Tools.FormExchangeRate formExchangeRate = new Forms.Tools.FormExchangeRate();
            formExchangeRate.MdiParent = this;
            formExchangeRate.Show();
        }

        private void btnWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("winword");
        }

        private void btnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("excel");
        }

        private void btnYoutube_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Tools.FormYoutube formYoutube = new Forms.Tools.FormYoutube();
            formYoutube.MdiParent = this;
            formYoutube.Show();
        }

        private void btnAgenda_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Tools.FormAgenda formNewNote = new Forms.Tools.FormAgenda();
            formNewNote.MdiParent = this;
            formNewNote.Show();
        }

        private void btnDefectiveProductList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Products.FormDefectiveProductList formDefectiveProductList = new Forms.Products.FormDefectiveProductList();
            formDefectiveProductList.MdiParent = this;
            formDefectiveProductList.Show();
        }

        private void btnNewProductSale_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Clients.FormNewProductSale formNewProductSale = new Forms.Clients.FormNewProductSale();
            //formNewProductSale.MdiParent = this;
            formNewProductSale.Show();
        }

        private void btnSalesList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Clients.FormSalesList formSalesList = new Forms.Clients.FormSalesList();
            formSalesList.MdiParent = this;
            formSalesList.Show();
        }

        private void btnNewDefectiveProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Products.FormNewDefectiveProduct newDefectiveProduct = new Forms.Products.FormNewDefectiveProduct();
            //newDefectiveProduct.MdiParent = this;
            newDefectiveProduct.Show();
        }

        private void btnDefectDescription_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Products.FormDefectDescription defectDescription = new Forms.Products.FormDefectDescription();
            //newDefectiveProduct.MdiParent = this;
            defectDescription.Show();
        }

        private void btnDefectiveProductDetails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Products.FormDefectiveProductDetails formDefectiveProductDetails = new Forms.Products.FormDefectiveProductDetails();
            formDefectiveProductDetails.MdiParent = this;
            formDefectiveProductDetails.Show();
        }

        private void btnCreateQRCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Products.FormCreateQRCode formCreateQRCode = new Forms.Products.FormCreateQRCode();
            //newDefectiveProduct.MdiParent = this;
            formCreateQRCode.Show();
        }

        private void btnNewInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnInvoiceList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Invoices.FormInvoiceList InvoiceList = new Forms.Invoices.FormInvoiceList();
            InvoiceList.MdiParent = this;
            InvoiceList.Show();
        }

        private void btnAddInvoiceItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Invoices.FormAddInvoiceItem addInvoiceItem = new Forms.Invoices.FormAddInvoiceItem();
            addInvoiceItem.MdiParent = this;
            addInvoiceItem.Show();
        }

        private void btnViewDetailedInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Invoices.FormViewDetailedInvoice formViewDetailedInvoice = new Forms.Invoices.FormViewDetailedInvoice();
            formViewDetailedInvoice.MdiParent = this;
            formViewDetailedInvoice.Show();
        }

        private void btnMainPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.MainPage.FormMainPage mainPage = new Forms.MainPage.FormMainPage();
            mainPage.MdiParent = this;
            mainPage.Show();
        }

        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Tools.FormAbout formAbout = new Forms.Tools.FormAbout();
            formAbout.MdiParent = this;
            formAbout.Show();
        }

        private void btnReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Reports.FormReports formReports = new Forms.Reports.FormReports();
            
            formReports.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Forms.MainPage.FormMainPage mainPage = new Forms.MainPage.FormMainPage();
            mainPage.MdiParent = this;
            mainPage.Show();
        }

        private void btnNewStaff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.Staff.FormNewStaff newStaff = new Forms.Staff.FormNewStaff();
            newStaff.Show();
        }
    }
}
