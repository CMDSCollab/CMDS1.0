using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSprite : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image buttonImg;
    public Sprite normalSpr;
    public Sprite touchSpr;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImg.sprite = touchSpr;
        Debug.Log("1");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImg.sprite = normalSpr;
    }


}
