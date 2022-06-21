using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HoverTipManager : MonoBehaviour
{
    public static Action<string, string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    public void ShowTip(string tipName,string tipDescription, Vector2 mousePos)
    {
        transform.Find("Name").GetComponent<Text>().text = tipName;
        transform.Find("Description").GetComponent<Text>().text = tipDescription;

        //transform.gameObject.SetActive(true);
        transform.position = new Vector2(mousePos.x+transform.Find("TipBg").GetComponent<RectTransform>().sizeDelta.x / 2+20, mousePos.y + transform.Find("TipBg").GetComponent<RectTransform>().sizeDelta.y / 2 + 20);
        //+transform.Find("TipBg").GetComponent<RectTransform>().sizeDelta.x/2
    }

    public void HideTip()
    {
        transform.Find("Name").GetComponent<Text>().text = default;
        transform.Find("Description").GetComponent<Text>().text = default;

        transform.gameObject.SetActive(false);
    }
}
