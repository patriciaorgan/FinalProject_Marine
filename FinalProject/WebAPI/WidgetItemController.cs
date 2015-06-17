using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FinalProject.WebAPI
{
    //this allows for the header of any message  sent to a website to match that website so it 
    //will accept and read the message. If the origins were different the browser would not read it
    [EnableCors(origins: "http://lugh4.it.nuigalway.ie", headers: "*", methods: "*")]
    public class WidgetItemController : ApiController
    {
        private static List<WidgetItem> items = new List<WidgetItem>  
        {  //initialising the List with 6 items so the list is always full
          new WidgetItem { Id = 1, caption = "Graph:Ireland WindSpeed in last 24hrs", 
              link="http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=WindSpeed"},
          new WidgetItem { Id = 2, caption = "Graph:Ireland Air Pressure in last 24hrs",
              link="http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=AtmPressure"},  
          new WidgetItem { Id = 3, caption = "Graph:Ireland Temperature in last 24hrs", 
              link="http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=DryBulb"},
          new WidgetItem { Id = 4, caption = "Graph:Ireland Humidity in last 24hrs", 
              link="http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=RelativeHumidity"},
          new WidgetItem { Id = 5, caption = "Graph:Ireland Wind Gust in last 24hrs", 
              link="http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=Gust"}, 
          new WidgetItem { Id = 6, caption = "Graph:Ireland Wind Direction in last 24hrs", 
              link ="http://webapps.marine.ie/IWPGraphs/Default.aspx?Type=2&ProjectId=2&Inst=WindDirection"}
        };

        //this method is used by the website the standard HTML get method, returns the list
        public IEnumerable<WidgetItem> Get()
        {
            return items;
        }

        //as the List is to always be full for the Carousel this is the only other method required
        public void replaceElement(WidgetItem e)
        {
            items.RemoveAt(0);
            items.Add(e);
        }

    }//end Class
}
