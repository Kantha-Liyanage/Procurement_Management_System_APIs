using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class UnitOfMeasure
    {
        public UnitOfMeasure()
        {
            Materials = new HashSet<Material>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
