using Newtonsoft.Json;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TenMinuteTakeover.AzureFunction.Configuration;
using TenMinuteTakeover.AzureFunction.Models;

namespace TenMinuteTakeover.AzureFunction.Clients
{
    static class SpotifyClient
    {
        const string TokenUrl = "https://accounts.spotify.com/api/token";
        const string PostMaloneArtistId = "2DaxqgrOhkeH0fpeiQq2f4?si=zYAbUJ_yQdK119SQF-BnLg";
        const string Country = "SE";
        static readonly HttpClient Client = new HttpClient();

        public static async Task<IEnumerable<SpotifyTrack>> GetFavouriteTracksAsync()
        {
            var settings = GetSettings();
            var token = await GetTokenAsync(settings);
            var api = new SpotifyWebAPI
            {
                AccessToken = token.AccessToken,
                TokenType = token.Type
            };

            // TODO: currently using 'GetArtistsTopTracksAsync' for testing purposes 
            var result = await api.GetArtistsTopTracksAsync(PostMaloneArtistId, Country);

            return result.Tracks.Select(o => new SpotifyTrack
            {
                Name = o.Name,
                Artist = o.Artists.First().Name,
                FeaturingArtist = o.Artists.Skip(1).Select(p => p.Name).ToArray()
            });
        }

        static async Task<SpotifyToken> GetTokenAsync(SpotifySettings settings)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{settings.ClientId}:{settings.ClientSecret}"));

            var args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, TokenUrl);
            request.Headers.Add("Authorization", $"Basic {auth}");
            request.Content = new FormUrlEncodedContent(args);

            var resp = await Client.SendAsync(request);
            var msg = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SpotifyToken>(msg);
        }

        static SpotifySettings GetSettings()
        {
            return new SpotifySettings
            {
                ClientId = Environment.GetEnvironmentVariable("SpotifyClientId"),
                ClientSecret = Environment.GetEnvironmentVariable("SpotifyClientSecret"),
            };
        }
    }
}
