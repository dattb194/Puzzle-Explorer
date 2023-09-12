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

    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelMng.inst.ID.ToString(), LoadSceneMode.Additive);

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
        UIMng.inst.SetbtnsDraw();
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
    }
    public void Lose()
    {
        IsPlaying = false;
        UIMng.inst.Lose();
    }
}
