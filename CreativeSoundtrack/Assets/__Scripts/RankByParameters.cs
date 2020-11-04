using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankByParameters : MonoBehaviour
{
    [Range(0,1), SerializeField]
    private float loudness, energy, instrumentalness, speechiness;

    public void DoRank()
    {
        GameObject.FindObjectOfType<ExampleTracksController>().SortByFeatures(loudness, energy, instrumentalness, speechiness);
    }
}
