using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEdiorScript : MonoBehaviour
{
    static LevelEditor levelEditor;
    private void Start()
    {
        
    }
    [MenuItem("Level Editor/Save")]
    static void Save()
    {
        levelEditor = FindAnyObjectByType<LevelEditor>();
        levelEditor.Save();
    }
    [MenuItem("Level Editor/Load")]
    static void Load()
    {
        levelEditor = FindAnyObjectByType<LevelEditor>();
        levelEditor.Load();
    }
}
