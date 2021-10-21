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
        public string Status { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }

        public virtual User ApprovedByNavigation { get; set; }
        public virtual Material Material { get; set; }
        public virtual PurchaseRequisition PurchaseRequisition { get; set; }
    }
}
