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
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MongoDBProjectLibrary
{
    public abstract class Place
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
       // private ObjectId id;
        private Coordinates coordinatesPlace;
        private string name;
        private string placeType;
        private int likes;
        private double? rating = null;
        private string description;
        private string workingTime;

        /*public ObjectId Id
        {
            get;
            set;
        }*/

        public Coordinates CoordinatesPlace
        {
            get
            {
                Coordinates result = new Coordinates(this.coordinatesPlace);
                return result;
            }

            set
            {
                if (value != null)
                {
                    this.coordinatesPlace = new Coordinates(value);
                }
                else
                {
                    this.coordinatesPlace = new Coordinates();
                }
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != null)
                {
                    this.name = value;
                }
                else
                {
                    this.name = "";
                }
            }
        }

        public string PlaceType
        {
            get { return this.placeType; }
            set
            {
                if (value != null)
                {
                    this.placeType = value;
                }
                else
                {
                    this.placeType = "";
                }
            }
        }

        public int Likes
        {
            get { return this.likes; }
            set
            {
                if (value >= 0)
                {
                    this.likes = value;
                }
                else
                {
                    this.likes = 0;
                }
            }
        }

        public double Rating
        {
            get
            {
                if (this.IsRatingSet)
                {
                    return this.rating.Value;
                }
                else
                {
                    return 0.0;
                }
            }
            set
            {
                if (value >= 0.0)
                {
                    this.rating = value;
                }
                else
                {
                    this.rating = null;
                }
            }
        }

        public bool IsRatingSet
        {
            get { return this.rating.HasValue; }
        }

        public string Description
        {
            get
            { return this.description; }
            set
            {
                if (value != null)
                {
                    this.description = value;
                }
                else
                {
                    this.description = null;
                }
            }
        }

        public string WorkingTime
        {
            get
            {
                return this.workingTime;
            }
            set
            {
                if (value != null)
                {
                    this.workingTime = value;
                }
                else
                {
                    this.workingTime = null;
                }
            }
        }


        public Place(Coordinates coord, string name, string placeType, int likes, double? rating = null, string description = null, string workingTime = null)
        {
            this.CoordinatesPlace = coord;
            this.Name = name;
            this.PlaceType = placeType;
            this.Likes = likes;
            if (rating != null) this.Rating = rating.Value;
            this.Description = description;
            this.WorkingTime = workingTime;
        }

        public Place()
            : this(new Coordinates(), "", "", 0)
        { }

        public Place(Place p)
            : this(p.coordinatesPlace, p.name, p.placeType, p.likes, p.rating, p.description, p.workingTime)
        { }

        public string GetDescription()
        {
            if (this.description != null)
            {
                return this.description;
            }
            else
            {
                return "No description";
            }
        }

        public string GetWorkingTime()
        {
            if (this.workingTime != null)
            {
                return this.workingTime;
            }
            else
            {
                return "No working time";
            }
        }

        public override string ToString()
        {
            return string.Format("Coordinates : {0}\nName: {1}\nPlace type : {2}\nLikes : {3}\nDescription : {4}\nWorking time : {5}\nRating : {6}\n", this.coordinatesPlace, this.name, this.placeType, this.likes, this.GetDescription(), this.GetWorkingTime(), this.rating);
        }

    }
}
