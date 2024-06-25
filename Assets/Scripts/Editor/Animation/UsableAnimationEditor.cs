using CCG.Animation;
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UsableAnimation))]
public class UsableAnimationEditor : Editor
{
    private UsableAnimation myTarget;
    public override void OnInspectorGUI()
    {
        myTarget = (UsableAnimation)target;
        DrawDefaultInspector();

        if (GUILayout.Button("PlayAnimation"))
        {
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    { 
        Type myType = typeof(UsableAnimation);
        var PlayAnimationMethod = myType.GetMethod("PlayUseAnimation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        PlayAnimationMethod?.Invoke(myTarget, null);
    }
}
