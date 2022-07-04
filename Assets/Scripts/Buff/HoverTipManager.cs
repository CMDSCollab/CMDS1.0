using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HoverTipManager : MonoBehaviour
{
    //public static Action<string, string, Vector2> OnMouseHover;
    //public static Action OnMouseLoseFocus;
    RectTransform tipBgRect;

    private void Start()
    {
        tipBgRect = transform.Find("TipBg").GetComponent<RectTransform>();
    }

    public void ShowTip(string tipName,string tipDescription, Vector2 mousePos)
    {
        transform.Find("Name").GetComponent<Text>().text = tipName;
        transform.Find("Description").GetComponent<Text>().text = tipDescription;

        bool isUpper = false;
        bool isRight = false;

        if (mousePos.x >= 960)
            isRight = true;
        if (mousePos.y >= 540)
            isUpper = true;

        if (isUpper == true && isRight == true)
        {
            tipBgRect.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
            transform.position = new Vector2(mousePos.x - tipBgRect.sizeDelta.x / 2 + 5, mousePos.y - tipBgRect.sizeDelta.y / 2 + 5);
        }
        if (isUpper == true && isRight == false)
        {
            tipBgRect.rotation = Quaternion.Euler(new Vector3(180, 180, 0));
            transform.position = new Vector2(mousePos.x + tipBgRect.sizeDelta.x / 2 - 5, mousePos.y - tipBgRect.sizeDelta.y / 2 + 5);
        }
        if (isUpper == false && isRight == true)
        {
            tipBgRect.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.position = new Vector2(mousePos.x - tipBgRect.sizeDelta.x / 2 + 5, mousePos.y + tipBgRect.sizeDelta.y / 2 - 5);
        }
        if (isUpper == false && isRight == false)
        {
            tipBgRect.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            transform.position = new Vector2(mousePos.x + tipBgRect.sizeDelta.x / 2 - 5, mousePos.y + tipBgRect.sizeDelta.y / 2 - 5);
        }
    }

    public void HideTip()
    {
        transform.Find("Name").GetComponent<Text>().text = default;
        transform.Find("Description").GetComponent<Text>().text = default;

        transform.gameObject.SetActive(false);
    }
}
