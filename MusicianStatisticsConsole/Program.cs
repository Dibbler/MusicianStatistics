// See https://aka.ms/new-console-template for more information
using MusicianStatisticsCore;
using MusicianStatisticsCore.Extensions;
using MusicianStatisticsCore.Models;

bool running = true;
MusicianAPIClient client = new MusicianAPIClient();

Console.WriteLine("Welcome to Musician Statistics!");

while (running)
{
    Console.WriteLine("Enter an artist, or type 'C' to close:");
    string? artist = Console.ReadLine();

    if (artist == null || artist.ToUpper() == "C")
    {
        break;
    }

    ArtistInfo artistInfo = new ArtistInfo(artist);

    //Query the REST API for recordings linked to the artist
    Console.WriteLine($"Searching for tracks associated with artist '{artist}'...");
    artistInfo.Recordings = client.GetRecordings(artist);
    Console.WriteLine($"{artistInfo.Recordings.Count} recording entries found...");

    //Perform analysis if any recordings are found
    if (artistInfo?.Recordings.Count > 0)
    {
        //For each recording, query the REST API for a lyrics entry associated with it
        foreach (string title in artistInfo.Recordings.Select(r => r.title))
        {
            try
            {
                TrackInfo trackInfo = client.GetTrackInfo(artist, title);
                Console.WriteLine($"Found lyrics for '{artist} - {title}'");
                artistInfo.TrackStats.Tracks.Add(trackInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to retrieve lyrics for '{artist} - {title}'");
            }
        }

        if (artistInfo.TrackStats.Tracks.Count == 0)
        {
            Console.WriteLine($"Unable to locate any lyrics data for artist: {artist}");
            continue;
        }

        Console.WriteLine();
        Console.WriteLine();
        //Calculate statistics based on the returned results
        Console.WriteLine($"Average number of words in a track: {artistInfo.TrackStats.AverageLength:0.##}");
        Console.WriteLine($"Maximum number of words in a track: {artistInfo.TrackStats.MaxLength}");
        Console.WriteLine($"Minimum number of words in a track: {artistInfo.TrackStats.MinLength}");
        Console.WriteLine($"Variance: {artistInfo.TrackStats.Variance:0.##}");
        Console.WriteLine($"Standard Deviation: {artistInfo.TrackStats.StandardDeviation:0.##}");
    }

}

