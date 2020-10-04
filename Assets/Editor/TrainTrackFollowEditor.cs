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
        if (GUILayout.Button("Go bitch, GO!"))
        {
            myScript.targetTrain.isBrakingFullStop = false;
            myScript.targetTrain.targetSpeed = 2.0f;
            myScript.targetTrain.brakingPower = 0.0f;
        }
    }
}