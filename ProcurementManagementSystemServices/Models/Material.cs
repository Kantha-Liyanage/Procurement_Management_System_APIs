using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class Material
    {
        public Material()
        {
            PurchaseRequisitionItems = new HashSet<PurchaseRequisitionItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public double? PriceUnit { get; set; }
        public double? UnitPrice { get; set; }
        public double? LeadTimeDays { get; set; }

        public virtual MaterialCategory Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
        public virtual ICollection<PurchaseRequisitionItem> PurchaseRequisitionItems { get; set; }
    }
}
