using CCG.Gameplay;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Card myTarget = (Card)target;
        EditorGUILayout.LabelField("CardID", myTarget.CardID.ToString());
        EditorGUILayout.LabelField("Name", myTarget.Name);
        EditorGUILayout.LabelField("Description", myTarget.CardDescription);
        EditorGUILayout.LabelField("DeckType", myTarget.DeckType.ToString());
        EditorGUILayout.LabelField("Value", myTarget.ValueOfCard.ToString());
        EditorGUILayout.LabelField("CurrentState", myTarget.StateMachine.CurrentState.ToString());
        EditorGUILayout.LabelField("Usable", myTarget.Usable.isUsable.ToString());
        EditorGUILayout.LabelField("Selectable", myTarget.Selectable.isSelectable.ToString());
        EditorGUILayout.LabelField("Dragable", myTarget.Dragable.isDragable.ToString());
    }
}
