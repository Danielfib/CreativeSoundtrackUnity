using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//[ExecuteInEditMode]
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
        if(other.tag == "Player")
        {
            Debug.Log("Area entered!");
            PlayerEntered();
        }
    }

    public void PlayerEntered()
    {
        new Thread(() =>
        {
            CreativeSoundtrackManager.Instance.EnteredNewArea(id, energy, valence);
        }).Start();
    }
}
