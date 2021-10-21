using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class User
    {
        public User()
        {
            PurchaseRequisitionItems = new HashSet<PurchaseRequisitionItem>();
            PurchaseRequisitions = new HashSet<PurchaseRequisition>();
            SiteBudgets = new HashSet<SiteBudget>();
            UserSites = new HashSet<UserSite>();
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UserType { get; set; }

        public virtual UserType UserTypeNavigation { get; set; }
        public virtual ICollection<PurchaseRequisitionItem> PurchaseRequisitionItems { get; set; }
        public virtual ICollection<PurchaseRequisition> PurchaseRequisitions { get; set; }
        public virtual ICollection<SiteBudget> SiteBudgets { get; set; }
        public virtual ICollection<UserSite> UserSites { get; set; }
    }
}
