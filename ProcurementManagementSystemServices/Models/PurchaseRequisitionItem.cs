using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class PurchaseRequisitionItem
    {
        public int PurchaseRequisitionId { get; set; }
        public int ItemId { get; set; }
        public int? MaterialId { get; set; }
        public double? RequiredQuantity { get; set; }
        public double? ApprovedQuantity { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Remarks { get; set; }
        public int? Status { get; set; }

        public virtual Material Material { get; set; }
        public virtual PurchaseRequisition PurchaseRequisition { get; set; }
        public virtual PurchaseRequisitionStatus StatusNavigation { get; set; }
    }
}
