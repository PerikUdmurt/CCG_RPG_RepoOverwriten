using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChildrenColorChanger))]
public class ChildrenColorChangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChildrenColorChanger childrenColorChanger = (ChildrenColorChanger)target;
        DrawDefaultInspector();

        if (GUILayout.Button("ChangeColor"))
        {
            childrenColorChanger.ChangeColor();
        }
    }
}
