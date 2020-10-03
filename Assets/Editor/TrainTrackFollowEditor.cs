using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrainTrackFollow))]
public class TrainTrackFollowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TrainTrackFollow myScript = (TrainTrackFollow)target;
        if (GUILayout.Button("Demo Animation"))
        {
            myScript.DemoAnimation();
        }
    }
}