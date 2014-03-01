using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProjectLibrary
{
    public class Pharmacy:Place
    {
        private List<string> additionalInformation;

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

        public Pharmacy(Coordinates coord, string name, int likes, List<string> additionalInformation, double? rating = null, string description = null, string workingTime = null)
            : base(coord, name, "pharmacy", likes, rating, description, workingTime)
        {
            this.AdditionalInformation = additionalInformation;
        }

        public Pharmacy()
            : this(new Coordinates(), "", 0, new List<string>())
        { }

        public Pharmacy(Pharmacy p)
            : this(p.CoordinatesPlace, p.Name, p.Likes, p.additionalInformation, p.Rating, p.Description, p.WorkingTime)
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
            return base.ToString() + string.Format("Additional information : {0}", this.PrintAdditionalInfo());
        }
    }
}
