using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CES_DrawCardR : CEffectBaseState
{
    private Vector3 drawPilePos = new Vector3(-850, -450, 0);
    private float localScaleX;
    private float localScaleY;
    private bool isRotateOver = false;
    private bool isScaleUpOver = false;

    public override void EnterState(GameMaster gM, int value)
    {
        //Debug.Log("entered");
        //Debug.Log(gM.cardSM.isUpdate);
        gM.cardSM.CalculateAllCardsDefaultPos(gM);

        //Debug.Log(gM.handM.handCardList.Count);
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
            if (gM.handM.handCardList[i].GetComponent<CardManager>().cardMovement == CardMovement.Draw)
            {
                cardRect.anchoredPosition = drawPilePos;
                cardRect.rotation = Quaternion.Euler(0, 0, -90);
                localScaleX = 0.3f;
                localScaleY = 0.3f;
                cardRect.localScale = new Vector3(localScaleX, localScaleY, 0);
                isRotateOver = false;
                isScaleUpOver = false;
            }
        }

        gM.cardSM.isUpdate = true;
        //Debug.Log(gM.cardSM.isUpdate);
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        //Debug.Log("DrawCardUpdate");
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
            switch (gM.handM.handCardList[i].GetComponent<CardManager>().cardMovement)
            {
                case CardMovement.Null:
                    cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.cardSM.cardsPos[i], gM.cardSM.moveSpeed * Time.deltaTime);
                    if (cardRect.anchoredPosition.x == gM.cardSM.cardsPos[i].x)
                    {
                        gM.cardSM.movingRecord[i] = true;
                    }
                    break;
                case CardMovement.Draw:
                    cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.cardSM.cardsPos[i], gM.cardSM.moveSpeed * Time.deltaTime);

                    if (isRotateOver == false)
                    {
                        cardRect.Rotate(0, 0, 1f);
                        if (cardRect.rotation.z >= 0)
                        {
                            cardRect.rotation = Quaternion.Euler(0, 0, 0);
                            isRotateOver = true;
                        }
                    }
                    if (isScaleUpOver == false)
                    {
                        localScaleX += 0.3f * Time.deltaTime;
                        localScaleY += 0.3f * Time.deltaTime;
                        cardRect.localScale = new Vector3(localScaleX, localScaleY, 0);
                        if (cardRect.localScale.x >= 1)
                        {
                            cardRect.localScale = new Vector3(1, 1, 0);
                            isScaleUpOver = true;
                        }
                    }
                    if (cardRect.anchoredPosition.x == gM.cardSM.cardsPos[i].x && isRotateOver == true && isScaleUpOver == true)
                    {
                        gM.cardSM.movingRecord[i] = true;
                        cardRect.GetComponent<CardManager>().cardMovement = CardMovement.Null;
                    }
                    break;
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
            EndState(gM,value);
            gM.cardSM.isUpdate = false;
        }
    }

    public override void EndState(GameMaster gM,int value)
    {
        Debug.Log("entered");
        //if (gM.buffM.FindBuff(CharacterBuff.IsTeamWork) != null)
        //{
        //    gM.buffSM.drawCardAmount = drawAmount;
        //    gM.buffSM.buffUsage = BuffUsage.EffectApply;
        //    gM.buffSM.EnterBuffState(gM.buffSM.teamworkState);
        //}
        gM.cardSM.EnterCardState(gM.cardSM.defaultState);
    }
}
