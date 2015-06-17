using System;
using System.Collections.Generic;
using FinalProject.Interfaces;

namespace FinalProject.Subjects
{
    public abstract class DataSubject: ISubject
    {  
        //using a List to be dynamic for Subscribers
        private List<ISubscriber> subscribers = new List<ISubscriber>();

        //using the Auto Implemented Properties as no other requirements for the local variables
        //each object will have a record of its own last updated time
        public DateTime LastTime { get; set; }

        //this will be set in each subclass constructor
        public string StationId{ get; set; }
        
        //this will be set in each subclass constructor
        public string HashTag { get; set; }

        //2d array to hold the potential headings and data values of ERDDAP HTML pages,
        //has a max number of 18
        public string[,] Table { get; set; }

        //getter and setter, common behaviour for all subclasses
        public string Update { get; set; }

        //this number is set based on the selections made to feed the URL
        public int Columns { get; set; }

        //this will hold the ERDDAP url for each Buoy, set in constructor of subclass
        public string BaseUrl { get; set; }

        //this will hold the full ERDDAP url for each Buoy, set in the buildURL method
        public string Url { get; set; }

        //common methods to all DataSubject subclasses
        public void AddSubscriber(ISubscriber sub)
        {
            //using the List method Add
            subscribers.Add(sub);
        }

        public void RemoveSubcriber(ISubscriber sub)
        {
            //using the List method Remove
            subscribers.Remove(sub);
        }

        //this method will update all the subscribers for this DataSubject subclass
        public void NotifySubscribers()
        {   //loop through all Subscribers
            foreach (ISubscriber sub in subscribers)
            {
                //call the overridden method of each subscriber 
                //and pass it this weather station object
                sub.update(this);
            }
        }

        //used in different ways by the subclasses
        public abstract void GetUpdate();
     
    }//end class
}
