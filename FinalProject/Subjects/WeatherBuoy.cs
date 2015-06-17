using FinalProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Subjects
{
    abstract class WeatherBuoy: DataSubject
    {
        //constructor common setting to all subclasses
        public WeatherBuoy()
        {
            Columns = 7;
            BaseUrl = "http://erddap.marine.ie/erddap/tabledap/IWBNetwork.htmlTable?time,AtmosphericPressure,WindDirection,WindSpeed,Gust,AirTemperature,RelativeHumidity&time%3E=";
        }
        
        //this method extracts the info from the URL of ERDDAP and populates the table 2d array
        //it sets the Update string so it can be used by subscribers
        public override void GetUpdate()
        {
            //helper class MyUtil is static so no need for an instance of it
            //call the buildURL method common behaviour to all ERDDAP
            MyUtil.buildURL(this);

            //call the method to load the values from the url into the 2d array called Table
            MyUtil.loadTable(this);
         
            if (Table != null)
            {
                //need to use the value from the table to ensure true values used
                DateTime dt = DateTime.ParseExact(Table[1, 0].Trim(), "yyyy-MM-dd'T'HH:mm:ss'Z'", null);

                //check what is the potential new Max time, then can compare against lastTime update
                if (dt != LastTime)
                {
                    //using the utility static class call the getCompass and pass it the value 
                    //for direction, returns emoticon arrow
                    string comp = MyUtil.getCompass(Table[1, 2].Trim());
                    
                   // works for twitter = "⛅";
                   // works for twitter and facebook = "☀";

                    //this will cater for when there is never humidity in the Donegal Buoy
                    string humidity;
                    if (this is DonegalBuoyM4)
                    { humidity = ""; }
                    else
                    { humidity = "Humidity:" + Table[1, 6].Trim() + "%"; }
                    
                    //assign using the setter method for update string in base class
                    Update = dt.ToString("MMM") + dt.Day + " " + dt.ToString("HH") +
                        ":00 " + HashTag +
                        "\n☀ Temperature:" + Table[1, 5].Trim() +
                        "°C " + humidity +
                        "\n💨 Direction: " + comp + " WindSpeed:" + Table[1, 3].Trim() +
                        "kn/Gust:" + Table[1, 4].Trim() + "kn";

                    //Observer Design Pattern method
                    NotifySubscribers();
                    //record the DateTime of the update
                    LastTime = dt;
                }//end if not duplicate

            }//end if table populated

        }//end method getUpdate
    }//end class
}
