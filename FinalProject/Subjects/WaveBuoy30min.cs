using FinalProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Subjects
{
    abstract class WaveBuoy30min: DataSubject
    {
        //constructor
        public WaveBuoy30min()
        {
            Columns = 6;
            BaseUrl = "http://erddap.marine.ie/erddap/tabledap/IWaveBNetwork30Min.htmlTable?time,PeakPeriod,PeakDirection,UpcrossPeriod,SignificantWaveHeight,SeaTemperature&time%3E=";
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
                    string comp = MyUtil.getCompass(Table[1, 2].Trim());

                    //check all data elements are not empty else replace output with nothing
                    string col1 = "";
                    string col2 = "";
                    string col3 = "";
                    string col4 = "";
                    string col5 = "";
                    if (Table[1, 1] != "&nbsp")
                    {
                        col1 = "Peak Period:" + Table[1, 1].Trim()+ "s";
                    }
                    if (Table[1, 2] != "&nbsp")
                    {
                        col2 = " Peak Direction: " + comp;
                    }
                    if (Table[1, 3] != "&nbsp")
                    {
                        col3 = "UpcrossPeriod:" + Table[1, 3].Trim() + "s";
                    }
                    if (Table[1, 4] != "&nbsp")
                    {
                        col4 = "SigWaveHeight:" + Table[1, 4].Trim() + "cm";
                    }
                    if (Table[1, 5] != "&nbsp")
                    {
                        col5 = "SeaTemp:" + Table[1, 5].Trim() + "°C";
                    }


                    //assign using the setter method for update string in base class
                    Update = dt.ToString("MMM") + dt.Day + " " + dt.ToString("HH:mm")
                             + " " + HashTag + "\n" + col1 +  " " + col2 +  "\n" + col3 + " " + col4 + " " + col5;

                    //Observer Design Pattern method
                    NotifySubscribers();
                    //record the DateTime of the update
                    LastTime = dt;
                }//end if not duplicate

            }//end if table populated              

        }//end method getUpdate
    }
}
