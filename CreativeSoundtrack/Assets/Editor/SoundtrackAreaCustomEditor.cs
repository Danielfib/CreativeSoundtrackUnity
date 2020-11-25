using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SoundtrackArea))]
public class SoundtrackAreaCustomEditor : Editor
{
    public Texture2D battleVibeIcon;
    public Texture2D romanticVibeIcon;
    public Texture2D exploringVibeIcon;
    public Texture2D sadVibeIcon;

    const int ICON_HEIGHT = 40;

    int toolbarIntAux = -1;
    int toolbarInt = 0;

    private static readonly string[] _dontIncludeMe = new string[] { "m_Script" };

    public override void OnInspectorGUI()
    {
        //hide Script property
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
        serializedObject.ApplyModifiedProperties();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open Source Code"))
            Application.OpenURL("https://github.com/Danielfib/CreativeSoundtrackUnity/tree/main/CreativeSoundtrack");
        if (GUILayout.Button("Open Spotify Dashboard"))
            Application.OpenURL("https://developer.spotify.com/dashboard/");
        GUILayout.EndHorizontal();

        GUILayout.Space(20f);
        GUILayout.Label("Trocar entre vibes", EditorStyles.boldLabel);

        Texture2D[] textures = { battleVibeIcon, romanticVibeIcon, exploringVibeIcon, sadVibeIcon };
        toolbarInt = GUILayout.Toolbar(toolbarInt, textures, GUILayout.Height(ICON_HEIGHT));
        if (toolbarIntAux != toolbarInt)
        {
            SoundtrackArea targetSA = (SoundtrackArea)target;
            toolbarIntAux = toolbarInt;
            switch (toolbarInt)
            {
                case 0:
                    targetSA.SetAudioFeatures(0, 0);
                    Debug.Log("battle: " + target.name);
                    break;
                case 1:
                    targetSA.SetAudioFeatures(0, 0);
                    Debug.Log("romantic: " + target.name);
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
    }
}
