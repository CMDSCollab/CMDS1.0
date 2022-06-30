using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerState_Sold : MerchantBaseState
{
    float localScaleX = 1f;
    float localScaleY = 1f;
    bool isScaleUpOver = false;

    public override void EnterState(GameMaster gM)
    {
        gM.merchantSM.currentItemRectTrans.gameObject.transform.SetParent(gM.comStatusBar.transform);
        localScaleX = 1f;
        localScaleY = 1f;
        gM.merchantSM.targetPos = gM.uiCanvas.transform.Find("CommonStatusBar").Find("Deck").localPosition; // 卡牌购买后 移动的终点坐标 即deckButton所在的位置
        gM.merchantSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
        RectTransform cardRect = gM.merchantSM.currentItemRectTrans;
        cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.merchantSM.targetPos, gM.merchantSM.moveSpeed * Time.deltaTime);
        gM.merchantSM.currentItemRectTrans.SetSiblingIndex(1000);

        Debug.Log(cardRect.Find("CardName").GetComponent<Text>().text);
        if (isScaleUpOver == false)
        {
            localScaleX -= 3f * Time.deltaTime;
            localScaleY -= 3f * Time.deltaTime;
            cardRect.localScale = new Vector3(localScaleX, localScaleY, 0);
            if (cardRect.localScale.x <= 0.3f)
            {
                cardRect.localScale = new Vector3(0.3f, 0.3f, 0);
                isScaleUpOver = true;
            }
        }
        if (cardRect.anchoredPosition.x == gM.merchantSM.targetPos.x && isScaleUpOver == true)
        {
            gM.cardSM.isUpdate = false;
            EndState(gM);
        }
    }

    public override void EndState(GameMaster gM)
    {
        gM.deckM.GetNewCopyDeck();
        gM.merchantSM.DestoryItemApperance();
        gM.buttonM.SynchronizeCardsCountInPileButton("Deck");
        gM.merchantSM.EnterMerchantState(gM.merchantSM.defaultState);
        isScaleUpOver = false;
    }
}
//    public class MerState_Sold : MerchantBaseState
//{
//    float localScaleX;
//    float localScaleY;
//    bool isScaleUpOver;

//    public override void EnterState(GameMaster gM)
//    {
//        //gM.merchantSM.currentItemRectTrans.gameObject.transform.SetParent(gM.uiCanvas.transform);
//        //gM.merchantSM.currentItemRectTrans.SetSiblingIndex(7);

//        gM.merchantSM.targetPos = new Vector3(-675, 250, 0); // 卡牌购买后 移动的终点坐标 即deckButton所在的位置
//        gM.merchantSM.isUpdate = true;
//    }

//    public override void UpdateState(GameMaster gM)
//    {
//        //gM.merchantSM.currentItemRectTrans.anchoredPosition = Vector3.MoveTowards(gM.merchantSM.currentItemRectTrans.anchoredPosition, gM.merchantSM.targetPos, gM.merchantSM.moveSpeed * Time.deltaTime);
//        //gM.merchantSM.currentItemRectTrans.SetSiblingIndex(1000);

//        //if (gM.merchantSM.currentItemRectTrans != null)
//        //{
//        //    if (gM.merchantSM.currentItemRectTrans.localPosition == gM.merchantSM.targetPos)
//        //    {
//        //        gM.merchantSM.isUpdate = false;
//        //        EndState(gM);
//        //    }
//        //}

//        RectTransform cardRect = gM.merchantSM.currentItemRectTrans;
//        cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.merchantSM.targetPos, gM.merchantSM.moveSpeed * Time.deltaTime);
//        gM.merchantSM.currentItemRectTrans.SetSiblingIndex(1000);

//        if (isScaleUpOver == false)
//        {
//            localScaleX -= 0.3f * Time.deltaTime;
//            localScaleY -= 0.3f * Time.deltaTime;
//            cardRect.localScale = new Vector3(localScaleX, localScaleY, 0);
//            if (cardRect.localScale.x <= 0.3f)
//            {
//                cardRect.localScale = new Vector3(0.3f, 0.3f, 0);
//                isScaleUpOver = true;
//            }
//        }
//        if (cardRect.anchoredPosition.x == gM.merchantSM.targetPos.x && isScaleUpOver == true)
//        {
//            gM.cardSM.isUpdate = false;
//            EndState(gM);
//        }
//    }

//    public override void EndState(GameMaster gM)
//    {
//        gM.deckM.GetNewCopyDeck();
//        gM.merchantSM.DestoryItemApperance();
//        gM.buttonM.SynchronizeCardsCountInPileButton("Deck");
//        gM.merchantSM.EnterMerchantState(gM.merchantSM.defaultState);
//    }
//}
