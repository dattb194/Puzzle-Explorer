using System.Collections;
using UnityEngine;

public abstract class UIFixed : MonoBehaviour
{
    public Vector2 screenSize => FindAnyObjectByType<Canvas>().renderingDisplaySize;
    public RectTransform mRect => GetComponent<RectTransform>();
    // Update is called once per frame
    public virtual void Start()
    {
        Fix();
    }
    public virtual void Update()
    {
#if UNITY_EDITOR
        Fix();
#endif
    }
    public abstract void Fix();
}