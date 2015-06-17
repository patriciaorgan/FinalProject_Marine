using Facebook;
using FinalProject.Interfaces;
using FinalProject.Subjects;
using System;
using System.Collections.Generic;

namespace FinalProject.Subscriber
{
    class FaceBook : ISubscriber
    {
        //creating an object of the FacebookClient type from the referenced  Facebook API
        FacebookClient client;

        //access token obtained from Graph API Facebook page, and applied to get fan page approval

        string access_token = "CAACEdEose0cBAKnXT9LyULiDW9xfpEOQv0RyUZBnWuu32vlX4KDWFXDaZCy6LRXp7zKwK9zGFdQGo1XqJlfYoSaEWZASdsyqsKZClZAZC9DqmZC5UgQILXDpHArAj5RfRudYmeXaruNwXtuzz4ZAuugtZAjeBTOkx6dlMosv1y1LOZC9UV9NQbva67KkNhZAO9M6Xr6l3QdUZBzrsakODZAGI2JBPr14WMBDCr00ZD";
        
        //constructor
        public FaceBook(List<ISubject> subjects)
        {
            //subscribing to all available subjects created so far
            foreach (ISubject data in subjects)
            {
                data.AddSubscriber(this);
            }

            try
            {
                //instantiating the Facebook Client in the constructor and checking for exceptions
                client = new FacebookClient(access_token);
                
            }
            catch (FacebookOAuthException e)
            {
                Console.WriteLine("Connection to FaceBook failed: " + e);
                throw;
            }
        }


        //implementing the update method from ISubscriber
        public void update(DataSubject ds)
        {
                try
                {
                    client.Post("/me/feed", new { message = ds.Update });
                    Console.WriteLine("Facebook Post was sent");
                }
                catch (FacebookApiException f)
                {
                    Console.WriteLine("Facebook API Exception: " + f.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Posting failed to Facebook: " + e);
                }
        }//end update method
    }//end class
}
