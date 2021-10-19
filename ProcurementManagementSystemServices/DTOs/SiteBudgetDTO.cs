using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.DTOs
{
    public partial class SiteBudgetDTO
    {
        public int? SiteId { get; set; }
        public int? MaterialCategoryId { get; set; }
        public double? Amount { get; set; }
        public string Supervisor { get; set; }
    }
}
