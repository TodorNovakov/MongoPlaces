using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDBProjectLibrary;
using MongoDB.Driver;
namespace MongoPlacesProject.Controllers
{
    public class CinemaController : Controller
    {
        private static Connection.DbConnection<Cinema> db = new Connection.DbConnection<Cinema>("places", "Cinema");
        private PlaceDao<Cinema> cinemas = new PlaceDao<Cinema>(db.Collection);
        //
        // GET: /Cinema/

        public ActionResult Index()
        {
            var result = cinemas.GetAllPlaces();
            return View(result);
        }

        //
        // GET: /Cinema/Details/5

        public ActionResult Details(string id)
        {
            Cinema res = cinemas.GetPlace(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        //
        // GET: /Cinema/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cinema/Create

        [HttpPost]
        public ActionResult Create(Cinema res)
        {
            try
            {
                // TODO: Add insert logic here

                cinemas.InsertPlace(res);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cinema/Edit/5

        public ActionResult Edit(string id)
        {
            Cinema res = cinemas.GetPlace(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        //
        // POST: /Cinema/Edit/5

        [HttpPost,ActionName("Edit")]
        public ActionResult EditConfirmed(Cinema res)
        {
            try
            {
                // TODO: Add update logic here
                cinemas.UpdatePlace(res);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cinema/Delete/5

        public ActionResult Delete(string id)
        {
            Cinema res = cinemas.GetPlace(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        //
        // POST: /Cinema/Delete/5

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here

                cinemas.DeletePlace(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Nearest5Places(string id)
        {
            try
            {
                // TODO: Add delete logic here
                Cinema p = cinemas.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Cinema>.GetNearestFivePlaces(cinemas, p);
                return View(result);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SameRatingPlaces(string id)
        {
            try
            {
                // TODO: Add delete logic here
                Cinema p = cinemas.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Cinema>.GetSameRatingPlaces(cinemas, p);
                return View(result);
            }
            catch
            {
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetPlacesJson()
        {
            var result = cinemas.GetAllPlaces().ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}
