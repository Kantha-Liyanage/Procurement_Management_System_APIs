using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Depots = new HashSet<Depot>();
            Materials = new HashSet<Material>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? BillToAddressId { get; set; }
        public int? ContactId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Address BillToAddress { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual ICollection<Depot> Depots { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
