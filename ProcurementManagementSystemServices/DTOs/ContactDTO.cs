using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.DTOs
{
    public partial class ContactDTO
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
