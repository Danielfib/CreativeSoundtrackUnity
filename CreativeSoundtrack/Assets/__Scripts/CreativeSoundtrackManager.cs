using Spotify4Unity;
using Spotify4Unity.Dtos;
using SpotifyAPI.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class CreativeSoundtrackManager : Singleton<CreativeSoundtrackManager>
{
    [SerializeField]
    private SpotifyService spotifyService;
    private List<Track> m_tracks = null;

    [HideInInspector]
    public int currentAreaPlaying = -1;

    private void Start()
    {
        Thread initThread = new Thread(Initializate);
        initThread.Start();
    }

    private void Connect()
    {
        bool didAttempt = spotifyService.Connect();
        Debug.Log("Connected? : " + didAttempt);
    }

    public void GetAllUserTracks()
    {
        m_tracks = spotifyService.GetSavedTracks();
    }

    private void Initializate()
    {
        Connect();

        while (m_tracks == null)
        {
            Thread.Sleep(500);
            GetAllUserTracks();
            Debug.Log("Trying to get users song!");
        }
        Debug.Log("Got user songs!");
    }

    public void EnteredNewArea(int areaId, float energy, float valence)
    {
        if (currentAreaPlaying != areaId && m_tracks != null)
        {
            currentAreaPlaying = areaId;
            SortByFeatures(//loudness, 
                            energy,
                            //instrumentalness, 
                            //speechiness, 
                            valence);
            Play();
        }
    }

    public void SortByFeatures(//float loudness, 
                                float energy,
                                //float instrumentalness, 
                                //float speechiness, 
                                float valence)
    {
        var audioFeatures = new List<SpotifyAPI.Web.Models.AudioFeatures>();

        List<string> tracksIds = m_tracks.Select(x => x.TrackId).ToList();
        for (var i = 0; i < tracksIds.Count; i += 100)
        {
            int howMany = Math.Min(100, tracksIds.Count - i - 1);
            var sublist = tracksIds.GetRange(i, howMany);
            var af = spotifyService.GetSeveralAudioFeatures(sublist).AudioFeatures;
            audioFeatures.AddRange(af);
        }

        List<Tuple<Track, float>> trackGrades = new List<Tuple<Track, float>>();
        int count = 0;
        foreach (var f in audioFeatures)
        {
            //smaller grades are better (less different from the parameters chosen)
            float grade = (//Math.Abs(f.Loudness - loudness) +
                           Math.Abs(f.Energy - energy) +
                           //Math.Abs(f.Instrumentalness - instrumentalness) +
                           //Math.Abs(f.Speechiness - speechiness) +
                           Math.Abs(f.Valence - valence)
                           );

            trackGrades.Add(new Tuple<Track, float>(m_tracks[count], grade));
            trackGrades.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            count++;
        }

        m_tracks = trackGrades.Select(x => x.Item1).ToList();
    }

    private async void Play()
    {
        Debug.Log("Playing " + m_tracks[0].Title + " in " + spotifyService.ActiveDevice?.Name);
        //spotifyService.PlaySong(m_tracks[0].TrackId);
        await spotifyService.PlayTrackAsync(m_tracks[0]);
        //await spotifyService.PlayAsync();
    }
}
