using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class MaterialCategory
    {
        public MaterialCategory()
        {
            Materials = new HashSet<Material>();
            SiteBudgets = new HashSet<SiteBudget>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<SiteBudget> SiteBudgets { get; set; }
    }
}
