using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcurementManagementSystemServices.DTOs
{
    public class PurchaseRequisitionItemDTO
    {
        public int PurchaseRequisitionId { get; set; }
        public int ItemId { get; set; }
        public int? MaterialId { get; set; }
        public double? RequiredQuantity { get; set; }
        public double? ApprovedQuantity { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
        public int? Status { get; set; }
    }
}
