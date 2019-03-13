using System;
using TenMinuteTakeover.AzureFunction.Configuration;
using Tweetinvi;

namespace TenMinuteTakeover.AzureFunction.Clients
{
    static class TwitterClient
    {
        public static void PublishTweetAsync(string tweet)
        {
            var settings = GetSettings();

            Auth.SetUserCredentials(
                settings.ConsumerKey,
                settings.ConsumerKeySecret,
                settings.AccessToken,
                settings.AccessTokenSecret);

            Tweet.PublishTweet(tweet); 
        }

        static TwitterSettings GetSettings()
        {
            return new TwitterSettings
            {
                ConsumerKey = Environment.GetEnvironmentVariable("TwitterConsumerKey"),
                ConsumerKeySecret = Environment.GetEnvironmentVariable("TwitterConsumerKeySecret"),
                AccessToken = Environment.GetEnvironmentVariable("TwitterAccessToken"),
                AccessTokenSecret = Environment.GetEnvironmentVariable("TwitterAccessTokenSecret")
            };
        }
    }
}
