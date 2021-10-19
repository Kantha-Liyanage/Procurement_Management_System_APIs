using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.DTOs
{
    public partial class MaterialDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public double? PriceUnit { get; set; }
        public double? UnitPrice { get; set; }
        public double? LeadTimeDays { get; set; }
    }
}
