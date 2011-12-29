using System;
using System.Linq;
using System.Web.Mvc;
using AttributeRouting;
using LuckySiteMonitor.DataAccess;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.Web.Controllers {
    [RouteArea("sites")]
    public class SitesController : Controller {

        private readonly SiteMonitorContext _context;

        public SitesController() {
            _context = new SiteMonitorContext();
        }

        [GET("")]
        public ViewResult Index() {
            var sites = _context.Sites.ToList();
            return View(sites);
        }

        [GET("{id}")]
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

        [GET("{id}/edit")]
        public ActionResult Edit(int id) {
            var site = _context.Sites.Single(s => s.Id == id);
            return View(site);
        }

        [PUT("{id}/edit")]
        public ActionResult Edit(Site site) {
            if (ModelState.IsValid) {
                site.ModifiedOn = DateTime.Now;
                _context.Entry(site).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", "Sites", new { site.Id });
            }
            return View(site);
        }

    }
}