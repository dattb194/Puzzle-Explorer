using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autorotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
