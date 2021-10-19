using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class Site
    {
        public Site()
        {
            PurchaseRequisitions = new HashSet<PurchaseRequisition>();
            SiteBudgets = new HashSet<SiteBudget>();
            UserSites = new HashSet<UserSite>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public int? ContactId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual ICollection<PurchaseRequisition> PurchaseRequisitions { get; set; }
        public virtual ICollection<SiteBudget> SiteBudgets { get; set; }
        public virtual ICollection<UserSite> UserSites { get; set; }
    }
}
