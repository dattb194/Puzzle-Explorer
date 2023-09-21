using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseDisplay : MonoBehaviour
{
    public void Skip()
    {
    
    }
    public void Replay()
    {
        GPMng.inst.ReloadScene();
    }
}
