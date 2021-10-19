using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class UserSite
    {
        public string Username { get; set; }
        public int Site { get; set; }

        public virtual Site SiteNavigation { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
