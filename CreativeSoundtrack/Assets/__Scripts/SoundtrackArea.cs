using Spotify4Unity.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//[ExecuteInEditMode]
public class SoundtrackArea : MonoBehaviour
{
    List<Track> tracks = new List<Track>();

    [HideInInspector]
    public int id;

    [Range(0, 1), SerializeField]
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
            CreativeSoundtrackManager.Instance.PlayTrack(tracks[0]);
        }
    }
}
