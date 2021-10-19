using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class PurchaseRequisition
    {
        public PurchaseRequisition()
        {
            PurchaseRequisitionItems = new HashSet<PurchaseRequisitionItem>();
        }

        public int Id { get; set; }
        public int? SiteId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
        public sbyte? IsOpen { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<PurchaseRequisitionItem> PurchaseRequisitionItems { get; set; }
    }
}
