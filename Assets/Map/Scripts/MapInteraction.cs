using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteraction : MonoBehaviour
{
    public Transform hitObj;
    private bool isDragging = false;
    private Vector2 mousePos;
    private bool scrollUpward = false;
    private bool scrollDownward = false;

    void Update()
    {
        if (FindObjectOfType<GameMaster>().panelM.isPanelOpen == false)
        {
            MapScroll();
        }

    }

    public void MapScroll()
    {
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //鼠标滚动滑轮 值就会变化
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                scrollUpward = true;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                scrollDownward = true;
            }
           
        }

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            if (mousePos.y - Input.mousePosition.y < -1.2f)
            {
                scrollUpward = true;
                mousePos = Input.mousePosition;
            }
            else if (mousePos.y - Input.mousePosition.y > 1.2f)
            {
                scrollDownward = true;
                mousePos = Input.mousePosition;
            }
        }
    }

    private void FixedUpdate()
    {
        HandleMapMovement();
    }

    private void HandleMapMovement()
    {
        if (scrollUpward)
        {
            if (transform.position.y < 21)
            {
                transform.Translate(Vector3.up * 1f);
                transform.GetComponent<MapManager>().LinePosReset(1f);
            }
            scrollUpward = false;
        }
        else if (scrollDownward)
        {
            if (transform.position.y > -1)
            {
                transform.Translate(Vector3.up * -1f);
                transform.GetComponent<MapManager>().LinePosReset(-1f);
            }
            scrollDownward = false;
        }
    }
}
