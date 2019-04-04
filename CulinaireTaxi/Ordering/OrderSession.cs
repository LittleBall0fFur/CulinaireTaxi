using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaireTaxi.Ordering
{
    public class OrderSession
    {
        int SessionID;
        Restaurant chosenRestaurant;
    }

    public class Restaurant
    {
        int id;
        string name;
        double longtitude, latitude;
    }
}
