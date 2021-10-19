using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.DTOs
{
    public partial class DepotDTO
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public int? ContactId { get; set; }
    }
}
