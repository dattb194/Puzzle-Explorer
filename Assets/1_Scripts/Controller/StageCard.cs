using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageCard : MonoBehaviour
{
    public int ID;
    [SerializeField] bool isUnlocked = false;
    [SerializeField] Text txtLv;
    [SerializeField] Image image;
    [SerializeField] GameObject objlock;
    Button button;

    [SerializeField] Sprite sprPassed;
    [SerializeField] Sprite sprNotPassed;
    public bool IsUnlocked
    {
        set {
            isUnlocked = value;
            image.sprite = value ? sprPassed : sprNotPassed;
            objlock.SetActive(!value);
        }
        get => isUnlocked;
    }
    public void Setdata(int level, bool _isPassed)
    {
        gameObject.SetActive(true);
        ID = level;
        txtLv.text = level.ToString();
        IsUnlocked = _isPassed;

        button = GetComponent<Button>();
        if (LevelMng.inst.LevelPlaying == ID || !IsUnlocked)
        {
            button.interactable = false;
            return;
        }

        button.interactable = true;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(()=>
        {
            LevelMng.inst.LevelPlaying = ID;
            GPMng.inst.ReloadScene();
        });
    }
}