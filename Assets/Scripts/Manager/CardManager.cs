using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardMovement
{
    Null,
    Draw,
    Discard,
    Seperate,
    Use
}

public class CardManager : MonoBehaviour
{
    public CardInfo cardInfo;
    public Text cardNameText;
    public Image cardTemplate;
    public GameMaster gM;

    public int handIndex;
    public int deckIndexRecord;
    public bool ifTouched = false;
    public bool isOnSell; //�Ƿ����̵��г��ۣ�
    public CardMovement cardMovement = CardMovement.Null;

    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        UpdateUI();
    }

    public void OnMouseEnter()
    {
        Debug.Log("AAAA" + gM.cardSM.currentState.ToString());

        if (isOnSell)
        {
            if(gM.merchantSM.currentState == gM.merchantSM.defaultState || gM.merchantSM.currentState == gM.merchantSM.deselectState)
            {
                gM.merchantSM.isUpdate = false;
                gM.merchantSM.currentItemRectTrans = this.gameObject.GetComponent<RectTransform>();
                gM.merchantSM.originPos = new Vector3(0, 0, 0);
                gM.merchantSM.EnterMerchantState(gM.merchantSM.selectState);
            }
        }
        if (gM.cardSM.currentState == gM.cardSM.defaultState || gM.cardSM.currentState == gM.cardSM.endSelectState)
        {
            gM.cardSM.isUpdate = false;
            gM.cardSM.selectCardIndex = handIndex - 1;
            gM.cardSM.EnterCardState(gM.cardSM.selectState);
        }


        //transform.position += Vector3.up * 50;
        //ifTouched = true;
    }

    public void OnMouseExit()
    {
        if (isOnSell)
        {
            if (gM.merchantSM.currentState == gM.merchantSM.selectState)
            {
                gM.merchantSM.isUpdate = false;
                gM.merchantSM.EnterMerchantState(gM.merchantSM.deselectState);
            }
        }
        if (gM.cardSM.currentState == gM.cardSM.selectState)
        {
            gM.cardSM.isUpdate = false;
            gM.cardSM.selectCardIndex = handIndex - 1;
            gM.cardSM.EnterCardState(gM.cardSM.endSelectState);
        }

        //transform.position += Vector3.down * 50;
        //ifTouched = false;
    }

    public void OnMouseClick()
    {

        if (isOnSell)
        {
            if (gM.merchantSM.currentState == gM.merchantSM.selectState)
            {
                if (cardInfo.realValue <= gM.comStatusBar.gold)
                {
                    gM.cardFunctionM.GetCardFromMerchant(this.gameObject);
                    gM.merchantSM.EnterMerchantState(gM.merchantSM.soldState);
                    gM.comStatusBar.GoldChange(- cardInfo.realValue);
                }
                else
                {
                    Debug.Log("No Gold");
                }

            }

            return;

        }

        if (gM.cardSM.currentState == gM.cardSM.selectState)
        {
            if (gameObject.transform.parent.name != "CardLayout") //ȷ�ϲ��������ƶѵĿ���ʹ��
            {
                //CardFuntion();
                if (cardInfo.specialFunctions.Contains(SpecialFunctionType.Exhausted) == false)
                {
                    cardMovement = CardMovement.Use;
                    gM.cardSM.EnterCardState(gM.cardSM.useState);
                    //cardMovement = CardMovement.Discard;
                    //gM.cardSM.EnterCardState(gM.cardSM.discardState);
                    //DiscardHandCard();
                }
                //gM.handM.OrganizeHand();
            }
            if (gameObject.transform.parent.name == "CardLayout" && gM.cardFunctionM.cardCanbeGetFromDrawPile == true)
            {
                gM.cardFunctionM.GetCardFromDrawPile(deckIndexRecord);
            }
            gM.cardFunctionM.FunctionEffectApply();
        }
    }

    //�������ƣ��ڴ��������غϽ���ʱ����
    public void DiscardHandCard()
    {
        gM.cardRepoM.discardPile.Add(deckIndexRecord, cardInfo);
        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //ͬ�����ƶѿ�������չʾText
        Destroy(this.gameObject);
    }

    public void UpdateUI()
    {
        cardNameText.text = cardInfo.cardName;
        cardTemplate.color = new Color(Random.Range(0.2f,0.7f), Random.Range(0.2f, 0.7f), Random.Range(0.2f, 0.7f));
        transform.Find("CardDescription").GetComponent<Text>().text = "Description:" +"\n" +cardInfo.description;
        for (int i = 0; i < cardInfo.baseFunctions.Count; i++)
        {
            switch (cardInfo.baseFunctions[i].functionType)
            {
                case BaseFunctionType.ArtEnergy:
                    transform.Find("ArtEnergy").Find("Value").GetComponent<Text>().text = "+" + cardInfo.baseFunctions[i].value.ToString();
                    break;
                case BaseFunctionType.DsgnEnergy:
                    transform.Find("DsgnEnergy").Find("Value").GetComponent<Text>().text = "+" + cardInfo.baseFunctions[i].value.ToString();
                    break;
                case BaseFunctionType.ProEnergy:
                    transform.Find("ProEnergy").Find("Value").GetComponent<Text>().text = "+" + cardInfo.baseFunctions[i].value.ToString();
                    break;
                default:
                    break;
            }
        }
        if (cardInfo is CardInfoPro)
        {
            CardInfoPro cardPro = (CardInfoPro)cardInfo;
            transform.Find("Redundancy").GetComponent<Text>().text = "Redundancy:"+ cardPro.codeRedundancy.ToString();
            if (cardPro.cardType == CardTypePro.Debug)
            {
                transform.Find("Redundancy").GetComponent<Text>().text = transform.Find("Redundancy").GetComponent<Text>().text + "\n" + cardPro.debugType;
            }
        }

        //�����ҿ�����Ҫ���style���������ʻ�
        if (cardInfo is CardInfoArt)
        {
            CardInfoArt cardArt = (CardInfoArt)cardInfo;
            Image img = transform.Find("StyleIcon").GetComponent<Image>();
            Debug.Log("TestXXXXXX" + gM.aiM.art.hitP.styleImageDic["Pixel"]);
            img.sprite = gM.aiM.art.hitP.styleImageDic[cardArt.style.ToString()];
    }
    }

    public void CardFuntion()
    {
        // ����ͨ�ù���
        for (int i = 0; i < cardInfo.baseFunctions.Count; i++)
        {
            switch (cardInfo.baseFunctions[i].functionType)
            {
                case BaseFunctionType.Damage:
                    //gM.cardSM.EnterCardState(gM.cardSM.attackState, cardInfo.baseFunctions[i].value);
                    gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(cardInfo.baseFunctions[i].value));
                    break;
                case BaseFunctionType.Shield:
                    //gM.aiM.pro.shieldPoint += cardInfo.baseFunctions[i].value;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
                    gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, cardInfo.baseFunctions[i].value, BuffSource.Character);
                    break;
                case BaseFunctionType.Heal:
                    break;
                case BaseFunctionType.ArtEnergy:
                    gM.aiM.artAI.EnergyValueChange(cardInfo.baseFunctions[i].value);
                    break;
                case BaseFunctionType.DsgnEnergy:
                    gM.aiM.desAI.EnergyValueChange(cardInfo.baseFunctions[i].value);
                    break;
                case BaseFunctionType.ProEnergy:
                    gM.aiM.proAI.EnergyValueChange(cardInfo.baseFunctions[i].value);
                    break;
                case BaseFunctionType.ArtSlot:
                    gM.aiM.artAI.energySlotAmount++;
                    gM.aiM.artAI.AddTheEnergySlot();
                    //if (gM.characterM.mainCharacterType == CharacterType.Designer && gM.aiM.des.isSycn)
                    //{
                    //    gM.aiM.artAI.EnergyValueChange(1);
                    //}
                    break;
                case BaseFunctionType.DsgnSlot:
                    gM.aiM.desAI.energySlotAmount++;
                    gM.aiM.desAI.AddTheEnergySlot();
                    break;
                case BaseFunctionType.ProSlot:
                    gM.aiM.proAI.energySlotAmount++;
                    gM.aiM.proAI.AddTheEnergySlot();
                    //if (gM.characterM.mainCharacterType == CharacterType.Designer && gM.aiM.des.isSycn)
                    //{
                    //    gM.aiM.proAI.EnergyValueChange(1);
                    //}
                    break;
                case BaseFunctionType.DrawCard:
                    gM.deckM.DrawCardFromDeckRandomly(cardInfo.baseFunctions[i].value);
                    //if (gM.characterM.mainCharacterType == CharacterType.Designer && gM.aiM.des.isTeamWork) { gM.aiM.des.GoTeamWork(cardInfo.baseFunctions[i].value); }
                    break;
                default:
                    break;
            }
        }
        // �������⹦��
        for (int i = 0; i < cardInfo.specialFunctions.Count; i++)
        {
            switch (cardInfo.specialFunctions[i])
            {
                case SpecialFunctionType.ArtIntentionChange:
                    gM.aiM.artAI.ChangeIntention();
                    break;
                case SpecialFunctionType.DsgnIntentionChange:
                    gM.aiM.desAI.ChangeIntention();
                    break;
                case SpecialFunctionType.ProIntentionChange:
                    gM.aiM.proAI.ChangeIntention();
                    break;
                case SpecialFunctionType.DrawSpecificCard:
                    gM.cardFunctionM.FindCardInDrawPile();
                    break;
                case SpecialFunctionType.Exhausted:
                    gM.deckM.cardInDeck.Remove(handIndex);
                    gM.handM.handCardList.RemoveAt(handIndex - 1);
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
  
        //������ ������ع���
        if(cardInfo is CardInfoArt)
        {
            if (gM.characterM.mainCharacterType != CharacterType.Artist)
            {
                return;
            }

            CardInfoArt cardArt = (CardInfoArt)cardInfo;

            gM.aiM.art.StyleCheck(cardArt);

            for(int i = 0; i < cardArt.artSpecialFunctions.Count; i++)
            {
                switch (cardArt.artSpecialFunctions[i].artFunctionType)
                {
                    case SpecialArtFunctionType.None:
                        break;
                    case SpecialArtFunctionType.TrueDamage:
                        gM.enM.enemyTarget.TakeTrueDamage(cardArt.artSpecialFunctions[i].value);
                        break;
                    case SpecialArtFunctionType.StyleEffect:
                        gM.aiM.art.StyleEffect();
                        break;
                    case SpecialArtFunctionType.GetIncome:
                        gM.comStatusBar.GoldChange(cardArt.artSpecialFunctions[i].value);
                        break;
                }
            }

            for(int i = 0; i < cardArt.artDrawExpandCard.Count; i++)
            {
                for(int x = 0; x < cardArt.artDrawExpandCard[i].times; x++) 
                {
                    gM.deckM.DrawSpecificSingleArtistExpandCard(cardArt.artDrawExpandCard[i].expandCardName.ToString());

                }
            }
        }
  

        // ���ʦ ������ع���
        if (cardInfo is CardInfoDsgn)
        {
            if (gM.characterM.mainCharacterType != CharacterType.Designer)
            {
                return;
            }

            CardInfoDsgn cardDsgn = (CardInfoDsgn)cardInfo;


            for (int i = 0; i < cardDsgn.desSpecialFunctions.Count; i++)
            {
                switch (cardDsgn.desSpecialFunctions[i].desFunctionType)
                {
                    case SpecialDesFunctionType.None:
                        break;
                    case SpecialDesFunctionType.ChangeChallenge:
                        gM.aiM.des.challengeLv += cardDsgn.desSpecialFunctions[i].value;
                        if (gM.aiM.des.challengeLv<0)
                        {
                            gM.aiM.des.challengeLv = 0;
                        }
                        gM.enM.enemyTarget.MainChaMCChange();
                        break;
                    case SpecialDesFunctionType.ChangeSkill:
                        gM.enM.enemyTarget.skillLv += cardDsgn.desSpecialFunctions[i].value;
                        if (gM.enM.enemyTarget.skillLv < 0)
                        {
                            gM.enM.enemyTarget.skillLv = 0;
                        }
                        gM.enM.enemyTarget.MainChaMCChange();
                        break;
                }
            }

            //for (int i = 0; i < cardDsgn.desPassiveEffects.Count; i++)
            //{
            //    switch (cardDsgn.desPassiveEffects[i].desPassiveEType)
            //    {
            //        case SpecialPassiveEffectType.None:
            //            break;
            //        case SpecialPassiveEffectType.IsTeamWork:
            //            gM.aiM.des.isTeamWork = true;
            //            gM.buffM.SetBuff(CharacterBuff.IsTeamWork, BuffTimeType.Temporary, 1, BuffValueType.SetValue, 1,BuffSource.Character);
            //            break;
            //        case SpecialPassiveEffectType.IsSycn:
            //            gM.aiM.des.isSycn = true;
            //            gM.buffM.SetBuff(CharacterBuff.IsSycn, BuffTimeType.Temporary, 1, BuffValueType.SetValue, 1, BuffSource.Character);
            //            break;
            //    }

            //}
        }

        // ����Գ ������ع���
        //if (cardInfo is CardInfoPro)
        if (cardInfo is CardInfoPro)
        {
            if (gM.characterM.mainCharacterType != CharacterType.Programmmer)
            {
                return;
            }
            CardInfoPro cardPro = (CardInfoPro)cardInfo;

            gM.aiM.pro.CheckCardDebug(cardPro.debugType);
            gM.aiM.pro.AddRedundantCode(cardPro.codeRedundancy);

            switch (cardPro.proSpecialFunction)
            {
                case SpecialFunctionPro.None:
                    break;
                case SpecialFunctionPro.DamageEqualsShield:
                    gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(gM.aiM.pro.shieldPoint));
                    break;
                case SpecialFunctionPro.DoubleShield:
                    if (gM.buffM.FindBuff(CharacterBuff.Defence) != null)
                    {
                    gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, gM.buffM.FindBuff(CharacterBuff.Defence).value, BuffSource.Character);
                    }
                    //gM.aiM.pro.shieldPoint += gM.aiM.pro.shieldPoint;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
                    break;
                case SpecialFunctionPro.UseHandCardsGainShield:
                    gM.cardFunctionM.isUseCardGainShield = true;
                    break;
                case SpecialFunctionPro.DiscardAllHandCard:
                    if (gM.handM.handCardList.Count > 0)
                    {
                        for (int i = gM.handM.handCardList.Count; i > 0; i--)
                        {
                            if (gM.handM.handCardList[i - 1].GetComponent<CardManager>().handIndex != handIndex)
                            {
                                gM.handM.handCardList[i - 1].GetComponent<CardManager>().DiscardHandCard();
                                gM.handM.handCardList.RemoveAt(gM.handM.handCardList[i - 1].GetComponent<CardManager>().handIndex - 1);
                                gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, 5, BuffSource.Character);
                                //gM.aiM.pro.shieldPoint += 5;
                                //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
                            }
                        }
                        //gM.handM.OrganizeHand();
                    }
                    break;
                case SpecialFunctionPro.Vengeance:
                    //gM.cardFunctionM.isVengeance = true;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Vengeance, true, 4);
                    gM.buffM.SetBuff(CharacterBuff.Vengeance, BuffTimeType.Temporary, 1, BuffValueType.SetValue, 4,BuffSource.Character);
                    break;
                case SpecialFunctionPro.ConsumeShieldDoubleDamage:
                    //gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(gM.aiM.pro.shieldPoint * 2));
                    //gM.aiM.pro.shieldPoint = 0;
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Defence, true, gM.aiM.pro.shieldPoint);
                    if (gM.buffM.FindBuff(CharacterBuff.Defence) != null)
                    {
                    gM.enM.enemyTarget.TakeDamage(gM.buffM.CharacterAttack(gM.buffM.FindBuff(CharacterBuff.Defence).value * 2));
                    gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, -gM.buffM.FindBuff(CharacterBuff.Defence).value, BuffSource.Character);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
