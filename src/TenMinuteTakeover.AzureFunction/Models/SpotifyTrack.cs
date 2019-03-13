namespace TenMinuteTakeover.AzureFunction.Models
{
    class SpotifyTrack
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string[] FeaturingArtist { get; set; } = new string [0];
    }
}
