using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageCard : MonoBehaviour
{
    public int ID;
    [SerializeField] bool isPassed = false;
    [SerializeField] Text txtLv;
    [SerializeField] Image image;
    Button button;
    
    [SerializeField] Sprite sprPassed;
    [SerializeField] Sprite sprNotPassed;
    public bool IsPassed
    {
        set {
            isPassed = value;
            image.sprite = value ? sprPassed : sprNotPassed;
        }
        get => isPassed;
    }
    public void Setdata(int level, bool _isPassed)
    {
        gameObject.SetActive(true);
        ID = level;
        txtLv.text = level.ToString();
        IsPassed = _isPassed;
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            LevelMng.inst.LevelPlaying = ID;
            GPMng.inst.ReloadScene();
        });
    }
}