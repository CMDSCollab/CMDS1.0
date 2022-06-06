using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AI1 : CombatBaseState
{
    public override void EnterState(GameMaster gM)
    {
        //Debug.Log("AI1CSEntered");
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                gM.aiM.proAI.TakeAction();
                //gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Pro;
                //gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.proAI.energyPoint);
                break;
            case CharacterType.Programmmer:
                gM.aiM.desAI.TakeAction();
                //gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Des;
                //gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.desAI.energyPoint);
                break;
            case CharacterType.Artist:
                gM.aiM.desAI.TakeAction();
                //gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Des;
                //gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.desAI.energyPoint);
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
                gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Pro;
                gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.proAI.energyPoint);
                break;
            case CharacterType.Programmmer:
                gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Des;
                gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.desAI.energyPoint);
                break;
            case CharacterType.Artist:
                gM.cEffectSM.aiTarget = CEffectStateManager.AITarget.Des;
                gM.cEffectSM.EnterCardState(gM.cEffectSM.addEnergyState, -gM.aiM.desAI.energyPoint);
                break;
        }
        gM.combatSM.SwitchCombatState(gM.combatSM.ai2State);
    }
}
