using CCG.Infrastructure.SceneHelper;
using CCG.Services.SceneLoader;
using CCG.StaticData.Cards;
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GamePlaySceneHelper))]
public class GameplayHelperEditor : Editor
{
    private GamePlaySceneHelper myTarget;
    private CardType cardType;
    private int maxCount;
    private int prepareCardSlot;
    public override void OnInspectorGUI()
    {
        
        myTarget = (GamePlaySceneHelper)target;
        ShowHelpBox();
        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying))
        {
            DrawDefaultInspector();
            ShowGameSpawner();
            EditorGUILayout.Separator();
            ShowCardReciever();
            EditorGUILayout.Separator();
            ShowDataPersistentService();
        }
    }

    private void ShowCardReciever()
    {
        GUILayout.Label("GameSpawner", EditorStyles.boldLabel);
        maxCount = EditorGUILayout.IntField(maxCount);
        prepareCardSlot = EditorGUILayout.IntField(prepareCardSlot);
        if (GUILayout.Button("StartCardReciever"))
        {
            myTarget.StartCardReciever(maxCount, prepareCardSlot);
        }
        if (GUILayout.Button("StopCardReciever"))
        {
            myTarget.StopCardReciever();
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
