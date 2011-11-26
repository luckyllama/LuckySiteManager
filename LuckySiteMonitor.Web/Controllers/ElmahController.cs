using System;
using System.Linq;
using System.Web.Mvc;
using LuckySiteMonitor.DataAccess;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.Web.Controllers {
    public class ElmahController : Controller {
        private readonly SiteMonitorContext _context;

        public ElmahController() {
            _context = new SiteMonitorContext();
        }

        public ActionResult Create(int siteId) {
            ViewBag.SiteId = siteId;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(int siteId, ElmahConfig elmah) {
            if (ModelState.IsValid) {
                var site = _context.Sites.Find(siteId);
                site.Elmah.Add(elmah);
                _context.SaveChanges();
                return PartialView("Details", elmah);
            }

            ViewBag.SiteId = siteId;
            return PartialView(elmah);
        }

        public ActionResult Edit(int id) {
            var elmah = _context.Elmah.Single(s => s.Id == id);
            return PartialView(elmah);
        }

        [HttpPost]
        public ActionResult Edit(ElmahConfig elmah) {
            if (ModelState.IsValid) {
                _context.Entry(elmah).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
                return PartialView("Details", elmah);
            }
            return PartialView(elmah);
        }

        public ActionResult Details(int id) {
            var elmah = _context.Elmah.Single(s => s.Id == id);
            return PartialView(elmah);
        }
    }
}