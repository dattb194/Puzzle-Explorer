using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/ Game config", fileName = "Game config")]
public class GameConfig : ScriptableObject
{
    public int levelSet;
    public int stageSet;
}