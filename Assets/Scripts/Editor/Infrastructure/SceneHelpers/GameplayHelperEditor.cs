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
        ShowHelpBox();
        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying))
        {
            DrawDefaultInspector();
            ShowGameSpawner();
            EditorGUILayout.Separator();
            ShowSceneLoader();
            EditorGUILayout.Separator();
            ShowDataPersistentService();
        }
    }
    private void ShowGameSpawner()
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
    
    private void ShowSceneLoader()
    {
        GUILayout.Label("SceneLoader", EditorStyles.boldLabel);
        sceneName = (SceneName)EditorGUILayout.EnumPopup("Scene", sceneName);
        if (GUILayout.Button("LoadScene"))
        {
            myTarget.LoadScene(sceneName);
        }
    }
    private void ShowDataPersistentService()
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

    private void ShowHelpBox()
    {
        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Available only GameMode", MessageType.Warning);
        }
    }
}
