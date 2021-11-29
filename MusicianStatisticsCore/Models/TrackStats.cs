using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianStatisticsCore.Models
{
    public class TrackStats
    {
        ArtistInfo ArtistInfo { get; set; }
        public List<TrackInfo> Tracks { get; set; } = new List<TrackInfo>();

        /// <summary>
        /// Return a specific track info
        /// </summary>
        public TrackInfo this[int i]
        {
            get => Tracks[i];
            set => Tracks[i] = value;
        }


        public TrackStats(ArtistInfo artistInfo)
        {
            this.ArtistInfo = artistInfo;
        }

        /// <summary>
        /// Return the maximum word count for the available tracks
        /// </summary>
        [Display(Name = "Maximum Length")]
        public int MaxLength
        {
            get
            {
                if (this.Tracks.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.Tracks.Select(r => r.WordCount).Max();
                }
            }
        }

        /// <summary>
        /// Return the minimum word count for the available tracks
        /// </summary>
        [Display(Name = "Minimum Length")]
        public int MinLength
        {
            get
            {
                if (this.Tracks.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.Tracks.Select(r => r.WordCount).Min();
                }
            }
        }

        /// <summary>
        /// Return the average number of words in the available tracks
        /// </summary>
        [Display(Name = "Average Length")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double AverageLength
        {
            get
            {
                if (this.Tracks.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.Tracks.Select(r => r.WordCount).Average();
                }
            }
        }

        /// <summary>
        /// Return the variance of word counts in the available tracks
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Variance
        {
            get
            {
                return Calculator.Variance(this.Tracks.Select(t => (double)t.WordCount));
            }
        }

        /// <summary>
        /// Return the standard deviation of word counts in the available tracks
        /// </summary>
        [Display(Name = "Standard Deviation")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double StandardDeviation
        {
            get
            {
                return Calculator.StandardDeviation(this.Tracks.Select(t => (double)t.WordCount));
            }
        }
    }
}
