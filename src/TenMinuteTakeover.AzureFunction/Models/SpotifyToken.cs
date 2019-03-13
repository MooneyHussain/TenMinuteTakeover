using Newtonsoft.Json;

namespace TenMinuteTakeover.AzureFunction.Models
{
    class SpotifyToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string Type { get; set; }
    }
}
