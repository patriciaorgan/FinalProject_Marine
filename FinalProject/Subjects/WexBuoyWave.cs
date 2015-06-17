using FinalProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Subjects
{
    class WexBuoyWave:WaveBuoyHourly
    {
        //constructor
        public WexBuoyWave()
        {
            StationId = "M5";
            HashTag = "#M5 #WearherBuoy #Wave #Wexford";
        }
    }
}
