using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardS_EndSelect : CardBaseState
{
    public override void EnterState(GameMaster gM)
    {
        //Debug.Log("EndSelect");
        gM.cardSM.CalculateAllCardsDefaultPos(gM);
        //for (int i = 0; i < gM.handM.handCardList.Count; i++)
        //{
        //    if (i == gM.cardSM.selectCardIndex)
        //    {
        //        gM.cardSM.selectedRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
        //        gM.cardSM.selectedTargetPos = new Vector3(gM.cardSM.selectedRect.anchoredPosition.x, gM.cardSM.selectedRect.anchoredPosition.y - gM.cardSM.selectMoveAmount, 0);
        //    }
        //}
        gM.cardSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
            cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.cardSM.cardsPos[i], gM.cardSM.moveSpeed * Time.deltaTime);
            if (cardRect.anchoredPosition.y == gM.cardSM.cardsPos[i].y)
            {
                gM.cardSM.movingRecord[i] = true;
            }
        }
        int cardsMovingRecord = 0;

        foreach (bool singleCardMovingOver in gM.cardSM.movingRecord)
        {
            if (singleCardMovingOver == true)
            {
                cardsMovingRecord++;
            }
        }
        if (cardsMovingRecord == gM.cardSM.cardsPos.Count)
        {
            //Debug.Log(cardsMovingRecord);
            EndState(gM);
            gM.cardSM.isUpdate = false;
        }
        //for (int i = 0; i < gM.handM.handCardList.Count; i++)
        //{
        //    RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
        //    if (i == gM.cardSM.selectCardIndex)
        //    {
        //        cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.cardSM.selectedTargetPos, gM.cardSM.moveSpeed * Time.deltaTime);
        //    }
        //}
        //if (gM.cardSM.selectedRect.position.y == gM.cardSM.selectedTargetPos.y)
        //{
        //    gM.cardSM.isUpdate = false;
        //    EndState(gM);
        //}
    }

    public override void EndState(GameMaster gM)
    {
        gM.cardSM.EnterCardState(gM.cardSM.defaultState);
    }
}
