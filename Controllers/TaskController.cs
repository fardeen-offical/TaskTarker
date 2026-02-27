using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskTracker.Models;
using System.Data.Entity; // Ye line lazmi honi chahiye
using TaskTracker.Filters;

namespace TaskTracker.Controllers
{
    [JwtAuthorize]
    public class TaskController : Controller
    {
        // 1. Database Connection Object
        private TaskDbContext db = new TaskDbContext();

        // 2. Index: SQL se saare tasks mangwana
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        // 3. Create (GET)
        public ActionResult Create()
        {
            return View();
        }

        // 4. Create (POST): Database mein task save karna
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges(); // Ye line SQL mein data insert karegi
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // 5. Edit (GET): Database se ID ke zariye task dhundna
        public ActionResult Edit(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null) return HttpNotFound();
            return View(task);
        }

        // 6. Edit (POST): Changes SQL mein update karna
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // 7. Delete (GET)
        public ActionResult Delete(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null) return HttpNotFound();
            return View(task);
        }

        // 8. Delete (POST): SQL se record khatam karna
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Database connection close karna (Memory bachanay ke liye)
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // Details: Kisi specific task ki poori maloomat dikhane ke liye
        public ActionResult Details(int id)
        {
            // Database se ID ke zariye task dhoondna
            var task = db.Tasks.Find(id);

            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);
        }
    }
}