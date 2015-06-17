using System;
using System.Collections.Generic;
using System.Threading;
using FinalProject.Interfaces;
using FinalProject.Subjects;
using FinalProject.Subscriber;

namespace FinalProject
{
    class Program
    {
        //declaring these object outside main and internal so they can be used within the namespace
        internal static DataSubject cork = new CorkBuoyM3();
        internal static DataSubject dublin = new DublinBuoyM2();
        internal static DataSubject donegal = new DonegalBuoyM4();
        internal static DataSubject wexford = new WexfordBuoyM5();
        internal static DataSubject forecast = new ForecastData();
        internal static DataSubject corkwd = new CorkBouyWave();
        internal static DataSubject dublinwd = new DubBuoyWave();
        internal static DataSubject wexfordwd = new WexBuoyWave();
        internal static DataSubject donegalwd = new DonBuoyWave();
        internal static DataSubject galbaywd = new GalBayBuoyWave();
        internal static DataSubject belmulAwd = new BelmulletAWave();
        internal static DataSubject belmulBwd = new BelmulletBWave();
        internal static DataSubject rockall = new RockallTroughBuoyM6();
        

        static void Main(string[] args)
        {
            
            //create a list of all subjects to pass to the Subscribers so they can register themselves with all
            List<ISubject> subjects = new List<ISubject>();
            subjects.Add(cork);
            subjects.Add(dublin);
            subjects.Add(donegal);
            subjects.Add(wexford);
            subjects.Add(corkwd);
            subjects.Add(forecast);
            subjects.Add(dublinwd);
            subjects.Add(galbaywd);
            subjects.Add(belmulAwd);
            subjects.Add(belmulBwd);
            subjects.Add(rockall);

            //want the Demo website to only subscribe to the Forecast class and not the others
            //shows flexibility of the code
            List<ISubject> websiteSubjects = new List<ISubject>();
            websiteSubjects.Add(forecast);

            //Adding of a subscribers to DataSubject, by passing the list of data subjects in constructor
            ISubscriber twit = new Twitter(subjects);
            //ISubscriber faceb = new FaceBook(subjects);
            ISubscriber demoweb = new DemoWebsite(websiteSubjects);
         
            try
            {
                //create a Threading.timer object to call the update method every 90000 milliseconds 
                //minute and half, so each timer can be treated separatly if an error occurs
                //[Weather]
                Timer Ctime = new Timer(CorkTimerCallback, null, 0 ,  90000); 
                Timer DBtime = new Timer(DublinTimerCallback, null, 0, 90000);
                Timer DLtime = new Timer(DonegalTimerCallback, null, 0, 90000);
                Timer Wtime = new Timer(WexfordTimerCallback, null, 0, 90000);
                Timer Rtime = new Timer(RockallWeatherTimerCallback,null, 0, 90000);

                //[Forecast Wave] // every 5 min for demo
                Timer Ftime = new Timer(ForcastTimerCallback, null, 0, 300000);

                //[Wave Hourly]
                Timer CWDtime = new Timer(CorkWaveTimerCallback, null, 0, 90000);
                Timer DBWDtime = new Timer(DublinWaveTimerCallback, null, 0, 90000);
                Timer DNWDtime = new Timer(DonegalWaveTimerCallback, null, 0, 90000);
                Timer WWDtime = new Timer(WexfordWaveTimerCallback, null, 0, 90000);
               
                //[Wave 30min]
                Timer GBWDtime = new Timer(GalwayBayWaveTimerCallback, null, 0, 90000); 
                Timer BAWDtime = new Timer(BelmulletAWaveTimerCallback, null, 0, 90000);
                Timer BBWDtime = new Timer(BelmulletBWaveTimerCallback, null, 0, 90000);
 
                //keep the console open to read output    
                Console.ReadLine();

            }catch(Exception e)
            {
                Console.WriteLine("Error has occured in Main: " + e.Message);
            }

        }//end Main method

        //separate timer methods that are called from main are static.
        //allows for separate error handling for each and personalised messages

        private static void RockallWeatherTimerCallback(object state)
        {
            try
            {
                rockall.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In RockallWeatherTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }
        private static void BelmulletAWaveTimerCallback(object state)
        {
            try
            {
                belmulAwd.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In BelmulletAWaveTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }
        private static void BelmulletBWaveTimerCallback(object state)
        {
            try
            {
                belmulBwd.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In BelmulletBWaveTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }
        private static void GalwayBayWaveTimerCallback(object state)
        {
            try
            {
                galbaywd.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In GalwayBayWaveTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }

        private static void WexfordWaveTimerCallback(object state)
        {
            try
            {
                wexfordwd.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In WexfordWaveTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }

        private static void DonegalWaveTimerCallback(object state)
        {
            try
            {
                donegalwd.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In DonegalWaveTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }

        private static void DublinWaveTimerCallback(object state)
        {
            try
            {
                dublinwd.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In DublinWaveTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }

        private static void CorkWaveTimerCallback(object state)
        {
            try
            {
                corkwd.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In CorkWaveTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }
        
        private static void ForcastTimerCallback(object state)
        {
            try
            {
                Console.WriteLine("Forecast Timer run " + DateTime.Now);
                forecast.GetUpdate();
               
            }
            catch (Exception e)
            {
                Console.WriteLine("In ForcastTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }
        
        private static void CorkTimerCallback(Object o)
        {
           try
            {            
               cork.GetUpdate();
            }
           catch (Exception e)
           {
               Console.WriteLine("In CorkTimerCallback Error has occurred: " + e.Message);
            }
           //force the collection
          GC.Collect();
           
        }
        
        private static void DublinTimerCallback(Object o)
        {
            try
            {
                dublin.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In DublinTimerCallback Error has occurred: " + e.Message);
            }
             //force the collection
             GC.Collect();
        }
        private static void DonegalTimerCallback(Object o)
        {
            try
            {
                donegal.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In DonegalTimerCallback Error has occurred: " + e.Message);
            }
           //force the collection
           GC.Collect();
        }
        private static void WexfordTimerCallback(Object o)
        {
            try
            {
                wexford.GetUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine("In WexfordTimerCallback Error has occurred: " + e.Message);
            }
            //force the collection
            GC.Collect();
        }
         
    }//end Class
}
