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
        public ActionResult Create(int siteId, ElmahConfig elmah, bool? isTest) {
            ViewBag.SiteId = siteId;

            if (ModelState.IsValid) {
                if (isTest.HasValue && isTest.Value) {
                    ViewBag.TestResult = Test(elmah);
                    return PartialView(elmah);
                }

                var site = _context.Sites.Find(siteId);
                site.Elmah.Add(elmah);
                _context.SaveChanges();
                return PartialView("Details", elmah);
            }

            return PartialView(elmah);
        }

        public ActionResult Edit(int id) {
            var elmah = _context.Elmah.Single(s => s.Id == id);
            return PartialView(elmah);
        }

        [HttpPost]
        public ActionResult Edit(ElmahConfig elmah, bool? isTest) {
            if (ModelState.IsValid) {
                if (isTest.HasValue && isTest.Value) {
                    ViewBag.TestResult = Test(elmah);
                    return PartialView(elmah);
                }

                _context.Entry(elmah).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
                return PartialView("Details", elmah);
            }
            return PartialView(elmah);
        }

        private static string Test(ElmahConfig elmah) {
            ElmahRepository repository = new ElmahRepository(elmah);
            var builder = new TagBuilder("span");
            try {
                var result = repository.Test();
                builder.MergeAttribute("class", result.ToString());
                switch (result) {
                    case ElmahRepository.TestResults.Success:
                        builder.InnerHtml = "Connected successfully!";
                        break;
                    case ElmahRepository.TestResults.Error:
                        builder.InnerHtml = "Could not connect. Please check your connection string and try again.";
                        break;
                    case ElmahRepository.TestResults.NoResults:
                        builder.InnerHtml = "No rows in the table. The application name is possibly incorrect.";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return builder.ToString();
            } catch {
                builder.MergeAttribute("class", "error");
                builder.InnerHtml = "An unexpected error occured. Please check your connection string and try again.";
                return builder.ToString();
            }
        }

        public ActionResult Details(int id) {
            var elmah = _context.Elmah.Single(s => s.Id == id);
            return PartialView(elmah);
        }
    }
}