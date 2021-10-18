using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class PurchaseRequisitionItem
    {
        public int PurchaseRequisitionId { get; set; }
        public int ItemId { get; set; }
        public int? MaterialId { get; set; }
        public double? Quantity { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
        public int? Status { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Material Material { get; set; }
        public virtual PurchaseRequisition PurchaseRequisition { get; set; }
        public virtual PurchaseRequisitionStatus StatusNavigation { get; set; }
    }
}
