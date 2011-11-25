using System.Web.Mvc;
using LuckySiteMonitor.DataAccess;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.Web.Controllers {
    public class SitesController : Controller {

        private readonly SiteRepository _siteRepository;

        public SitesController() {
            _siteRepository = new SiteRepository();
        }

        public ViewResult Index() {
            var sites = _siteRepository.Get();
            return View(sites);
        }

        public ViewResult Details(int id) {
            Site site = _siteRepository.Get(id);
            return View(site);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Site site) {
            if (ModelState.IsValid) {
                var id = _siteRepository.Insert(site);
                return RedirectToAction("Details", new { id });
            }

            return View(site);
        }

        public ActionResult Edit(int id) {
            Site site = _siteRepository.Get(id);
            return View(site);
        }

        [HttpPost]
        public ActionResult Edit(Site site) {
            if (ModelState.IsValid) {
                _siteRepository.Update(site);
                return RedirectToAction("Details", new { Id = site.Id });
            }
            return View(site);
        }

    }
}