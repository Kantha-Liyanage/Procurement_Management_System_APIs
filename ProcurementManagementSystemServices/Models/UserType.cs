using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class UserType
    {
        public UserType()
        {
            SiteBudgets = new HashSet<SiteBudget>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SiteBudget> SiteBudgets { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
