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
    public interface IPlaceDao<T> where T:Place 
    {
        List<T> GetAllPlaces();
        /*ObjectId*/ string GetPlaceId(string name);
        T GetPlace(/*ObjectId*/ string id);
        void InsertPlace(T place);
        void DeletePlace(/*ObjectId*/ string id);
        void UpdatePlace(T place);
    }
}
