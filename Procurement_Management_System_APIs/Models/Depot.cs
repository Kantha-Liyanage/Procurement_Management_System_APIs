using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class Depot
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public int? ContactId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
