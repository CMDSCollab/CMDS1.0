using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardS_Discard : CardBaseState
{
    private Vector3 discardPilePos = new Vector3(850, -450, 0);
    private float localScaleX;
    private float localScaleY;
    private bool isRotateOver;
    private bool isScaleUpOver;
                              
    public override void EnterState(GameMaster gM)
    {
        gM.cardSM.CalculateAllCardsDefaultPos(gM);
        localScaleX = 1f;
        localScaleY = 1f;
        isRotateOver = false;
        isScaleUpOver = false;
        gM.cardSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
        switch (gM.cardSM.discardType)
        {
            case DiscardType.InUse:
                RectTransform useRect = gM.cardSM.cardInUse.GetComponent<RectTransform>();
                useRect.anchoredPosition = Vector3.MoveTowards(useRect.anchoredPosition, discardPilePos, gM.cardSM.moveSpeed * 2 * Time.deltaTime);
                if (isScaleUpOver == false)
                {
                    localScaleX -= 3f * Time.deltaTime;
                    localScaleY -= 3f * Time.deltaTime;
                    useRect.localScale = new Vector3(localScaleX, localScaleY, 0);
                    if (useRect.localScale.x <= 0.3f)
                    {
                        useRect.localScale = new Vector3(0.3f, 0.3f, 0);
                        isScaleUpOver = true;
                    }
                }
                if (useRect.anchoredPosition.x == discardPilePos.x && isScaleUpOver == true)
                {
                    gM.cardSM.isUpdate = false;
                    EndState(gM);
                }
                break;
            case DiscardType.AllHand:
                for (int i = 0; i < gM.handM.handCardList.Count; i++)
                {
                    RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
                    if (gM.handM.handCardList[i].GetComponent<CardManager>().cardMovement == CardMovement.Discard)
                    {
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
                        if (cardRect.anchoredPosition.x == discardPilePos.x && isRotateOver == true && isScaleUpOver == true)
                        {
                            gM.cardSM.movingRecord[i] = true;
                        }
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
                    gM.cardSM.isUpdate = false;
                    EndState(gM);
                }
                break;
        }
    }

    public override void EndState(GameMaster gM)
    {
        switch (gM.cardSM.discardType)
        {
            case DiscardType.InUse:
                gM.cardSM.cardInUse.DiscardHandCard();
                break;
            case DiscardType.AllHand:
                for (int i = gM.handM.handCardList.Count; i > 0; i--)
                {
                    CardManager cardM = gM.handM.handCardList[i - 1].GetComponent<CardManager>();
                    if (cardM.cardMovement == CardMovement.Discard)
                    {
                        cardM.DiscardHandCard();
                    }
                }
                gM.handM.handCardList.Clear();
                break;
        }
        gM.cardSM.EnterCardState(gM.cardSM.defaultState);
    }
}
