//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class productacceptance
    {
        public int acceptanceid { get; set; }
        public int customerid { get; set; }
        public int staffid { get; set; }
        public System.DateTime arrivaldate { get; set; }
        public System.DateTime departuredate { get; set; }
        public string productserialno { get; set; }
        public bool productstatus { get; set; }
        public string productstatusdetail { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual staff staff { get; set; }
    }
}
