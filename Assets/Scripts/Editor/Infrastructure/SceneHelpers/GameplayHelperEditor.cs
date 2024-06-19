using CCG.Infrastructure.SceneHelper;
using CCG.Services.SceneLoader;
using CCG.StaticData.Cards;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GamePlaySceneHelper))]
public class GameplayHelperEditor : Editor
{
    private GamePlaySceneHelper myTarget;
    private CardType cardType;
    private SceneName sceneName;
    public override void OnInspectorGUI()
    {
        
        myTarget = (GamePlaySceneHelper)target;

        HelpBox();
        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying))
        {
            DrawDefaultInspector();
            GameSpawner();
            SceneLoader();
        }
    }

    private void GameSpawner()
    {
        GUILayout.Label("GameSpawner") ;
        cardType = (CardType)EditorGUILayout.EnumPopup("CardType", cardType);
        if (GUILayout.Button("SpawnCard"))
        {
            myTarget.SpawnCard(cardType);
        }

        if (GUILayout.Button("SpawnCardSlot"))
        {
            myTarget.SpawnCardSlot();
        }
    }

    private void SceneLoader()
    {
        GUILayout.Label("SceneLoader");
        sceneName = (SceneName)EditorGUILayout.EnumPopup("Scene", sceneName);
        if (GUILayout.Button("LoadScene"))
        {
            myTarget.LoadScene(sceneName);
        }
    }

    private void HelpBox()
    {
        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Available only GameMode", MessageType.Warning);
        }
    }
}
