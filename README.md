# Musician Statistics

## Running this solution
The solution can either be executed as a console or MVC web application. The console application can be executed from the build directory, or set the preferred application as the startup project in Visual Studio and run.

This solution consists of four .NET Core projects:

## MusicianStatisticsCore
This contains the bulk of the business and data layer of the solution, including an API client to connect to MusicBrainz and Lyrics OVH to retrieve data as the models and calculator to produce artist statistics.

## MusicianStatisticsConsole
A console application which when given an artist as input will lookup tracks that artist has produced, then calculate statistics about the lyrical content of those tracks.

## MusicianStatisticsWeb
A MVC web app which allows a user to enter an artist as input and will lookup tracks that artist has produced, then calculate statistics about the lyrical content of those tracks.

## MusicianStatistics.Test
A MSTest project containing some unit tests relating to the track info calculation methods.
