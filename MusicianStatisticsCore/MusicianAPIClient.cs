using MusicianStatisticsCore.Models;
using System.Net.Http.Headers;
using static MusicianStatisticsCore.Models.Recordings;

namespace MusicianStatisticsCore
{
    public class MusicianAPIClient : IDisposable
    {
        private HttpClient _musicBrainzClient;
        private HttpClient _lyricsovhClient;

        protected ProductInfoHeaderValue _productValue = new ProductInfoHeaderValue("MusicianAPIClient", "1.0");
        protected ProductInfoHeaderValue _commentValue = new ProductInfoHeaderValue("(+Example musician details poller)");

        protected const string MUSICBRAINZ_BASEURI = "https://musicbrainz.org/ws/2/";
        protected const string LYRICSOVH_BASEURI = "https://api.lyrics.ovh/v1/";

        /// <summary>
        /// Instantiate the REST API client with non-default service URIs
        /// </summary>
        /// <param name="musicBrainzBaseUri">The base URI of the Music Brainz API</param>
        /// <param name="lyricsovhBaseUri">The base URI of the Lyrics OVH API<</param>
        public MusicianAPIClient(string musicBrainzBaseUri = MUSICBRAINZ_BASEURI, string lyricsovhBaseUri = LYRICSOVH_BASEURI)
        {
            _musicBrainzClient = GetHttpClient(musicBrainzBaseUri);
            _lyricsovhClient = GetHttpClient(lyricsovhBaseUri);
        }

        public MusicianAPIClient()
        {
            _musicBrainzClient = GetHttpClient(MUSICBRAINZ_BASEURI);
            _lyricsovhClient = GetHttpClient(LYRICSOVH_BASEURI);
        }

        /// <summary>
        /// Instantiate a HttpClient with the target base URI and pre-defined user agent headers
        /// </summary>
        /// <param name="baseUri"></param>
        private HttpClient GetHttpClient(string baseUri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.UserAgent.Add(_productValue);
            client.DefaultRequestHeaders.UserAgent.Add(_commentValue);

            return client;
        }


        /// <summary>
        /// Return lyrics information from the Lyrics OVH API
        /// </summary>
        /// <param name="artist">Artist name</param>
        /// <param name="trackTitle">Song title</param>
        /// <returns></returns>
        public TrackInfo GetTrackInfo(string artist, string trackTitle)
        {
            TrackInfo trackLyrics;
            string targetUri = $"{Uri.EscapeDataString(artist)}/{Uri.EscapeDataString(trackTitle)}";

            Task<HttpResponseMessage> response = _lyricsovhClient.GetAsync(targetUri);
            response.Wait();

            HttpResponseMessage responseResult = response.Result;
            if (responseResult.IsSuccessStatusCode)
            {
                Task<TrackInfo> readTask = responseResult.Content.ReadAsAsync<TrackInfo>();
                readTask.Wait();

                trackLyrics = readTask.Result;
                trackLyrics.Title = trackTitle;
                return trackLyrics;
            }
            else
            {
                throw ParseErrorResponse(responseResult);
            }
        }

        /// <summary>
        /// Returns all known recordings associated with the artist from the Music Brainz API
        /// </summary>
        /// <param name="artist">Artist name</param>
        /// <returns></returns>
        public List<Recording> GetRecordings(string artist)
        {
            List<Recording> recordings = new List<Recording>();
            int offset = 0;
            int pageSize = 100;
            int rateLimitMS = 1000; //MusicBrainz only allows 1 call per second
            bool retrievingRecords = true;

            while (retrievingRecords)
            {
                string targetUri = $"recording?query=artist:{Uri.EscapeDataString(artist)} AND status:official and type:album&limit={pageSize}&offset={offset}&fmt=json";

                Task<HttpResponseMessage> response = _musicBrainzClient.GetAsync(targetUri);
                response.Wait();

                HttpResponseMessage responseResult = response.Result;
                if (responseResult.IsSuccessStatusCode)
                {
                    Task<Recordings> readTask = responseResult.Content.ReadAsAsync<Recordings>();
                    readTask.Wait();
                    
                    //If we've reached the end of the results, stop
                    if (readTask.Result.recordings.Count == 0)
                    {
                        retrievingRecords = false;
                        continue;
                    }

                    recordings.AddRange(readTask.Result.recordings.DistinctBy(r => r.title.Trim().ToUpper()).ToList());
                }
                else
                {
                    throw ParseErrorResponse(responseResult);
                }

                offset = offset + pageSize;
                Task.Delay(rateLimitMS).Wait();
            }

            return recordings.DistinctBy(r => r.title.Trim().ToUpper()).ToList();
        }

        /// <summary>
        /// Parses an error response and returns a new exception containing the response content
        /// </summary>
        protected Exception ParseErrorResponse(HttpResponseMessage response)
        {
            //Get error response data
            Task<string> readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();

            string errorString = readTask.Result;
            return new Exception(errorString);
        }

        public void Dispose()
        {
            _musicBrainzClient.Dispose();
            _lyricsovhClient.Dispose();
        }
    }
}