using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianStatisticsCore.Models
{
    public class Recordings
    {

        public DateTime created { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        public List<Recording> recordings { get; set; }

        public class Artist
        {
            public string id { get; set; }
            public string name { get; set; }

            [JsonProperty("sort-name")]
            public string SortName { get; set; }
            public string disambiguation { get; set; }
        }

        public class ArtistCredit
        {
            public string name { get; set; }
            public Artist artist { get; set; }
        }

        public class ReleaseGroup
        {
            public string id { get; set; }
            public string title { get; set; }

            [JsonProperty("secondary-types")]
            public List<string> SecondaryTypes { get; set; }

            [JsonProperty("secondary-type-ids")]
            public List<string> SecondaryTypeIds { get; set; }
        }

        public class Track
        {
            public string id { get; set; }
            public string number { get; set; }
            public string title { get; set; }
        }

        public class Medium
        {
            public int position { get; set; }
            public string format { get; set; }
            public List<Track> track { get; set; }

            [JsonProperty("track-count")]
            public int TrackCount { get; set; }

            [JsonProperty("track-offset")]
            public int TrackOffset { get; set; }
        }

        public class Release
        {
            public string id { get; set; }

            [JsonProperty("status-id")]
            public string StatusId { get; set; }
            public int count { get; set; }
            public string title { get; set; }
            public string status { get; set; }

            [JsonProperty("release-group")]
            public ReleaseGroup ReleaseGroup { get; set; }

            [JsonProperty("track-count")]
            public int TrackCount { get; set; }
            public List<Medium> media { get; set; }
        }

        public class Recording
        {
            public string id { get; set; }
            public int score { get; set; }
            public string title { get; set; }
            public object video { get; set; }

            [JsonProperty("artist-credit")]
            public List<ArtistCredit> ArtistCredit { get; set; }
            public List<Release> releases { get; set; }
        }

    }
}
