using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuckySiteMonitor.Entities {
    public class Site {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
