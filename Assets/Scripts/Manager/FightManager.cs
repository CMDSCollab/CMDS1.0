using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public GameMaster gM;
    public float actionTime;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    //public void FightProcessManager()
    //{
    //    // ����ҽ�ɫ���лغ�ĩ����
    //    if (gM.characterM.mainCharacterType == CharacterType.Programmmer)
    //    {
    //        gM.aiM.pro.OnPlayerTurnEnded();
    //    }
    //    else if(gM.characterM.mainCharacterType == CharacterType.Designer)
    //    {
    //        gM.aiM.des.ChallengeDMG();
    //    }
    //    else if(gM.characterM.mainCharacterType == CharacterType.Artist)
    //    {
    //        gM.aiM.art.StyleEffect();
    //    }

    ////    //���ƶ���

    //    if (gM.handM.handCardList.Count > 0)
    //    {
    //        for (int i = gM.handM.handCardList.Count; i > 0; i--)
    //        {
    //            gM.handM.handCardList[i - 1].GetComponent<CardManager>().DiscardHandCard();
    //        }
    //    }
    //    // AI�����ж�
    //    switch (gM.characterM.mainCharacterType)
    //    {
    //        case CharacterType.Designer:
    //            gM.aiM.proAI.TakeAction();
    //            gM.aiM.artAI.TakeAction();
    //            break;
    //        case CharacterType.Programmmer:
    //            gM.aiM.desAI.TakeAction();
    //            gM.aiM.artAI.TakeAction();
    //            break;
    //        case CharacterType.Artist:
    //            gM.aiM.desAI.TakeAction();
    //            gM.aiM.proAI.TakeAction();
    //            break;
    //    }

    //    gM.buffM.LastTimeDecrease("Character", "Enemy");
    //    gM.buffM.LastTimeDecrease("Enemy", "Enemy");
    //    // ִ���ж�
    //    gM.enM.enemyTarget.TakeAction();

    //    gM.buffM.LastTimeDecrease("Enemy", "Character");
    //    gM.buffM.LastTimeDecrease("Character", "Character");


    //    gM.deckM.DrawCardFromDeckRandomly(gM.deckM.drawCardAmount);

    //    if (gM.deckM.cardInDeckCopy.Count < 1)
    //    {
    //        gM.deckM.GetNewCopyDeck();
    //        gM.cardRepoM.discardPile.Clear(); //������ƶ��ڿ���
    //    }

    //    gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //ͬ�����ƶѿ�������չʾText
    //    gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
    //}
        }
