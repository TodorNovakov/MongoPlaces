using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProjectLibrary
{
    public class Hostel:Place
    {
        private int capacity;
        private List<string> additionalInformation;

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

        public Hostel(Coordinates coord, string name, int likes, int capacity, List<string> additionalInformation, double? rating = null, string description = null, string workingTime = null)
            : base(coord, name, "hostel", likes, rating, description, workingTime)
        {
            this.Capacity = capacity;
            this.AdditionalInformation = additionalInformation;
        }

        public Hostel()
            : this(new Coordinates(), "", 0, 0, new List<string>())
        { }

        public Hostel(Hostel h)
            : this(h.CoordinatesPlace, h.Name, h.Likes, h.capacity, h.additionalInformation, h.Rating, h.Description, h.WorkingTime)
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
            return base.ToString() + string.Format("Capacity : {0}\nAdditional information : {1}\n", this.capacity, this.PrintAdditionalInfo());
        }

    }
}
