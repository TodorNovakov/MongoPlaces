using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDBProjectLibrary;
using MongoPlacesProject.HelpFunctions;
using Newtonsoft.Json.Linq;


namespace MongoPlacesProject.Controllers
{
    public class RestaurantController : Controller
    {
        private  static Connection.DbConnection<Restaurant> db = new Connection.DbConnection<Restaurant>("places","Restaurant");
        private  PlaceDao<Restaurant> restaurants=new PlaceDao<Restaurant>(db.Collection);
        //
        // GET: /Restaurant/

        public ActionResult Index()
        {
            var result = restaurants.GetAllPlaces();
            return View(result);
        }

        //
        // GET: /Restaurant/Details/5

        public ActionResult Details(string id)
        {
            Restaurant res = restaurants.GetPlace(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        //
        // GET: /Restaurant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Restaurant/Create

        [HttpPost]
        public ActionResult Create(Restaurant res)
        {
            try
            {
                // TODO: Add insert logic here
                restaurants.InsertPlace(res);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Restaurant/Edit/5

        public ActionResult Edit(string id)
        {
            Restaurant res = restaurants.GetPlace(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        //
        // POST: /Restaurant/Edit/5

        [HttpPost,ActionName("Edit")]
        public ActionResult EditConfirmed(Restaurant res)
        {
            try
            {
                restaurants.UpdatePlace(res);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Restaurant/Delete/5

        public ActionResult Delete(string id)
        {
            Restaurant res = restaurants.GetPlace(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        //
        // POST: /Restaurant/Delete/5

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                restaurants.DeletePlace(id);
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
                Restaurant p = restaurants.GetPlace(id);
                var result=HelpFunctions.LocateHelper<Restaurant>.GetNearestFivePlaces(restaurants, p);
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
                Restaurant p = restaurants.GetPlace(id);
                var result = HelpFunctions.LocateHelper<Restaurant>.GetSameRatingPlaces(restaurants, p);
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
            var result = restaurants.GetAllPlaces().ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);
   
        }     

    }
}
