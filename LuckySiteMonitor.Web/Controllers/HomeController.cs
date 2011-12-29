using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using LuckySiteMonitor.DataAccess;

namespace LuckySiteMonitor.Web.Controllers {
    public class HomeController : Controller {
        
        private readonly SiteMonitorContext _context;

        public HomeController() {
            _context = new SiteMonitorContext();
        }

        [GET("")]
        public ActionResult Index() {
            var sites = _context.Sites.ToList();
            if (!sites.Any()) {
                return RedirectToAction("Create", "Sites");
            }

            return View(sites);
        }

        [GET("about")]
        public ActionResult About() {
            return View();
        }
    }
}
