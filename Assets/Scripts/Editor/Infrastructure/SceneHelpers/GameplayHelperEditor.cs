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
            EditorGUILayout.Separator();
            SceneLoader();
            EditorGUILayout.Separator();
            DataPersistentService();
        }
    }
    private void GameSpawner()
    {
        GUILayout.Label("GameSpawner", EditorStyles.boldLabel) ;
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
        GUILayout.Label("SceneLoader", EditorStyles.boldLabel);
        sceneName = (SceneName)EditorGUILayout.EnumPopup("Scene", sceneName);
        if (GUILayout.Button("LoadScene"))
        {
            myTarget.LoadScene(sceneName);
        }
    }
    [Separator]
    private void DataPersistentService()
    {
        GUILayout.Label("DataPersistentService", EditorStyles.boldLabel);
        if (GUILayout.Button("NewProgress"))
        {
            myTarget.NewProgress();
        }
        if (GUILayout.Button("SaveProgress"))
        {
            myTarget.SaveProgress();
        }
        if (GUILayout.Button("LoadProgress"))
        {
            myTarget.LoadProgress();
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
