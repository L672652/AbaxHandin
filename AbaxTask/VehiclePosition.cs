using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbaxTask
{
    public class VehiclePosition
    {
        public VehiclePositionInner VP { get; set; }


    }
    public class VehiclePositionInner
    {
        public int Veh { get; set; }
        public decimal Lat { get; set; }

        public decimal Long { get; set; }

        public string next_stop { get; set; }
    }
}


/* {"VP":{"desi":"Z","dir":"1","oper":90,"veh":6304,"tst":"2024-04-13T16:38:48.608Z","tsi":1713026328,
 * "spd":43.79,"hdg":31,"lat":60.938211,"long":25.527790,"acc":0.00,"dl":-379,"odo":null,
 * "drst":null,"oday":"2024-04-13","jrn":9863,"line":286,"start":"18:35","loc":"GPS","stop":null,"route":"3001Z","occu":0}}*/
