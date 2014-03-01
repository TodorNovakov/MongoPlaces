using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProjectLibrary
{
    public class Coordinates
    {
        private double latitude;
        private double longitude;

        public double Latitude
        {
            get { return this.latitude; }
            set
            {
                if (value >= 0.0)
                {
                    this.latitude = value;
                }
            }
        }

        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                if (value >= 0.0)
                {
                    this.longitude = value;
                }
            }
        }

        public Coordinates(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public Coordinates()
            : this(0.0, 0.0)
        { }

        public Coordinates(Coordinates c)
            : this(c.latitude, c.longitude)
        { }

        public override string ToString()
        {
            return "latitude : " + this.latitude + "  longitude : " + this.longitude;
        }
    }
}
