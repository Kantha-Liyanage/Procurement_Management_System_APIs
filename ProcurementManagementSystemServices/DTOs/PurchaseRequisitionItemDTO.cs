using System;

namespace ProcurementManagementSystemServices.DTOs
{
    public class PurchaseRequisitionItemDTO
    {
        public int PurchaseRequisitionId { get; set; }
        public int ItemId { get; set; }
        public int? MaterialId { get; set; }
        public double RequiredQuantity { get; set; }
        public double ApprovedQuantity { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Remarks { get; set; }
        public string? Status { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialCategory { get; set; }
        public string? Uom { get; set; }
        public double? PriceUnit { get; set; }
        public double? UnitPrice { get; set; }
        public double? SubTotal { get; set; }
        public string SupplierName { get; set; }
        public double LeadTimeDays { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
    }
}
