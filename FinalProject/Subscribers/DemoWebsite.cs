    using FinalProject.Interfaces;
    using FinalProject.Subjects;
    using System;
    using System.Collections.Generic;
    using System.Web.Http.SelfHost;
    using System.Web.Http;
    using FinalProject.WebAPI;

    namespace FinalProject.Subscriber
    {
        class DemoWebsite : ISubscriber
        {   //have used netsh.exe on this machine to allow full permission to users on this port
            static readonly Uri _baseAddress = new Uri("http://localhost:60065/");

            //constructor
            public DemoWebsite(List<ISubject> subjects)
            {
                //adding this subscriber when created to all the available subjects
                foreach (ISubject data in subjects)
                {
                    data.AddSubscriber(this);
                }
                //once the class is created in Main the SelfHost is started
                try
                {
                    //[WEBAPI SELFHOST]
                    // Set up server configuration
                    HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);

                    //added this line to allow cross origin resource sharing CORS,
                    //well actually to make the client think it is from the same origin
                    config.EnableCors();

                    config.Routes.MapHttpRoute(
                      name: "DefaultApi",
                      routeTemplate: "api/{controller}/{id}",
                      defaults: new { id = RouteParameter.Optional }
                    );
                    // Create server
                    var server = new HttpSelfHostServer(config);
                    // Start listening
                    server.OpenAsync().Wait();
                    Console.WriteLine("Web API Self hosted on " + _baseAddress );
                
                }catch(Exception e)
                {
                    Console.WriteLine("Error with lauching SelfHosting WebAPI: " + e);
                }
           
            }//end constructor

            public void update(DataSubject ds)
            {
                //create instance of the objects required to create WebAPI elements
                WidgetItem wi = new WidgetItem();
                WidgetItemController wc = new WidgetItemController();

                    if (ds is ForecastData)
                    {
                        //need to extract the link from the string once we know it is from Forecast
                        wi.caption = ds.Update.Substring(0, ds.Update.IndexOf("\n"));
                        wi.link = ds.Update.Substring(ds.Update.IndexOf("\n"));
                        //adding this item to the array will add it to the list and will be available in the WebAPI
                        //location, need to first remove an item to allow for there to always be 6 items in the list
                        wc.replaceElement(wi);
                        //[Test output]
                        Console.WriteLine("Demo Website update made:" + wi.caption);
                   
                    }
                    else
                    {  //this will display the Buoy data in same form as other Subscribers
                        wi.caption = ds.Update;
                        wi.link = "";
                        wc.replaceElement(wi);
                    }
            }//end update method
        }//end class
    }
