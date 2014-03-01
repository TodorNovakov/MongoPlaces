using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDBProjectLibrary;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace MongoPlacesProject.HelpFunctions
{
    public class LocateHelper<T> where T:Place
    {
        public static double DistancePoints(Coordinates c1,Coordinates c2)
        {
            double latitudeDiffer=Math.Abs(c1.Latitude-c2.Latitude);
            double longitudeDiffer=Math.Abs(c1.Longitude-c2.Longitude);
            double result=Math.Sqrt(Math.Pow(latitudeDiffer,2)+Math.Pow(longitudeDiffer,2));

            return result;
        }

        public static List<T> GetSameRatingPlaces(PlaceDao<T> collection, T p)
        {
            var result = from place in collection.GetAllPlaces()
                         where place.Rating == p.Rating && place.Id!=p.Id
                         select place;

            return result.ToList<T>();
        }

        
        public static List<T> GetNearestFivePlaces(PlaceDao<T> collection, T p)
        {
            var places = (from place in collection.GetAllPlaces()
                          where place.Id != p.Id
                          select place).ToList();
            Dictionary<double, T> placesDistance = new Dictionary<double, T>();
            for (int i = 0; i < places.Count; i++)
            {
                double differ = DistancePoints(p.CoordinatesPlace, places[i].CoordinatesPlace);
                placesDistance.Add(differ, places[i]);
            }
            IEnumerable<T> result;
            if (placesDistance.Count < 5)
            {
                result = placesDistance.Values;
            }
            else
            {
                placesDistance.OrderBy(i => i.Key);
                result = placesDistance.Values.Take(5);
            }

            return result.ToList<T>();
        }

        public string GetType(string id)
        {
             MongoClient client = new MongoClient("mongodb://localhost");
             MongoServer server = client.GetServer();
             MongoDatabase database = server.GetDatabase("places");
             String[] names = new String[] { "Cinema", "Restaurant", "Pharmacy", "Hotel", "Hostel", "GovernmentBuilding" };
             foreach (string name in names)
             {
                 MongoCollection collection = database.GetCollection(name);
                 var queryFind = Query<Place>.Where(r => r.Id == id);
                 Place res = collection.FindOneAs<Place>(queryFind);
                 if (res != null)
                     return name;
             }
             return null;
        }
    }
}