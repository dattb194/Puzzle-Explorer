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

        Initialize();
        StartCoroutine(LoadLevelPlaying());
    }
    void Initialize()
    {
        if (gameConfig.deleteAllPP)
        {
            PlayerPrefs.DeleteAll();
            gameConfig.deleteAllPP = false;
        }
        if (gameConfig.levelSet > 0)
        {
            LevelMng.inst.LevelPlaying = gameConfig.levelSet;

            if (LevelMng.inst.LevelPlaying > LevelMng.inst.LevelUnlocked)
                LevelMng.inst.LevelUnlocked = LevelMng.inst.LevelPlaying;

            gameConfig.levelSet = 0;
        }
        if (gameConfig.stageSet > 0)
        {
            LevelMng.inst.StagePlaying = gameConfig.stageSet;
            gameConfig.stageSet = 0;
        }
        GCMng.inst.Load();
    }
    IEnumerator LoadLevelPlaying()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelMng.inst.LevelPlaying.ToString(), LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        var items = FindObjectsOfType<ItemEditor>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].LoadFrefab();
        }
        LevelMng.inst.LoadLevel();
        DrawingPhysics.inst.Initialize();
        UIMng.inst.ShowMenuGame();
    }
    public void SkipLevel()
    {
        LevelMng.inst.LevelPlaying++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }
    public void Lose()
    {
        IsPlaying = false;
        UIMng.inst.Lose();
    }
}
