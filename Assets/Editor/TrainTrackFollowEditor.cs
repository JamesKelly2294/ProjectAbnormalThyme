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
            myScript.PullUpToStart();
        }
        if (GUILayout.Button("Go bitch, GO!"))
        {
            myScript.targetTrain.IsBrakingFullStop = false;
            myScript.targetTrain.targetSpeed = 2.0f;
            myScript.targetTrain.brakingPower = 0.0f;
        }
    }
}