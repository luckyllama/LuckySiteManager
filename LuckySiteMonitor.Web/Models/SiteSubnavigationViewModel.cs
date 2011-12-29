using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.Web.Models {
    public class SiteSubnavigationViewModel {
        public Site Site { get; set; }
        public SitePageType ActiveLink { get; set; }
        public bool Is(SitePageType type) {
            return ActiveLink == type;
        }
    }

    public enum SitePageType {
        Summary,
        Ping,
        Elmah
    }

}