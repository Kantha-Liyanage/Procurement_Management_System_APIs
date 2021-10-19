using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.DTOs
{
    public partial class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BillToAddressId { get; set; }
        public int? ContactId { get; set; }
        public bool? IsActive { get; set; }
    }
}
