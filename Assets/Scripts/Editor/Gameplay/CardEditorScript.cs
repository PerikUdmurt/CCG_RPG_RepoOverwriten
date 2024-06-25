using CCG.Gameplay;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Card myTarget = (Card)target;
        ShowCardInfo(myTarget);
        ShowCardAvailAbility(myTarget);
    }

    private void ShowCardInfo(Card target)
    {
        EditorGUILayout.LabelField("CardInfo", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("CardID", target.CardID.ToString());
        EditorGUILayout.LabelField("Name", target.Name);
        EditorGUILayout.LabelField("Description", target.CardDescription);
        EditorGUILayout.LabelField("DeckType", target.DeckType.ToString());
        EditorGUILayout.LabelField("Value", target.ValueOfCard.ToString());
        EditorGUILayout.LabelField("CurrentState", target.StateMachine?.CurrentState.ToString());
    }

    private void ShowCardAvailAbility(Card target)
    {
        EditorGUILayout.LabelField("CardInfo", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Usable", target.Usable.isUsable.ToString());
        EditorGUILayout.LabelField("Selectable", target.Selectable.isSelectable.ToString());
        EditorGUILayout.LabelField("Dragable", target.Dragable.isDragable.ToString());
    }
}
