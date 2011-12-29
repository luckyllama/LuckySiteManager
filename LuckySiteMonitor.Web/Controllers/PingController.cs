using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using LuckySiteMonitor.DataAccess;

namespace LuckySiteMonitor.Web.Controllers {
    [RouteArea("sites")]
    public class PingController : Controller {

        private readonly SiteMonitorContext _context;

        public PingController() {
            _context = new SiteMonitorContext();
        }

        [GET("{id}/ping")]
        public ViewResult Index(int id) {
            var site = _context.Sites.Single(s => s.Id == id);
            return View(site);
        }
    }
}
