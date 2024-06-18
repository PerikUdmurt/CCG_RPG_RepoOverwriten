using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tools
{
    [MenuItem("Tools/Clear prefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
