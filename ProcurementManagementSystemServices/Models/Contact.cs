using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class Contact
    {
        public Contact()
        {
            Depots = new HashSet<Depot>();
            Sites = new HashSet<Site>();
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Depot> Depots { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
