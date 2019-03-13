using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TenMinuteTakeover.AzureFunction.Clients;

namespace TenMinuteTakeover.AzureFunction
{
    public static class TimedFunction
    {
        [FunctionName("TimedFunction")]
        public static async Task RunAsync([TimerTrigger("*/5 * * * * * ")]TimerInfo myTimer, ILogger log)
        {
            var tracks = await SpotifyClient.GetFavouriteTracksAsync();

            foreach (var track in tracks)
            {
                var tweetToSend = $"@BBCR1 - {track.Name} by {track.Artist}";

                if (track.FeaturingArtist.Any())
                    tweetToSend += tweetToSend + $" feat {string.Join(",", track.FeaturingArtist)}";

                TwitterClient.PublishTweetAsync(tweetToSend);
            }
        }
    }
}
