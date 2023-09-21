using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GCMng : MonoBehaviour
{
    public static GCMng inst;
    List<string> rows;

    public List<SkinInfo> listSkin;

    private void Awake()
    {
        inst = this;
    }
    public void Load()
    {
#if UNITY_EDITOR
        StartCoroutine(LoadGameConfig(LoadConfigGameFromSheet));
#endif
    }

    public IEnumerator LoadGameConfig(Action<string> onSheetLoadedAction)
    {
        string url = "https://docs.google.com/spreadsheets/d/*/export?format=csv";
        string actualUrl = url.Replace("*", "11F02qXsrTPe811YpwI1P8bOc1vFLsinffBhtAtaGf9g");
        using (UnityWebRequest request = UnityWebRequest.Get(actualUrl))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError) { }

            else
                onSheetLoadedAction(request.downloadHandler.text);
        }
        yield return null;

    }
    private void LoadConfigGameFromSheet(string rawCVSText)
    {
        listSkin.Clear();
        rows = new List<string>();
        char[] array = { '\n' };
        foreach (var item in rawCVSText.Split('\n'))
        {
            rows.Add(item);
        }

        ///Load skin config
        List<string> nameHexa = new List<string>();
        List<string> hexaColor = new List<string>();

        for (int i = 1; i < 8; i++)
        {
            SkinInfo skinInfo = new SkinInfo();
            if (int.TryParse(rows[2].Split(',')[i], out int _ID))
                skinInfo.ID = _ID;
            if (int.TryParse(rows[3].Split(',')[i], out int _Price))
                skinInfo.Price = _Price;
            skinInfo.Type = rows[4].Split(',')[i];
            listSkin.Add(skinInfo);
        }
    }


}
[System.Serializable]
public class SkinInfo
{
    public int ID = 0;
    public int Price = 0;
    public string Type = "Gold";
}