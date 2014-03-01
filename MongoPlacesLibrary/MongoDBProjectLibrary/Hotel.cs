using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProjectLibrary
{
    public class Hotel:Place
    {
        private int capacity;
        private List<string> additionalInformation;
        private string priceCategory;

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            set
            {
                if (value >= 0)
                {
                    this.capacity = value;
                }
                else
                {
                    this.capacity = 0;
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

        public Hotel(Coordinates coord, string name, int likes, int capacity, List<string> additionalInformation, string priceCategory, double? rating = null, string description = null, string workingTime = null)
            : base(coord, name, "hotel", likes, rating, description, workingTime)
        {
            this.Capacity = capacity;
            this.AdditionalInformation = additionalInformation;
            this.PriceCategory = priceCategory;
        }

        public Hotel()
            : this(new Coordinates(), "", 0, 0, new List<string>(), "")
        { }

        public Hotel(Hotel h)
            : this(h.CoordinatesPlace, h.Name, h.Likes, h.capacity, h.additionalInformation, h.priceCategory, h.Rating, h.WorkingTime)
        { }

        public string PrintAdditionalInfo()
        {
            string res = null;
            foreach (string str in this.additionalInformation)
            {
                res += string.Format("{0} ", str);
            }
            return res;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("Capacity : {0}\nAdditional information : {1}\nPrice category :{2}\n", this.capacity, this.PrintAdditionalInfo(), this.priceCategory);
        }
    }
}
