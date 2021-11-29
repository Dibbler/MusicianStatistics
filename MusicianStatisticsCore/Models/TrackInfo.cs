using MusicianStatisticsCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianStatisticsCore.Models
{
    public class TrackInfo
    {
        private string? _lyrics;
        private int _wordCount;

        public string Title = "Unknown";
        public int WordCount { get { return _wordCount; } }

        public string? Lyrics
        {
            get
            {
                return _lyrics;
            }
            set
            {
                _lyrics = value;
                if (value != null)
                {
                    _wordCount = _lyrics.WordCount(); //Calculate wordcount when lyrics are set to avoid unnecessary recalculation
                }
            }
        }

    }
}
