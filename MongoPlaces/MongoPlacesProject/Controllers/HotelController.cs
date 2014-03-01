using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDBProjectLibrary;
using MongoDB.Driver;

namespace MongoPlacesProject.Controllers
{
    public class HotelController : Controller
    {
        private static Connection.DbConnection<Hotel> db = new Connection.DbConnection<Hotel>("places", "Hotel");
        private PlaceDao<Hotel> hotels = new PlaceDao<Hotel>(db.Collection);
        //
        // GET: /Hotel/

        public ActionResult Index()
        {
            var result = hotels.GetAllPlaces();
            return View(result);
        }

        //
        // GET: /Hotel/Details/5

        public ActionResult Details(string id)
        {
            Hotel hot = hotels.GetPlace(id);
            if (hot == null)
            {
                return HttpNotFound();
            }
            return View(hot);
        }

        //
        // GET: /Hotel/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Hotel/Create

        [HttpPost]
        public ActionResult Create(Hotel hot)
        {
            try
            {
                // TODO: Add insert logic here
                hotels.InsertPlace(hot);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hotel/Edit/5

        public ActionResult Edit(string id)
        {
            Hotel hot = hotels.GetPlace(id);
            if (hot == null)
            {
                return HttpNotFound();
            }
            return View(hot);
        }

        //
        // POST: /Hotel/Edit/5

        [HttpPost,ActionName("Edit")]
        public ActionResult EditConfirmed(Hotel hot)
        {
            try
            {
                // TODO: Add update logic here
                hotels.UpdatePlace(hot);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hotel/Delete/5

        public ActionResult Delete(string id)
        {
            Hotel result = hotels.GetPlace(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // POST: /Hotel/Delete/5

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                hotels.DeletePlace(id);
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
                // TODO: Ahotels.GetPlace(id);
                Hotel p = hotels.GetPlace(id);

                var result = HelpFunctions.LocateHelper<Hotel>.GetNearestFivePlaces(hotels, p);
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
                Hotel p = hotels.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Hotel>.GetSameRatingPlaces(hotels, p);
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
            var result = hotels.GetAllPlaces().ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);

        } 
    }
}
