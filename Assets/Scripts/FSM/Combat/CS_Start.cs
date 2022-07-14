using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Start : CombatBaseState
{
    public override void EnterState(GameMaster gM)
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                //gM.aiM.des.ChallengeDMG();
                break;
            case CharacterType.Programmmer:
                gM.aiM.pro.OnPlayerTurnEnded();
                break;
            case CharacterType.Artist:
                gM.aiM.art.StyleEffect();
                break;
        }
        //ÊÖÅÆ¶ªÆú
        if (gM.handM.handCardList.Count > 0)
        {
            for (int i = gM.handM.handCardList.Count; i > 0; i--)
            {
                gM.handM.handCardList[i - 1].GetComponent<CardManager>().cardMovement = CardMovement.Discard;
          
                //gM.handM.handCardList[i - 1].GetComponent<CardManager>().DiscardHandCard();
                //gM.handM.handCardList.RemoveAt(gM.handM.handCardList[i - 1].GetComponent<CardManager>().handIndex - 1);
            }
            gM.cardSM.discardType = DiscardType.AllHand;
            gM.cardSM.EnterCardState(gM.cardSM.discardState);
        }
        EndState(gM);
    }

    public override void UpdateState(GameMaster gM)
    {

    }

    public override void EndState(GameMaster gM)
    {
        gM.combatSM.SwitchCombatState();
    }
}
