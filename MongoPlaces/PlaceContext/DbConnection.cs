using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MongoDBProjectLibrary;
using MongoDB.Driver;

namespace Connection
{
    public class DbConnection<T> where T:Place
    {
        private static MongoClient client = new MongoClient("mongodb://localhost");
        private static MongoServer server = client.GetServer();
        private   MongoDatabase database;
        private   MongoCollection<T> collection;

        public DbConnection(string databaseName,string collectionName)
        {
            database = server.GetDatabase(databaseName);
            collection = database.GetCollection<T>(collectionName);
        }

        public  MongoCollection<T> Collection
        {
            get { return this.collection; }
        }



    }
}
