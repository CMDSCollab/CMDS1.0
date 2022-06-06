using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardS_Select : CardBaseState
{
    public override void EnterState(GameMaster gM)
    {
        gM.cardSM.CalculateAllCardsSeperatePos(gM);
        gM.cardSM.CalculateAllCardsDefaultPos(gM);
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            if (i == gM.cardSM.selectCardIndex)
            {
                gM.cardSM.selectedRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
                gM.cardSM.selectedTargetPos = new Vector3(gM.cardSM.cardsPos[i].x, gM.cardSM.selectedRect.anchoredPosition.y + gM.cardSM.selectMoveAmount, 0);
            }
        }
 
        gM.cardSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
        //Debug.Log("Select");
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
            if (i == gM.cardSM.selectCardIndex)
            {
                cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.cardSM.selectedTargetPos, gM.cardSM.moveSpeed * Time.deltaTime);
            }
            else
            {
                cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.cardSM.seperateCardsPos[i], gM.cardSM.moveSpeed * Time.deltaTime);
            }
        }

        if (gM.cardSM.selectedRect!=null)
        {
            if (gM.cardSM.selectedRect.position.y == gM.cardSM.selectedTargetPos.y)
            {
                gM.cardSM.isUpdate = false;
                EndState(gM);
            }
        }

    }

    public override void EndState(GameMaster gM)
    {

    }
}
