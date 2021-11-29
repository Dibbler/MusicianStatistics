using Microsoft.AspNetCore.Mvc;
using MusicianStatistics.Models;
using MusicianStatisticsCore;
using MusicianStatisticsCore.Models;
using System.Diagnostics;

namespace MusicianStatistics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(ArtistInfo model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(ArtistInfo model)
        {
            using (MusicianAPIClient client = new MusicianAPIClient())
            {
                try
                {
                    model.Recordings = client.GetRecordings(model.Name);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                if (model.Recordings.Count > 0)
                {
                    //For each recording, query the REST API for a lyrics entry associated with it
                    foreach (string title in model.Recordings.Select(r => r.title))
                    {
                        try
                        {
                            TrackInfo trackInfo = client.GetTrackInfo(model.Name, title);
                            model.TrackStats.Tracks.Add(trackInfo);
                        }
                        catch (Exception ex)
                        {
                            model.Errors.Add(ex);
                        }
                    }

                    if (model.TrackStats.Tracks.Count == 0)
                    {
                        ModelState.AddModelError("", $"No lyrics found for artist {model.Name}");
                    }
                }
            }

            if (model.Recordings.Count == 0)
            {
                ModelState.AddModelError("", $"No recordings found for artist {model.Name}");
            }

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}