using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TrackPathManager))]
public class TrackPathManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TrackPathManager myScript = (TrackPathManager)target;
        if (GUILayout.Button("Demo Animation"))
        {
            myScript.DemoAnimation();
        }
    }
}