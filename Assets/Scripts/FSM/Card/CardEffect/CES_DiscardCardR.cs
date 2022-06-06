using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CES_DiscardCardR : CEffectBaseState
{
    private Vector3 discardPilePos = new Vector3(850, -450, 0);

    private float localScaleX;
    private float localScaleY;
    private bool isRotateOver = false;
    private bool isScaleUpOver = false;

    public override void EnterState(GameMaster gM,int value)
    {
        gM.cardSM.CalculateAllCardsDefaultPos(gM);
        localScaleX = 1f;
        localScaleY = 1f;
        isRotateOver = false;
        isScaleUpOver = false;
        gM.cardSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        Debug.Log("Discard");
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
            switch (gM.handM.handCardList[i].GetComponent<CardManager>().cardMovement)
            {
                case CardMovement.Null:
                    cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, gM.cardSM.cardsPos[i], gM.cardSM.moveSpeed * 2 * Time.deltaTime);
                    if (cardRect.anchoredPosition.x == gM.cardSM.cardsPos[i].x)
                    {
                        gM.cardSM.movingRecord[i] = true;
                    }
                    break;
                case CardMovement.Discard:
                    cardRect.anchoredPosition = Vector3.MoveTowards(cardRect.anchoredPosition, discardPilePos, gM.cardSM.moveSpeed * 2 * Time.deltaTime);

                    if (isRotateOver == false)
                    {
                        cardRect.Rotate(0, 0, -2f);
                        if (cardRect.rotation.z <= -0.7f) // -0.7=90¶È
                        {
                            cardRect.rotation = Quaternion.Euler(0, 0, -90);
                            //Debug.Log(cardRect.rotation.z);
                            isRotateOver = true;
                        }
                    }
                    if (isScaleUpOver == false)
                    {
                        localScaleX -= 2f * Time.deltaTime;
                        localScaleY -= 2f * Time.deltaTime;
                        cardRect.localScale = new Vector3(localScaleX, localScaleY, 0);
                        if (cardRect.localScale.x <= 0.3f)
                        {
                            cardRect.localScale = new Vector3(0.3f, 0.3f, 0);
                            isScaleUpOver = true;
                        }
                    }

                    //Debug.Log(isRotateOver);
                    //Debug.Log(isScaleUpOver);
                    if (cardRect.anchoredPosition.x == discardPilePos.x && isRotateOver == true && isScaleUpOver == true)
                    {
                        gM.cardSM.movingRecord[i] = true;
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
        //Debug.Log(cardsMovingRecord);
        Debug.Log(gM.cardSM.cardsPos.Count);
        if (cardsMovingRecord == gM.cardSM.cardsPos.Count)
        {
            gM.cardSM.isUpdate = false;
            EndState(gM,value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        for (int i = gM.handM.handCardList.Count; i > 0; i--)
        {
            CardManager cardM = gM.handM.handCardList[i - 1].GetComponent<CardManager>();
            if (cardM.cardMovement == CardMovement.Discard)
            {
                cardM.DiscardHandCard();
            }
        }
        gM.cardSM.EnterCardState(gM.cardSM.defaultState);
    }
}
