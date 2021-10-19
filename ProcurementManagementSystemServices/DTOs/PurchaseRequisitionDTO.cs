using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcurementManagementSystemServices.DTOs
{
    public class PurchaseRequisitionDTO
    {
        public PurchaseRequisitionDTO()
        {
            this.items = new List<PurchaseRequisitionItemDTO>();
        }

        public int Id { get; set; }
        public int? SiteId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
        public List<PurchaseRequisitionItemDTO> items;
    }
}
