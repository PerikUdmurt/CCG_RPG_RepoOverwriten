using CCG.Editor;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SeparatorAttribute))]
public class SeparatorDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        SeparatorAttribute separatorAttribute = attribute as SeparatorAttribute;
        Rect separatorRect = new Rect(position.xMin, 
            position.yMin + separatorAttribute.Spacing, 
            position.width,
            separatorAttribute.Height);

        EditorGUI.DrawRect(separatorRect, color: Color.white);
    }

    public override float GetHeight()
    {
        SeparatorAttribute separatorAttribute = attribute as SeparatorAttribute;
        float totalSpacing = separatorAttribute.Spacing + separatorAttribute.Height +separatorAttribute.Spacing;
        return totalSpacing;
    }
}
