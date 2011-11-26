using System;
using System.Linq;
using System.Web.Mvc;
using LuckySiteMonitor.DataAccess;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.Web.Controllers {
    public class SitesController : Controller {

        private readonly SiteMonitorContext _context;

        public SitesController() {
            _context = new SiteMonitorContext();
        }

        public ViewResult Index() {
            var sites = _context.Sites.ToList();
            return View(sites);
        }

        public ViewResult Details(int id) {
            var site = _context.Sites.Single(s => s.Id == id);
            return View(site);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Site site) {
            if (ModelState.IsValid) {
                site.CreatedOn = DateTime.Now;
                _context.Sites.Add(site);
                _context.SaveChanges();
                return RedirectToAction("Details", new { site.Id });
            }

            return View(site);
        }

        public ActionResult Edit(int id) {
            var site = _context.Sites.Single(s => s.Id == id);
            return View(site);
        }

        [HttpPost]
        public ActionResult Edit(Site site) {
            if (ModelState.IsValid) {
                site.ModifiedOn = DateTime.Now;
                _context.Entry(site).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { site.Id });
            }
            return View(site);
        }

    }
}