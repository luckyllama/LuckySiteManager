
using System.ComponentModel.DataAnnotations;

namespace LuckySiteMonitor.Entities {
    public class ElmahConfig {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Connection String")]
        public string ConnectionString { get; set; }
        [Display(Name = "Application Filter")]
        public string ApplicationFilter { get; set; }
    }
}
