using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDBProjectLibrary;
using MongoDB.Driver;

namespace MongoPlacesProject.Controllers
{
    public class HostelController : Controller
    {
        private static Connection.DbConnection<Hostel> db = new Connection.DbConnection<Hostel>("places", "Hostel");
        private PlaceDao<Hostel> hostels = new PlaceDao<Hostel>(db.Collection);
        //
        // GET: /Hostel/

        public ActionResult Index()
        {
            var result = hostels.GetAllPlaces();
            return View(result);
        }

        //
        // GET: /Hostel/Details/5

        public ActionResult Details(string id)
        {
            Hostel hs = hostels.GetPlace(id);
            if (hs == null)
            {
                return HttpNotFound();
            }

            return View(hs);
        }

        //
        // GET: /Hostel/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Hostel/Create

        [HttpPost]
        public ActionResult Create(Hostel hs)
        {
            try
            {
                // TODO: Add insert logic here
                hostels.InsertPlace(hs);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hostel/Edit/5

        public ActionResult Edit(string id)
        {
            Hostel hs = hostels.GetPlace(id);
            if (hs == null)
            {
                return HttpNotFound();
            }
            return View(hs);
        }

        //
        // POST: /Hostel/Edit/5

        [HttpPost,ActionName("Edit")]
        public ActionResult EditConfirmed(Hostel hs)
        {
            try
            {
                // TODO: Add update logic here
                hostels.UpdatePlace(hs);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hostel/Delete/5

        public ActionResult Delete(string id)
        {
            Hostel hs = hostels.GetPlace(id);
            if (hs == null)
            {
                return HttpNotFound();
            }
            return View(hs);
        }

        //
        // POST: /Hostel/Delete/5

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                hostels.DeletePlace(id);
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
                Hostel p = hostels.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Hostel>.GetNearestFivePlaces(hostels, p);
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
                Hostel p = hostels.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Hostel>.GetSameRatingPlaces(hostels, p);
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
            var result = hostels.GetAllPlaces().ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);

        } 
    }
}
