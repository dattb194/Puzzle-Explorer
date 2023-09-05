using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCtrl : LineRendererSmoother
{
    public int maxEnegy = 100;
    private void Update()
    {
        Drag();
        if (Input.GetMouseButtonUp(0) || enegy <= 0 )
        {
            if (drawing)
            {
                drawing = false;
                GenerateMeshCollider();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
            enegy = 100;
    }

    int cout = 0;
    [SerializeField] int enegy;
    [SerializeField] bool drawing = false;
    Ray ray;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enegy = maxEnegy;
            drawing = true;
            Line.positionCount = 0;
            cout = 0;
        }
    }
    private void OnMouseExit()
    {
        if (drawing)
        {
            drawing = false;
            GenerateMeshCollider();
        }
    }
    private void Drag()
    {
        if (!drawing) return;
        if (!mouseMove()) return;
        enegy--;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            //if (hit.collider.gameObject.tag == "wallDraw")
            {
                cout++;
                Vector3 v = new Vector3(hit.point.x, hit.point.y, 0);
                Line.positionCount = cout;
                Line.SetPosition(cout - 1, v);
            }
        }
    }
    bool mouseMove()
    {
        if (Input.GetAxis("Mouse X") != 0) return true;
        if (Input.GetAxis("Mouse Y") != 0) return true;
        return false;
    }
}
