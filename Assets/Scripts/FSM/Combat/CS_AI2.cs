using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AI2 : CombatBaseState
{
    public override void EnterState(GameMaster gM)
    {
        Debug.Log("AI2CSEntered");
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                gM.aiM.artAI.TakeAction();
                break;
            case CharacterType.Programmmer:
                gM.aiM.artAI.TakeAction();
                break;
            case CharacterType.Artist:
                gM.aiM.proAI.TakeAction();
                break;
        }
    }

    public override void UpdateState(GameMaster gM)
    {
        if (gM.combatSM.isUpdate == true)
        {
            gM.combatSM.isUpdate = false;
            EndState(gM);
        }
    }

    public override void EndState(GameMaster gM)
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Art;
                gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.artAI.energyPoint);
                break;
            case CharacterType.Programmmer:
                gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Art;
                gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.artAI.energyPoint);
                break;
            case CharacterType.Artist:
                gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Pro;
                gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.proAI.energyPoint);
                break;
        }
        gM.combatSM.SwitchCombatState(gM.combatSM.enemyState);
        gM.buffM.LastTimeDecrease("Character", "Enemy");
    }
}
