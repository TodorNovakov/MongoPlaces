using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProjectLibrary
{
    public class Cinema:Place
    {
        private int seatingCapacity;
        private List<string> additionalInformation;

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
                List<string> res = new List<string>(this.additionalInformation);
                return res;
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

        public Cinema(Coordinates coord, string name, int likes, int seatingCapacity, List<string> additionalInformation, double? rating = null, string description = null, string workingTime = null)
            : base(coord, name, "cinema", likes, rating, description, workingTime)
        {
            this.SeatingCapacity = seatingCapacity;
            this.AdditionalInformation = additionalInformation;
        }

        public Cinema()
            : this(new Coordinates(), "", 0, 0, new List<string>())
        { }

        public Cinema(Cinema c)
            : this(c.CoordinatesPlace, c.Name, c.Likes, c.seatingCapacity, c.additionalInformation, c.Rating, c.Description, c.WorkingTime)
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
            return base.ToString() + string.Format("Seating capacity : {0}\nAdditional information : {1}\n", this.seatingCapacity, this.PrintAdditionalInfo());
        }
    }
}
