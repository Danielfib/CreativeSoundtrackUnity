using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SoundtrackArea : MonoBehaviour
{
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
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Area entered!");
        if(other.tag == "Player")
        {
            PlayerEntered();
        }
    }

    public void PlayerEntered()
    {
        if(CreativeSoundtrackManager.Instance.currentAreaPlaying != id)
        {
            CreativeSoundtrackManager.Instance.currentAreaPlaying = id;
            CreativeSoundtrackManager.Instance.SortByFeatures(//loudness, 
                                                                energy,
                                                                //instrumentalness, 
                                                                //speechiness, 
                                                                valence);
        }
    }
}
