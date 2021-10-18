using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class PurchaseRequisitionStatus
    {
        public PurchaseRequisitionStatus()
        {
            PurchaseRequisitionItems = new HashSet<PurchaseRequisitionItem>();
        }

        public int Status { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PurchaseRequisitionItem> PurchaseRequisitionItems { get; set; }
    }
}
