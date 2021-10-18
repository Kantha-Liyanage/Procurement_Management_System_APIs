using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class Address
    {
        public Address()
        {
            Depots = new HashSet<Depot>();
            Sites = new HashSet<Site>();
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual ICollection<Depot> Depots { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
