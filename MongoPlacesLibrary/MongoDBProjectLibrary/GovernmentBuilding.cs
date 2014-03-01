using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBProjectLibrary
{
    public class GovernmentBuilding:Place
    {
        public GovernmentBuilding(Coordinates coord, string name, int likes, double? rating = null, string description = null, string workingTime = null)
            :base(coord,name,"government building",likes,rating,description,workingTime)
        {}

        public GovernmentBuilding():
            this(new Coordinates(), "", 0, 0)
       {}

        public GovernmentBuilding(GovernmentBuilding gb)
            : this(gb.CoordinatesPlace, gb.Name, gb.Likes, gb.Rating, gb.Description, gb.WorkingTime)
        { }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
