﻿using System;
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

    int toolbarIntAux = -1;
    int toolbarInt = 0;

    private static readonly string[] _dontIncludeMe = new string[] { "m_Script" };

    public override void OnInspectorGUI()
    {
        //hide Script property
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
        serializedObject.ApplyModifiedProperties();

        //source code links
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open Source Code"))
            Application.OpenURL("https://github.com/Danielfib/CreativeSoundtrackUnity/tree/main/CreativeSoundtrack");
        if (GUILayout.Button("Open Spotify Dashboard"))
            Application.OpenURL("https://developer.spotify.com/dashboard/");
        GUILayout.EndHorizontal();

        GUILayout.Space(20f);

        //Vibe buttons
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
                    AssignLabel(targetSA.gameObject, battleVibeIcon);
                    break;
                case 1:
                    targetSA.SetAudioFeatures(0, 0);
                    AssignLabel(targetSA.gameObject, romanticVibeIcon);
                    break;
                case 2:
                    targetSA.SetAudioFeatures(0, 0);
                    AssignLabel(targetSA.gameObject, exploringVibeIcon);
                    break;
                case 3:
                    targetSA.SetAudioFeatures(0, 0);
                    AssignLabel(targetSA.gameObject, sadVibeIcon);
                    break;
            }
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
}
