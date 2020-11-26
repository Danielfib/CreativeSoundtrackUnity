using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    private static readonly string[] _dontIncludeMe = new string[] { "m_Script" };

    public override void OnInspectorGUI()
    {
        SoundtrackArea targetSA = (SoundtrackArea)target;
        int toolbarInt = targetSA.toolbarInt;
        int toolbarIntAux = targetSA.toolbarIntAux;

        //hide Script property
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
        serializedObject.ApplyModifiedProperties();

        //source code links
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open Source Code"))
            Application.OpenURL("https://github.com/Danielfib/CreativeSoundtrackUnity");
        if (GUILayout.Button("Open Spotify Dashboard"))
            Application.OpenURL("https://developer.spotify.com/dashboard/");
        GUILayout.EndHorizontal();

        GUILayout.Space(20f);

        //Vibe buttons
        GUILayout.Label("Choose area's vibe:", EditorStyles.boldLabel);
        Texture2D[] textures = { battleVibeIcon, romanticVibeIcon, exploringVibeIcon, sadVibeIcon };
        toolbarInt = GUILayout.Toolbar(toolbarInt, textures, GUILayout.Height(ICON_HEIGHT));
        if (toolbarIntAux != toolbarInt)
        {
            toolbarIntAux = toolbarInt;
            switch (toolbarInt)
            {
                case 0:
                    targetSA.SetAudioFeatures(1, 0.4f);
                    AssignLabel(targetSA.gameObject, battleVibeIcon);
                    break;
                case 1:
                    targetSA.SetAudioFeatures(0.8f, 0.8f);
                    AssignLabel(targetSA.gameObject, romanticVibeIcon);
                    break;
                case 2:
                    targetSA.SetAudioFeatures(0.5f, 0.3f);
                    AssignLabel(targetSA.gameObject, exploringVibeIcon);
                    break;
                case 3:
                    targetSA.SetAudioFeatures(0, 0);
                    AssignLabel(targetSA.gameObject, sadVibeIcon);
                    break;
            }
            targetSA.selectedVibeColor = getVibeColor(toolbarInt);
            targetSA.toolbarInt = toolbarInt;
            targetSA.toolbarIntAux = toolbarIntAux;
        }
    }

    public void AssignLabel(GameObject g, Texture2D tex = null)
    {
        if(tex == null)
            tex = EditorGUIUtility.IconContent("sv_label_0").image as Texture2D;

        Type editorGUIUtilityType = typeof(EditorGUIUtility);
        BindingFlags bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        object[] args = new object[] { g, tex };
        editorGUIUtilityType.InvokeMember("SetIconForObject", bindingFlags, null, null, args);
    }

    private Color getVibeColor(int vibeSelected)
    {
        Color vibeColor = Color.red;

        switch (vibeSelected)
        {
            case 0: //combat
                vibeColor = new Color(231f / 255f, 228f / 255f, 90f / 255f, 1);
                break;
            case 1: //romance
                vibeColor = new Color(191f / 255f, 98f / 255f, 98f / 255f, 1);
                break;
            case 2: //adventure
                vibeColor = new Color(99f / 255f, 192f / 255f, 183f / 255f, 1);
                break;
            case 3: //sad
                vibeColor = new Color(50f / 255f, 69f / 255f, 151f / 255f, 1);
                break;
        }
        return vibeColor;
    }
}
