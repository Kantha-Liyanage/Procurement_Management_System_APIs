using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class Supervisor
    {
        public string Username { get; set; }
        public int BudgetId { get; set; }

        public virtual SiteBudget Budget { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
