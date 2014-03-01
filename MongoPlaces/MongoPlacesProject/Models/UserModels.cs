using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MongoDBProjectLibrary;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace MongoPlaces.Models
{
    public class UserModels
    {
        public ObjectId id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<ObjectId> listOfFavouriteObjects { get; set; }

    }

    public class MongoDBEntities : DbContext
    {
        public MongoDBEntities() : base("name=MongoConnection") { }
      //  static MongoServer server = MongoServer.Create("mongodb://localhost");
      //  MongoDatabase database = server.GetDatabase("places");

        private static MongoClient client = new MongoClient("mongodb://localhost");
        private static MongoServer server = client.GetServer();
        private MongoDatabase database = server.GetDatabase("places");

        public void CreateUser(UserModels user)
        {
            var collection = database.GetCollection<UserModels>("users");
            //var findUser = collection.Find()
            collection.Insert<UserModels>(user);
        }

        public List<UserModels> GetAllUsers()
        {
            var collection = database.GetCollection<UserModels>("users");
            var result = collection.FindAll();
            //return (int) result.Count();
            List<UserModels> users = new List<UserModels>();
            foreach (var item in result)
            {
                //UserModels user = new UserModels{ id = item.id, email = item.id}
                users.Add(item);
            }
            return users;
        }

        public bool Exist(UserModels user)
        {
            var users = GetAllUsers();
            foreach (var item in users)
            {
                if (item.email == user.email && item.password == user.password)
                    return true;
            }
            return false;
        }

        public UserModels GetUser(string email)
        {
            var collection = database.GetCollection<UserModels>("users");
            var query = new QueryDocument();
            query.Set("email", email);
            var result = collection.FindOne(query);
            return result;
        }

        public void UpdateFavPlaces(string user, List<ObjectId> favouriteObjects)
        {
            var userObject = GetUser(user);
            userObject.listOfFavouriteObjects = favouriteObjects;
            var collection = database.GetCollection<UserModels>("users");
            //var query = new QueryDocument();
            //query.Set("listOfFavouriteObjects", userObject.listOfFavouriteObjects.ToBson());
            collection.Save(userObject);
        }

        public string GetType(string id) 
        {
            /*MongoClient client = new MongoClient("mongodb://localhost");
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("places");*/
            String[] names = new String[] { "Cinema", "Restaurant", "Pharmacy", "Hotel", "Hostel", "GovernmentBuilding" };
            foreach (string name in names)
            {
                MongoCollection collection = database.GetCollection(name);
                var queryFind = Query<Restaurant>.Where(r => r.Id == id);
                var res = collection.FindOneAs<Restaurant>(queryFind);
                if (res != null)
                    return name;
            }
            return null;
        }

  
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}