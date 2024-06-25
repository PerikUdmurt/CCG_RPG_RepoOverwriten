using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ShowButtonAttribute))]
public class ButtonDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        if (GUILayout.Button("Text"))
        {
            
        }
    }
}

