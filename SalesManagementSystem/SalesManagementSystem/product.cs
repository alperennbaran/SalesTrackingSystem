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
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.productacceptance = new HashSet<productacceptance>();
            this.productmovement = new HashSet<productmovement>();
            this.producttracking = new HashSet<producttracking>();
        }
    
        public int productid { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public decimal purchase { get; set; }
        public decimal sale { get; set; }
        public short stock { get; set; }
        public int categoryid { get; set; }
        public Nullable<bool> status { get; set; }
    
        public virtual category category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productacceptance> productacceptance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productmovement> productmovement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<producttracking> producttracking { get; set; }
    }
}