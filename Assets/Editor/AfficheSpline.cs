using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spline))]
public class AfficheSpline : Editor
{
    public override void OnInspectorGUI()
    {
        Spline spline = (Spline)target;

        if (GUILayout.Button("Draw Spline"))
            spline.DrawSpine();
            
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        base.OnInspectorGUI();
    }
}
