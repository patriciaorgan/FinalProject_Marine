using System;
using NUnit.Framework;
using FinalProject.Util;
using FinalProject.Subjects;

namespace FinalProject.UnitTests
{
    [TestFixture]
    public class UnitTestFinalProject
    {
        [Test]
        public void TestGetCompass()
        {
            //create a string to hold the result
            string comp = MyUtil.getCompass("90");
            string East = "➡";
 
            //test the result is as expected 
            Assert.AreEqual(East, comp);
        }

        [Test]
        public void TestloadTable()
        {
            //initialise a Subject object
            DataSubject Corkds = new CorkBuoyM3();
            //need to set the url before loading the table
            MyUtil.buildURL(Corkds);

            //call the method that is to be tested, 
            //this method should populate the Table variable
            MyUtil.loadTable(Corkds);

            //results at this particular moment are from this time frame,
            //so can compare the Tempreture table cell is correct as expected

            //create a string to hold the result
            string result = Corkds.Table[1, 5].Trim();
            //correct for the test, will need to adjust whne retesting
            string expected = "10.791";

            //test the result is as expected 
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestbuildURL()
        {
            //initialise a Subject object
            DataSubject Corkds = new CorkBuoyM3();
            //need to set the url before loading the table
            MyUtil.buildURL(Corkds);

            //create a string to hold the result
            string result = Corkds.Url;
            //correct for this test, will need to adjust when retesting
            string expected = "http://erddap.marine.ie/erddap/tabledap/IWBNetwork.htmlTable?time,AtmosphericPressure,WindDirection,WindSpeed,Gust,AirTemperature,RelativeHumidity&time%3E=2015-04-13T17:00:00Z&station_id=%22M3%22&orderByMax(%22time%22)";

            //test the result is as expected 
            Assert.AreEqual(expected, result);
        }
    }
}
