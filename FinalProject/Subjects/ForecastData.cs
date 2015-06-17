using FinalProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Subjects
{
    public class ForecastData : DataSubject
    {
        private List<string> links = new List<string>();
        private int index = 0;

        public ForecastData()
        {
            //important for substring method
            //the stings have been set up with first line description and second line separated by '\n' then link
            //[Forecast Data]
            links.Add("Wave Forecasts\n" +
                       "http://www.marine.ie/Home/site-area/data-services/marine-forecasts/wave-forecasts");
           
            links.Add("Tidal Predictions\n" +
                      "http://www.marine.ie/Home/site-area/data-services/marine-forecasts/tidal-predictions");

            links.Add("Ocean Forecasts\n" +
                        "http://www.marine.ie/Home/site-area/data-services/marine-forecasts/ocean-forecasts" );
           //[24hr Review Data]
           //could separate this list into a new Class called Overview Data, 
           //but felt it would be repeating the code unnecessarily
            links.Add("Graph:Ireland WindSpeed in last 24hrs\n" + 
                "http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=WindSpeed");
            links.Add( "Graph:Ireland Air Pressure in last 24hrs\n" +
                "http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=AtmPressure");
            links.Add("Graph:Ireland Temperature in last 24hrs\n" +
                "http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=DryBulb");
            links.Add("Graph:Ireland Humidity in last 24hrs\n" +
                 "http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=RelativeHumidity");
            links.Add("Graph:Ireland Wind Gust in last 24hrs\n" +
                "http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=Gust");
            links.Add("Graph:Ireland Wind Direction in last 24hrs\n" +
                "http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=WindDirection");

        }//end constructor

        //override this method from the superclass, in this class will use the List of possible 
        //updates and choose the next one until the last
        public override void GetUpdate()
        {
            //set the string for the update
            Update = links[index];

            //loop through all the list and return to start when reaches the end
            if (index == links.IndexOf(links.Last()))
            { index = 0; }
            else
            { index++; }

            //call the method to all the subscribers to update
            NotifySubscribers();
        }//end getUpdate
    }
}
