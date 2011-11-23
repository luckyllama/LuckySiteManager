using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuckySiteMonitor.DataAccess;

namespace LuckySiteMonitor.Web.Controllers {
    public class HomeController : Controller {

        private readonly SiteRepository _siteRepository;

        public HomeController() {
            _siteRepository = new SiteRepository();
        }

        public ActionResult Index() {
            var sites = _siteRepository.Get();
            if (!sites.Any()) {
                return RedirectToAction("Create", "Sites");
            }

            return View(sites);
        }

        public ActionResult About() {
            return View();
        }
    }
}
