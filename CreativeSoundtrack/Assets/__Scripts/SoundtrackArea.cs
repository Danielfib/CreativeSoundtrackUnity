using Spotify4Unity.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//[ExecuteInEditMode]
public class SoundtrackArea : MonoBehaviour
{
    List<Track> tracks = new List<Track>();
    private int currentTrackId = 0;

    [HideInInspector]
    public int id;

    private float //loudness,
                  energy,
                  //instrumentalness,
                  // speechiness, 
                  valence;

    private void Awake()
    {
        id = gameObject.GetInstanceID();

        CreativeSoundtrackManager.Instance.AddInitilizationAction(() =>
        {
            new Thread(() =>
            {
                tracks.AddRange(CreativeSoundtrackManager.Instance.GetBestSongsFor(energy, valence, 5));
                Debug.Log("InitializedArea");
            }).Start();
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Area entered!");
            PlayerEntered();
        }
    }

    public void PlayerEntered()
    {
        if (CreativeSoundtrackManager.Instance.EnteredNewArea(id))
        {
            CreativeSoundtrackManager.Instance.PlayTrack(tracks[0], PlayNextSong);
        }
    }

    //called when current sont is about to end
    public void PlayNextSong()
    {
        int randomIndex = Mathf.RoundToInt(Random.Range(0, tracks.Count - 1));
        if(tracks.Count > 1)
        {
            while(randomIndex == currentTrackId)
            {
                randomIndex = Mathf.RoundToInt(Random.Range(0, tracks.Count - 1));
            }
        }

        var nextSong = tracks[randomIndex];
        CreativeSoundtrackManager.Instance.PlayTrack(nextSong, PlayNextSong);
        currentTrackId = randomIndex;
    }

    public void SetAudioFeatures(float energy, float valence)
    {
        this.energy = energy;
        this.valence = valence;
    }
}
