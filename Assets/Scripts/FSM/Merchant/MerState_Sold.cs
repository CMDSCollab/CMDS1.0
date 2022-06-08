using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerState_Sold : MerchantBaseState
{
    public override void EnterState(GameMaster gM)
    {
        gM.merchantSM.currentItemRectTrans.gameObject.transform.SetParent(gM.uiCanvas.transform);
        //gM.merchantSM.currentItemRectTrans.SetSiblingIndex(7);

        gM.merchantSM.targetPos = new Vector3(-675, 250, 0); // 卡牌购买后 移动的终点坐标 即deckButton所在的位置
        gM.merchantSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
        gM.merchantSM.currentItemRectTrans.anchoredPosition = Vector3.MoveTowards(gM.merchantSM.currentItemRectTrans.anchoredPosition, gM.merchantSM.targetPos, gM.merchantSM.moveSpeed * Time.deltaTime);
        gM.merchantSM.currentItemRectTrans.SetSiblingIndex(1000);

        if (gM.merchantSM.currentItemRectTrans != null)
        {
            if (gM.merchantSM.currentItemRectTrans.localPosition == gM.merchantSM.targetPos)
            {
                gM.merchantSM.isUpdate = false;
                EndState(gM);
            }
        }
    }

    public override void EndState(GameMaster gM)
    {
        gM.deckM.GetNewCopyDeck();
        gM.merchantSM.DestoryItemApperance();
        gM.buttonM.SynchronizeCardsCountInPileButton("Deck");
        gM.merchantSM.EnterMerchantState(gM.merchantSM.defaultState);
    }
}
