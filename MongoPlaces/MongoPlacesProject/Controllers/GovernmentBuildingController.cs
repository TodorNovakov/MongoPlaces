using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDBProjectLibrary;
using MongoDB.Driver;

namespace MongoPlacesProject.Controllers
{
    public class GovernmentBuildingController : Controller
    {
        private static Connection.DbConnection<GovernmentBuilding> db = new Connection.DbConnection<GovernmentBuilding>("places", "GovernmentBuilding");
        private PlaceDao<GovernmentBuilding> governBuildings = new PlaceDao<GovernmentBuilding>(db.Collection);
        //
        // GET: /GovenrmentBuilding/

        public ActionResult Index()
        {
            var result = governBuildings.GetAllPlaces();
            return View(result);
        }

        //
        // GET: /GovenrmentBuilding/Details/5

        public ActionResult Details(string id)
        {
            GovernmentBuilding gb = governBuildings.GetPlace(id);
            if (gb == null)
            {
                return HttpNotFound();
            }

            return View(gb);
        }

        //
        // GET: /GovenrmentBuilding/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GovenrmentBuilding/Create

        [HttpPost,ActionName("Create")]
        public ActionResult CreateConfirmed(GovernmentBuilding gb)
        {
            try
            {
                // TODO: Add insert logic here
                governBuildings.InsertPlace(gb);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GovenrmentBuilding/Edit/5

        public ActionResult Edit(string id)
        {
            GovernmentBuilding gb = governBuildings.GetPlace(id);
            if (gb == null)
            {
                return HttpNotFound();
            }
            return View(gb);
        }

        //
        // POST: /GovenrmentBuilding/Edit/5

        [HttpPost,ActionName("Edit")]
        public ActionResult EditConfirmed(GovernmentBuilding gb)
        {
            try
            {
                // TODO: Add update logic here
                governBuildings.UpdatePlace(gb);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GovenrmentBuilding/Delete/5

        public ActionResult Delete(string id)
        {
            GovernmentBuilding gb= governBuildings.GetPlace(id);
            if (gb == null)
            {
                return HttpNotFound();
            }
            return View(gb);
        }

        //
        // POST: /GovenrmentBuilding/Delete/5

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                governBuildings.DeletePlace(id);
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
                GovernmentBuilding p = governBuildings.GetPlace(id);
                var result = HelpFunctions.LocateHelper<GovernmentBuilding>.GetNearestFivePlaces(governBuildings, p);
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
                GovernmentBuilding p = governBuildings.GetPlace(id);
                var result = HelpFunctions.LocateHelper<GovernmentBuilding>.GetSameRatingPlaces(governBuildings, p);
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
            var result = governBuildings.GetAllPlaces().ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);

        } 
    }
}
