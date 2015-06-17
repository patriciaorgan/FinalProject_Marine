using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Subjects;
using HtmlAgilityPack;

namespace FinalProject.Util
{
     public static class MyUtil
    {
         public static string getCompass(string degree)
        {   
            //using emojicons to set the arrow of the direction
            string N = "⬆";
            string S = "⬇";
            string E = "➡";
            string W = "⬅";
            string NE = "↗";
            string NW = "↖";
            string SE = "↘";
            string SW = "↙";
            //needs to be initialised, but all the if statements should catch all possible numbers
            string choice = "";

            //require error handling for the convert method
            try
            {
                //convert the passed in string into a double
                double degrees = Convert.ToDouble(degree);

                if ((degrees >= 337) || (degrees >= 0 && degrees <= 22))
                    choice = N;
                if ((degrees > 22 && degrees < 67))
                    choice = NE;
                if ((degrees >= 67 && degrees <= 112))
                    choice = E;
                if ((degrees > 112 && degrees < 157))
                    choice = SE;
                if ((degrees >= 157 && degrees <= 202))
                    choice = S;
                if ((degrees > 202 && degrees < 247))
                    choice = SW;
                if ((degrees >= 247 && degrees <= 292))
                    choice = W;
                if ((degrees > 292 && degrees < 337))
                    choice = NW;

                return choice;

            }catch(FormatException){
                Console.WriteLine("Passed in parameter was not of a suitable type to convert to a double");
                return choice;
            }
        }//end compass method

         /* This method is common to the Buoy data, It takes in the object of 
          * the calling Class and stores the data in its table,
          * it calls on variables as required from that class, like columns, and url
          * <parameters> DataSubject
          */
         public static void loadTable(DataSubject ds)
        {

            //declare a web object and from that a document object that loads the URL that is required
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc;

            //initialise the 2D array to the required amount of columns
            ds.Table = new string[2, ds.Columns];

            doc = web.Load(ds.Url);
            //[Test output]
            //Console.WriteLine(ds.Url);
           
            //this is to keep count of index in loops
            int i = 0;
            try
            {       //using XPath, this loops through html tags <th> headers, 
                    //using the class that this particular page uses
                    var columns=  doc.DocumentNode.SelectNodes("//table[@class='erd commonBGColor']/tr/th");
                    if (columns != null)
                    {
                        foreach (HtmlNode column in columns)
                        {
                            //check to see if node is null
                            if (column.InnerHtml == null)
                            { continue; }
                            else
                            {   //this is the length of column in the table in ERDDAP
                                if (i < ds.Columns)
                                {
                                    ds.Table[0, i] = column.InnerHtml;
                                    i++;
                                }
                            }
                        }//end loop
                    }
                    //reset the index
                    i = 0;
                    //using XPath, this loops through html tags <td> data elements,
 		            //using the class that this particular page uses
                    var cells = doc.DocumentNode.SelectNodes("//table[@class='erd commonBGColor']/tr/td");
                    if (cells != null)
                    {
                        foreach (HtmlNode cell in cells)
                        {
                            //a catch to ensure the node is not null
                            if (cell.InnerHtml == null)
                            { continue; }
                            else
                            {
                                if (i < ds.Columns)
                                {
                                    //load into 2d array in the second row
                                    ds.Table[1, i] = cell.InnerHtml;
                                    i++;
                                }
                            }
                        }//end loop
                    }
                //[Test]
                //printing to check contents
                /*
                if(ds.Table != null){
                    foreach (var h in ds.Table)
                    { if (h != null) Console.WriteLine(h); }
                }
                 * */
            }catch (NullReferenceException)
            {
                Console.WriteLine("No table elements to read");
            }

        }//end loadTable Method

         public static void buildURL(DataSubject ds)
         {
             //reset the URL when method called
             ds.Url = ds.BaseUrl;

             //using this AddDays() method with the DateTime to go back 2 days worth to make sure to catch values
             DateTime d = DateTime.Now.AddDays(-2);

             //add the search parameters like station id and date and time to the url
             //placed the date in correct format and extract the hour and round it to even number
             string patt = @"yyyy-MM-dd";
             string newD = d.ToString(patt);
             ds.Url += newD + "T" + d.Hour + ":00:00Z&station_id=%22" + ds.StationId + "%22&orderByMax(%22time%22)";
         }
    }//end MyUtil class
}
