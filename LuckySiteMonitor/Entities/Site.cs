using System;
using System.Collections.Generic;

namespace LuckySiteMonitor.Entities {
    public class Site {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ElmahConfig> Elmah { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
