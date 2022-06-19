using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardS_Draw : CardBaseState
{
    private Vector3 drawPilePos = new Vector3(-850, -450, 0);

    private float localScaleX;
    private float localScaleY;
    private bool isRotateOver = false;
    private bool isScaleUpOver = false;

    private int drawAmount = 0;

    public override void EnterState(GameMaster gM)
    {
        //Debug.Log("DrawCardEntered");
        //Debug.Log(gM.cardSM.isUpdate);
        gM.cardSM.CalculateAllCardsDefaultPos(gM);
        drawAmount = 0;

        //Debug.Log(gM.handM.handCardList.Count);
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            RectTransform cardRect = gM.handM.handCardList[i].GetComponent<RectTransform>();
            if (gM.handM.handCardList[i].GetComponent<CardManager>().cardMovement == CardMovement.Draw)
            {
                drawAmount++;
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

    public override void UpdateState(GameMaster gM)
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
            EndState(gM);
            gM.cardSM.isUpdate = false;
        }
    }

    public override void EndState(GameMaster gM)
    {
        if (gM.buffM.FindBuff(CharacterBuff.IsTeamWork) != null)
        {
            AIMate targetAI = gM.aiM.artAI;
            for (int i = 0; i < drawAmount; i++)
            {
                int random = Random.Range(0, 1);
                //Debug.Log(random);
                //Debug.Log(drawAmount);
                switch (gM.characterM.mainCharacterType)
                {
                    case CharacterType.Designer:
                        if (random == 0)
                        {
                            targetAI = gM.aiM.artAI;
                        }
                        else
                        {
                            targetAI = gM.aiM.proAI;
                        }
                        break;
                    case CharacterType.Programmmer:
                        if (random == 0)
                        {
                            targetAI = gM.aiM.desAI;
                        }
                        else
                        {
                            targetAI = gM.aiM.artAI;
                        }
                        break;
                    case CharacterType.Artist:
                        if (random == 0)
                        {
                            targetAI = gM.aiM.desAI;
                        }
                        else
                        {
                            targetAI = gM.aiM.proAI;
                        }
                        break;
                }
                targetAI.energyPoint += 1;
                if (targetAI.energyPoint > targetAI.energySlotAmount)
                {
                    targetAI.energyPoint = targetAI.energySlotAmount;
                }
                targetAI.transform.Find("EnergyBar").GetComponent<Slider>().value = targetAI.energyPoint;
                targetAI.IntentionValueChangeAndUISync();
            }

        }
        //Debug.Log(gM.cEffectSM.isCardEffectRunning);
        if (gM.cEffectSM.isCardEffectRunning == true)
        {
            gM.cEffectSM.CardEffectsApply(gM.cEffectSM.cardInUse);
        }
        gM.cardSM.EnterCardState(gM.cardSM.defaultState);
    }
}
