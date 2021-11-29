using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicianStatisticsCore.Models.Recordings;

namespace MusicianStatisticsCore.Models
{
    public class ArtistInfo
    {
        public string Name { get; set; } = "";
        public List<Recording> Recordings { get; set; } = new List<Recording>();
        public TrackStats TrackStats { get; set; }
        public List<Exception> Errors { get; set; } = new List<Exception>();

        public ArtistInfo()
        {
            this.TrackStats = new TrackStats(this);
        }

        /// <summary>
        /// Instantiate an artist info record with the artist name pre-set
        /// </summary>
        /// <param name="artistName"></param>
        public ArtistInfo(string artistName) : this()
        {
            this.Name = artistName;
        }

    }
}
