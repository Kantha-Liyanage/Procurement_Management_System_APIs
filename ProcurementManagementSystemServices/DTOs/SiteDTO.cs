using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.DTOs
{
    public partial class SiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public int? ContactId { get; set; }
        public AddressDTO Address { get; set; }
        public ContactDTO Contact { get; set; }
    }
}
