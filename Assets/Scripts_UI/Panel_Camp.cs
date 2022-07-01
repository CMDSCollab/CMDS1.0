using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Camp : PanelController
{
    private bool isFirstClicked;
    private int restPoint = 20;

    public void OnClickRest()
    {
        if (!isFirstClicked)
        {
            gM.characterM.mainCharacter.HealSelf(restPoint);
            isEnd = true;
            isFirstClicked = true;
        }
    }
}
