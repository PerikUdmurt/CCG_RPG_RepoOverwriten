using System;
using CCG.Data;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CCG.Editor
{
    public class SaveLoadWindow : EditorWindow
    {
        private GameData gameData;
        private string fileText = "������� �������� ����� ���������� � ������� ������";
        private string targetFileName = "TestData.json";

        [MenuItem("Window/SaveLoadWindow")]
        public static void ShowWindow()
        {
            GetWindow(typeof(SaveLoadWindow));
        }

        private void OnGUI()
        {
            
            targetFileName = EditorGUILayout.TextField(targetFileName);
            if(GUILayout.Button("LoadData"))
            {
                fileText = LoadData(targetFileName);
                gameData = fileText.ToDeserialized<GameData>();
            }

            EditorGUILayout.LabelField(fileText, EditorStyles.wordWrappedLabel);
        }

        public string LoadData(string fileName)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, fileName);
            string loadedData = "������ ���� �� ������ � ����������";
            if (File.Exists(fullPath))
            {
                try
                {
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            loadedData = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception)
                {
                    return "������ ��� ������� �������� ������ �� �����";
                }
            }
            return loadedData;
        }
    }
}
