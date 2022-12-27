using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Circulation))]
public class AfficheSpline : Editor
{
    private bool ifDrawSpline = true;

    public override void OnInspectorGUI()
    {
        Circulation circulation = (Circulation)target;

        if (GUILayout.Button("Draw Spline"))
            ifDrawSpline = false;

        if (!ifDrawSpline)
        {
            circulation.DrawSpine();
            ifDrawSpline = true;
        }

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("intersection"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
