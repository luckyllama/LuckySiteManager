using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using Elmah;
using LuckySiteMonitor.DataAccess;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.Web.Controllers {
    [RouteArea("sites")]
    public class ElmahController : Controller {
        private readonly SiteMonitorContext _context;

        public ElmahController() {
            _context = new SiteMonitorContext();
        }

        [GET("{id}/elmah")]
        public ViewResult Index(int id) {
            var site = _context.Sites.Single(s => s.Id == id);
            return View(site);
        }

        [GET("{siteId}/elmah/create")]
        public ActionResult Create(int siteId) {
            ViewBag.SiteId = siteId;
            return View();
        }

        [POST("{siteId}/elmah/create")]
        public ActionResult Create(int siteId, ElmahConfig elmah) {
            ViewBag.SiteId = siteId;
            // todo: validate connection string
            if (ModelState.IsValid) {
                var site = _context.Sites.Find(siteId);
                site.Elmah.Add(elmah);
                _context.SaveChanges();
                return RedirectToAction("Index", "Elmah", new { id = siteId });
            }

            return View(elmah);
        }

        [GET("{siteId}/elmah/edit/{id}")]
        public ActionResult Edit(int siteId, int id) {
            var config = _context.Elmah.Single(s => s.Id == id);
            ViewBag.SiteId = siteId;
            return View(config);
        }

        [PUT("{siteId}/elmah/edit/{id}")]
        public ActionResult Edit(ElmahConfig elmah, int siteId) {
            ViewBag.SiteId = siteId;
            // todo: validate connection string
            if (ModelState.IsValid) {
                _context.Entry(elmah).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Elmah", new { id = siteId });
            }
            return View(elmah);
        }

        // todo: delete

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

#if DEBUG
        #region Insert Rows For Testing

        public const string Insert = @"insert into ELMAH_error (Application, Host, Type, Source, Message, [User], StatusCode, TimeUtc, AllXml)
                                           values ('/LM/W3SVC/11/ROOT', 'FRANK-PC', @type, @source, @message, '', @code, @time, @xml)";

        public ActionResult InsertRandomTestData() {
            for (int pastDayAmount = 0; pastDayAmount < 60; pastDayAmount++) {
                Random rand = new Random();

                for (int amountPerDay = rand.Next(25); amountPerDay > 0; amountPerDay--) {

                    var pastDay = DateTime.UtcNow.AddDays(-pastDayAmount);
                    var error = new Error();
                    var time = new DateTime(pastDay.Year, pastDay.Month, pastDay.Day, rand.Next(24), rand.Next(60), rand.Next(60));
                    if (rand.Next(4)%3 == 0) {
                        var pageName = "";
                        var randomPageName = rand.Next(4);
                        switch (randomPageName) {
                            case 0:
                                pageName = "/Zero.aspx";
                                break;
                            case 1:
                                pageName = "/IndexOne.aspx";
                                break;
                            case 2:
                                pageName = "/TestPageTwo.aspx";
                                break;
                            case 3:
                                pageName = "/the-third-404.css";
                                break;
                            default:
                                pageName = "/default.aspx";
                                break;
                        }
                        error = new Error(
                            new HttpException(404, "The controller for path '/" + pageName + "' was not found or does not implement IController."),
                            System.Web.HttpContext.Current) { Time = time };
                    } else {
                        var randomException = rand.Next(10);
                        switch (randomException) {
                            case 0:
                                error = new Error(
                                    new ArgumentNullException("param"),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 1:
                                error = new Error(
                                    new System.Data.ConstraintException("The 'CreatedOn' property on 'Table' could not be set to a 'null' value. You must set this property to a non-null value of type 'DateTime'."),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 2:
                                error = new Error(
                                    new ArgumentException("The parameters dictionary contains a null entry for parameter 'Something' of non-nullable type 'System.Boolean' for method 'System.Web.Mvc.ActionResult Edit(LuckySiteMonitor.Entities.Test, Boolean)' in 'LuckySiteMonitor.Web.Controllers.Test'. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter. Parameter name: parameters"),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 3:
                                error = new Error(
                                    new NullReferenceException("Object reference not set to an instance of an object."),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 4:
                                error = new Error(
                                    new InvalidOperationException("The 'CreatedOn' property on 'Table' could not be set to a 'null' value. You must set this property to a non-null value of type 'DateTime'."),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 5:
                                error = new Error(
                                    new HttpCompileException("The partial view 'Test/Action' was not found or no view engine supports the searched locations. The following locations were searched: ~/Views/Test/Action.aspx ~/Views/Test/Action.ascx"),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 6:
                                error = new Error(
                                    new NullReferenceException("The specified table does not exist. [ Tests ]."),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 7:
                                error = new Error(
                                    new ArgumentException("This is an argument exception."),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 8:
                                error = new Error(
                                    new ArgumentNullException("param2"),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            case 9:
                                error = new Error(
                                    new HttpCompileException("The partial view 'Test/Action2' was not found or no view engine supports the searched locations. The following locations were searched: ~/Views/Test/Action2.aspx ~/Views/Test/Action2.ascx"),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                            default:
                                error = new Error(
                                    new Exception("If it's not another exception, it's this one. "),
                                    System.Web.HttpContext.Current) { Time = time };
                                break;
                        }
                    }

                    ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(error);
                }
            }
            return Content("Success");
        }

        #endregion Insert Rows For Testing
#endif

    }
}