using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChampionCreator
{
#if UNITY_EDITOR
    [MenuItem("BoC/Create Champion")]
    public static void CreateChampion()
    {
        ChampionData championData = ScriptableObject.CreateInstance<ChampionData>();
        AssetDatabase.CreateAsset(championData, "Assets/Resources/Champions/New Champion.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = championData;

    }
#endif
}
