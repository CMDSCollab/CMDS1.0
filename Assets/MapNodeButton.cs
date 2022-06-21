using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeButton : MonoBehaviour
{
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    public SpriteRenderer sprRender;



    private void OnMouseEnter()
    {
        sprRender.sprite = activeSprite;
    }

    private void OnMouseExit()
    {
        sprRender.sprite = inactiveSprite;
    }
}
