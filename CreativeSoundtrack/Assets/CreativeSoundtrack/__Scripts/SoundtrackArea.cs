﻿using Spotify4Unity.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class SoundtrackArea : MonoBehaviour
{
    List<Track> tracks = new List<Track>();
    private int currentTrackId = 0;

    [HideInInspector]
    public int id;

    [HideInInspector, SerializeField]
    private float energy, valence;

    #region Editor
    [HideInInspector, SerializeField]
    public Color selectedVibeColor;
    [HideInInspector, SerializeField]
    public int toolbarIntAux = -1, toolbarInt = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = selectedVibeColor;
        BoxCollider bc = GetComponent<BoxCollider>();
        Vector3 scaledBounds = Vector3.Scale(bc.size, transform.localScale);
        Gizmos.DrawWireCube(transform.position + bc.center, scaledBounds);
    }

    public void SetAudioFeatures(float energy, float valence)
    {
        this.energy = energy;
        this.valence = valence;
    }
    #endregion

    private void Start()
    {
        //Debug.Log("Start new area!");
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
        if (other.tag == "Player")
        {
            PlayerEntered();
        }
    }

    public void PlayerEntered()
    {
        if (CreativeSoundtrackManager.Instance.EnteredNewArea(id))
        {
            Debug.Log("Playing song with: " + energy + " " + valence);
            CreativeSoundtrackManager.Instance.PlayTrack(tracks[0], PlayNextSong);
        }
    }

    //called when current sont is about to end
    public void PlayNextSong()
    {
        int randomIndex = Mathf.RoundToInt(Random.Range(0, tracks.Count - 1));
        if (tracks.Count > 1)
        {
            while (randomIndex == currentTrackId)
            {
                randomIndex = Mathf.RoundToInt(Random.Range(0, tracks.Count - 1));
            }
        }

        var nextSong = tracks[randomIndex];
        CreativeSoundtrackManager.Instance.PlayTrack(nextSong, PlayNextSong);
        currentTrackId = randomIndex;
    }
}