using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDBProjectLibrary;

namespace MongoPlacesProject.Controllers
{
    public class PharmacyController : Controller
    {
        private static Connection.DbConnection<Pharmacy> db = new Connection.DbConnection<Pharmacy>("places", "Pharmacy");
        private PlaceDao<Pharmacy> pharmacies = new PlaceDao<Pharmacy>(db.Collection);
        //
        // GET: /Pharmacy/

        public ActionResult Index()
        {
            var result = pharmacies.GetAllPlaces();
            return View(result);
        }

        //
        // GET: /Pharmacy/Details/5

        public ActionResult Details(string id)
        {
            Pharmacy result = pharmacies.GetPlace(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // GET: /Pharmacy/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pharmacy/Create

        [HttpPost]
        public ActionResult Create(Pharmacy pharm)
        {
            try
            {
                // TODO: Add insert logic here
                pharmacies.InsertPlace(pharm);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Pharmacy/Edit/5

        public ActionResult Edit(string id)
        {
            Pharmacy pharm = pharmacies.GetPlace(id);
            if (pharm == null)
            {
                return HttpNotFound();
            }
            return View(pharm);
        }

        //
        // POST: /Pharmacy/Edit/5

        [HttpPost,ActionName("Edit")]
        public ActionResult EditConfirmed(Pharmacy pharm)
        {
            try
            {
                // TODO: Add update logic here
                pharmacies.UpdatePlace(pharm);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Pharmacy/Delete/5

        public ActionResult Delete(string id)
        {
            Pharmacy pharm = pharmacies.GetPlace(id);
            if (pharm == null)
            {
                return HttpNotFound();
            }
            return View(pharm);
        }

        //
        // POST: /Pharmacy/Delete/5

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                pharmacies.DeletePlace(id);
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
                Pharmacy p = pharmacies.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Pharmacy>.GetNearestFivePlaces(pharmacies, p);
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
                Pharmacy p = pharmacies.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Pharmacy>.GetSameRatingPlaces(pharmacies, p);
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
            var result = pharmacies.GetAllPlaces().ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);

        } 
    }
}
