using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardS_Use : CardBaseState
{
    RectTransform cardInUseRect;
    Vector3 demonstationPos = new Vector3(800,0,0);

    public override void EnterState(GameMaster gM)
    {
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            if (gM.handM.handCardList[i].GetComponent<CardManager>().cardMovement == CardMovement.Use)
            {
                gM.cardSM.cardInUse = gM.handM.handCardList[i].GetComponent<CardManager>();
                cardInUseRect = gM.cardSM.cardInUse.GetComponent<RectTransform>();
            }
        }
        gM.handM.handCardList.RemoveAt(gM.cardSM.cardInUse.handIndex - 1);
        for (int i = 0; i < gM.handM.handCardList.Count; i++) //重置所有的卡的handIndex去对应的目标
        {
            gM.handM.handCardList[i].GetComponent<CardManager>().handIndex = i + 1;
        }
        gM.cardSM.CalculateAllCardsDefaultPos(gM);
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

        cardInUseRect.anchoredPosition = Vector3.MoveTowards(cardInUseRect.anchoredPosition, demonstationPos, gM.cardSM.moveSpeed * 2 * Time.deltaTime);

        if (cardsMovingRecord == gM.cardSM.cardsPos.Count && cardInUseRect.anchoredPosition.x == demonstationPos.x)
        {
            gM.cardSM.isUpdate = false;
            //Debug.Log(cardsMovingRecord);
            EndState(gM);

        }
    }

    public override void EndState(GameMaster gM)
    {
        gM.cEffectSM.cardInUse = gM.cardSM.cardInUse.cardInfo;
        gM.cEffectSM.isCardEffectRunning = true;
        gM.cEffectSM.CardEffectsApply(gM.cEffectSM.cardInUse);
    }
}
