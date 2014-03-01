using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace MongoDBProjectLibrary
{
    public class PlaceDao<T> : IPlaceDao<T> where T : Place
    {
        private MongoCollection<T> placeCollection;

        public PlaceDao(MongoCollection<T> places)
        {
            this.placeCollection = places;
        }

        public /*ObjectId*/ string GetPlaceId(string name)
        {
            var queryFind = Query<T>.Where(r => r.Name == name);
            T res = this.placeCollection.FindOneAs<T>(queryFind);
            return res.Id;
        }

        public T GetPlace(/*ObjectId*/ string id)
        {
            var queryFind = Query<T>.Where(r => r.Id == id);
            T res = this.placeCollection.FindOneAs<T>(queryFind);
            return res;
        }

        public void InsertPlace(T r)
        {
            this.placeCollection.Insert<T>(r);
        }

        public void DeletePlace(/*ObjectId*/ string id)
        {
            var queryRes = Query<T>.Where(p => p.Id == id);
            this.placeCollection.Remove(queryRes);
        }

        public void UpdatePlace(T place)
        {
           /* var queryRes = Query<T>.Where(p =>p.Id==id);
            var update = Update<T>.Replace(place);
            this.placeCollection.Update(queryRes, update);*/

            /*IMongoQuery query = Query.EQ("_id", place.Id);
            IMongoUpdate update = MongoDB.Driver.Builders.Update.Set
            ("Name", place.Name);
            this.placeCollection.Update(query, update);*/

            this.placeCollection.Save<T>(place);

        }


        public List<T> GetAllPlaces()
        {
            var cursor = this.placeCollection.FindAll();
            var result = new List<T>();
            foreach (var elem in cursor)
            {
                result.Add(elem);
            }
            return result;
        }

    }
}
