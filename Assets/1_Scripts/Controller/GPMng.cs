using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GPMng : MonoBehaviour
{
    public static GPMng inst;
    public bool IsPlaying = false;

    public UnityAction onLoadLevelDone;

    public List<ItemEditor> itemPrefab;

    GameConfig gameConfig => Resources.Load<GameConfig>("Config/Game config");

    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        if (gameConfig.levelSet > 0)
        {
            LevelMng.inst.LevelPlaying = gameConfig.levelSet;
            gameConfig.levelSet = 0;
        }
        if (gameConfig.stageSet > 0)
        {
            LevelMng.inst.StagePlaying = gameConfig.stageSet;
            gameConfig.stageSet = 0;
        }

        StartCoroutine(LoadLevelPlaying());
    }
    IEnumerator LoadLevelPlaying()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelMng.inst.LevelPlaying.ToString(), LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        var items = FindObjectsOfType<ItemEditor>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].LoadFrefab();
        }
        //onLoadLevelDone?.Invoke();

        LevelMng.inst.LoadLevel();
        DrawingPhysics.inst.Initialize();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        IsPlaying = true;
    }
    public void Win()
    {
        IsPlaying = false;
        UIMng.inst.Win();
        LevelMng.inst.LevelPlaying++;
        if (LevelMng.inst.LevelPlaying > LevelMng.inst.LevelUnlocked)
        LevelMng.inst.LevelUnlocked = LevelMng.inst.LevelPlaying;
        LevelMng.inst.LevelPlaying ++;

    }
    public void Lose()
    {
        IsPlaying = false;
        UIMng.inst.Lose();
    }
}
