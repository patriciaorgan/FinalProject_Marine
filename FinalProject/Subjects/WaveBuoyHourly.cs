using FinalProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Subjects
{
    abstract class WaveBuoyHourly:DataSubject
    {
        //constructor common setting to all subclasses
        public WaveBuoyHourly()
        {
            Columns = 7;
            BaseUrl = "http://erddap.marine.ie/erddap/tabledap/IWBNetwork.htmlTable?time,AtmosphericPressure,WaveHeight,WavePeriod,MeanWaveDirection,Hmax,SeaTemperature&time%3E=";
        }

        //this method extracts the info from the URL of ERDDAP and populates the table 2d array
        //it sets the Update string so it can be used by subscribers
        public override void GetUpdate()
        {
            //helper class MyUtil is static so no need for an instance of it
            //call the buildURL method common behaviour to all ERDDAP
            MyUtil.buildURL(this);

            //call the method to load the values from the url into the 2d array called table
            MyUtil.loadTable(this);

            if (Table != null)
            {
                //need to use the value from the table to ensure true values used
                DateTime dt = DateTime.ParseExact(Table[1, 0].Trim(), "yyyy-MM-dd'T'HH:mm:ss'Z'", null);

                //check what is the potential new Max time then can compare against last update
                if (dt != LastTime)
                {
                    //using the utility static class call the getCompass and pass it the value 
                    //for direction, returns emoticon arrow
                    string comp = MyUtil.getCompass(Table[1, 4].Trim());
                    
                    //assign using the setter method for update string in base class
                    Update = dt.ToString("MMM") + dt.Day + " " + dt.ToString("HH") +
                        ":00 " + HashTag +
                        "\nPressure:" + Table[1, 1].Trim() + 
                        "mb  Height:" + Table[1, 2].Trim() + 
                        "m\nPeriod:" + Table[1, 3].Trim() + 
                        "s MeanWaveDirection: " + comp + " WaveHeightMax:" + Table[1, 5].Trim() + 
                        "m SeaTemp:" + Table[1, 6].Trim() + "°C";
                    //Observer Design Pattern method
                    NotifySubscribers();
                    //record the DateTime of the update
                    LastTime = dt;
                }//end if not duplicate

            }//end if table populated

        }//end method getUpdate
    }//end Class
}
