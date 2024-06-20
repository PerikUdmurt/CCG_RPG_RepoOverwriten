using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CCG.Editor
{
    public class SaveLoadWindow : EditorWindow
    {
        private string fileText = "������� �������� ����� ���������� � ������� ������";
        private string targetFileName = "TestData.json";

        [MenuItem("Window/SaveLoadWindow")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(SaveLoadWindow));
        }

        private void OnGUI()
        {
            targetFileName = EditorGUILayout.TextField(targetFileName);
            if(GUILayout.Button("LoadData"))
            {
                fileText = LoadData(targetFileName);
            }
            GUILayout.Label(fileText);
            
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
