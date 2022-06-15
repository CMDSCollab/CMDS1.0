using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_End : CombatBaseState
{
    public override void EnterState(GameMaster gM)
    {
        gM.deckM.DrawCardFromDeckRandomly(gM.deckM.drawCardAmount);

        if (gM.deckM.cardInDeckCopy.Count < 1)
        {
            gM.deckM.GetNewCopyDeck();
            gM.cardRepoM.discardPile.Clear(); //清空弃牌堆内卡牌
        }

        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //同步弃牌堆卡牌数量展示Text
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
        EndState(gM);
    }

    public override void UpdateState(GameMaster gM)
    {

    }

    public override void EndState(GameMaster gM)
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                gM.aiM.des.flow.GetComponent<FlowManager>().AddDot();
                break;
            case CharacterType.Programmmer:
                break;
            case CharacterType.Artist:
                break;
        }
        gM.combatSM.currentState = gM.combatSM.startState;
    }
}
