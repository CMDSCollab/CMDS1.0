using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Camera UICamera;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Canvas canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera
            || canvas.renderMode == RenderMode.WorldSpace)
        {
            UICamera = canvas.worldCamera;
        }
        else if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            UICamera = null;
        }
    }

    public static UIManager GetInstance()
    {
        return Instance;
    }
}
