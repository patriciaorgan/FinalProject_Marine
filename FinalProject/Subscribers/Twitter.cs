using System;
using System.Collections.Generic;
using FinalProject.Interfaces;
using FinalProject.Subjects;
using Tweetinvi;

namespace FinalProject.Subscriber
{
    class Twitter: ISubscriber
    {
        
        // Obtain these values by creating a Twitter app at http://dev.twitter.com/
        private static string consumer_Key = "d4R7LaqJJWAVB5FgTkkPKtNtY";
        private static string consumer_Secret = "boFYhisfKKvBvgZj4pNfDcOQbfqNdv2jeBzh871rkdNrUoBSd3";
        private static string access_Token = "2999399709-2wey5BUJYyL7Cm98d8IFFGgF0N3JaBTPMgGDQn2";
        private static string access_Token_Secret = "RpEA4GGmhzU66ib8THEDjbvrYTq4OOHmOuF9MvDi3tE6H";

        public Twitter(List<ISubject> subjects)
        {   //common to the observer design pattern is to create Subject object and use the addSubcriber method
            //in the constructor to the data subject as there are many Data sources will be passing a list of 
             //Subjects in the constructor

            foreach (ISubject data in subjects)
            {
                data.AddSubscriber(this);
            }

            //constructor creates the connection using the twitter API, with error handling
            try
            {
                // Setup your credentials
                //Twitter application connection created with my Dev Keys from test twitter account
                TwitterCredentials.ApplicationCredentials = TwitterCredentials.CreateCredentials(
                    access_Token, access_Token_Secret, consumer_Key, consumer_Secret);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error has occurred when accessing Twitter: " + ex);
            }
        }

        //implementing the update method from ISubscriber
        public void update(DataSubject ds)
        {
                try
                {
               
                    //[Test output]
                    Console.WriteLine("Tweet weather :" + ds.Update);
                    //publish to twitter account
                    Tweet.PublishTweet(ds.Update);
                
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("Publish data could not be read");
                }

        }//end method
    }
}
