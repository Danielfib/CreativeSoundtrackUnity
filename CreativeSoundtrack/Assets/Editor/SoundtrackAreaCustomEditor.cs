using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SoundtrackArea))]
public class SoundtrackAreaCustomEditor : Editor
{
    public Texture2D battleVibeIcon;
    public Texture2D romanticVibeIcon;

    const int ICON_HEIGHT = 40;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20f);
        GUILayout.Label("Trocar entre vibes", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button(battleVibeIcon, GUILayout.Width(ICON_HEIGHT), GUILayout.Height(ICON_HEIGHT)))
        {
            Debug.Log("battle: " + target.name);
        }

        if (GUILayout.Button(romanticVibeIcon, GUILayout.Width(ICON_HEIGHT), GUILayout.Height(ICON_HEIGHT)))
        {
            Debug.Log("romantic: " + target.name);
        }

        GUILayout.EndHorizontal();
    }
}
