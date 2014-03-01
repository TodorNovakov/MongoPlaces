using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProjectLibrary
{
    public class Restaurant:Place
    {
        private int seatingCapacity;
        private List<string> additionalInformation;
        private string priceCategory;

        public int SeatingCapacity
        {
            get
            {
                return this.seatingCapacity;
            }
            set
            {
                if (value >= 0)
                {
                    this.seatingCapacity = value;
                }
                else
                {
                    this.seatingCapacity = 0;
                }
            }
        }
        public List<string> AdditionalInformation
        {
            get
            {
                List<string> resultList = new List<string>(this.additionalInformation);
                return resultList;
            }

            set
            {
                if (value != null)
                {
                    this.additionalInformation = new List<string>(value);
                }
                else
                {
                    this.additionalInformation = new List<string>();
                }
            }
        }
        public string PriceCategory
        {
            get
            {
                return this.priceCategory;
            }
            set
            {
                if (value != null)
                {
                    this.priceCategory = value;
                }
                else
                {
                    this.priceCategory = "";
                }
            }
        }

        public Restaurant(Coordinates coord, string name,int likes,int seatingCapacity, List<string> additionalInformation, string priceCategory,double? rating = null, string description = null, string workingTime = null)
            : base(coord, name,"restaurant", likes, rating, description, workingTime)
        {
            this.SeatingCapacity = seatingCapacity;
            this.AdditionalInformation = additionalInformation;
            this.PriceCategory = priceCategory;
        }

        public Restaurant()
            : this(new Coordinates(), "", 0, 0, new List<string>(), "")
        { }

        public Restaurant(Restaurant r)
            : this(r.CoordinatesPlace, r.Name, r.Likes, r.seatingCapacity, r.additionalInformation, r.priceCategory, r.Rating, r.Description, r.WorkingTime)
        { }

        public override string ToString()
        {
            return base.ToString() + string.Format("Seating capacity : {0}\nAdditional information : {1}\nPrice category :{2}\n", this.seatingCapacity, this.PrintAdditionalInfo(), this.priceCategory);
        }

        public string PrintAdditionalInfo()
        {
            string res = null;
            foreach (string str in this.additionalInformation)
            {
                res += string.Format("{0} ", str);
            }
            return res;
        }
    }
}
