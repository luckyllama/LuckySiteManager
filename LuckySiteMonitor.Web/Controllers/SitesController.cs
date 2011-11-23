using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuckySiteMonitor.Entities;
using LuckySiteMonitor.Web.Models;

namespace LuckySiteMonitor.Web.Controllers
{   
    public class SitesController : Controller
    {
        private LuckySiteMonitorWebContext context = new LuckySiteMonitorWebContext();

        //
        // GET: /Sites/

        public ViewResult Index()
        {
            return View(context.Sites.ToList());
        }

        //
        // GET: /Sites/Details/5

        public ViewResult Details(int id)
        {
            Site site = context.Sites.Single(x => x.Id == id);
            return View(site);
        }

        //
        // GET: /Sites/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Sites/Create

        [HttpPost]
        public ActionResult Create(Site site)
        {
            if (ModelState.IsValid)
            {
                context.Sites.Add(site);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(site);
        }
        
        //
        // GET: /Sites/Edit/5
 
        public ActionResult Edit(int id)
        {
            Site site = context.Sites.Single(x => x.Id == id);
            return View(site);
        }

        //
        // POST: /Sites/Edit/5

        [HttpPost]
        public ActionResult Edit(Site site)
        {
            if (ModelState.IsValid)
            {
                context.Entry(site).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        //
        // GET: /Sites/Delete/5
 
        public ActionResult Delete(int id)
        {
            Site site = context.Sites.Single(x => x.Id == id);
            return View(site);
        }

        //
        // POST: /Sites/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Site site = context.Sites.Single(x => x.Id == id);
            context.Sites.Remove(site);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}