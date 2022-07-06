using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AS_ChangeIntention : ActionBaseState
{
    private bool firstState = true;
    public override void EnterState(GameMaster gM, int value)
    {
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            switch (gM.characterM.mainCharacterType)
            {
                case CharacterType.Designer:
                    foreach (EnemyIntentionRatio ratio in gM.enM.enemyTarget.enemyInfo.basicIntentions)
                    {
                        if (gM.enM.enemyTarget.currentIntention == ratio.intention)
                        {
                            gM.cEffectSM.EnterCardState(gM.cEffectSM.skillCState, ratio.skillUp);
                        }
                    }
                    //gM.enM.enemyTarget.MainChaMCChange();
                    break;
                case CharacterType.Programmmer:
                    gM.actionSM.isUpdate = true;
                    break;
                case CharacterType.Artist:
                    gM.actionSM.isUpdate = true;
                    break;
            }
        }
        else
        {
            if (gM.combatSM.currentState == gM.combatSM.ai1State)
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
            }
            else if (gM.combatSM.currentState == gM.combatSM.ai2State)
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
            }
            gM.actionSM.isUpdate = true;
        }
    }
    public override void BeforeUpdate(GameMaster gM, int value)
    {

    }
    public override void UpdateState(GameMaster gM, int value)
    {
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            RectTransform intentionRect = gM.enM.enemyTarget.transform.Find("Intention").GetComponent<RectTransform>();
            if (firstState == true)
            {
                intentionRect.Rotate(0, 5f, 0);
                if (intentionRect.rotation.y >= 0.5)
                {
                    firstState = false;
                    gM.enM.enemyTarget.GetComponent<BasicEnemy>().GenerateEnemyIntention();
                }
            }
            else
            {
                intentionRect.Rotate(0, -5f, 0);
                if (intentionRect.rotation.y <= 0.05)
                {
                    firstState = true;
                    gM.actionSM.isUpdate = false;
                    EndState(gM, value);
                }
            }
        }
        else
        {
            RectTransform intentionRect = CurrentAITarget(gM).Find("IntentionPos").GetComponent<RectTransform>();
            if (firstState == true)
            {
                intentionRect.Rotate(0, 5f, 0);
                if (intentionRect.rotation.y >= 0.5)
                {
                    firstState = false;
                    CurrentAITarget(gM).GetComponent<AIMate>().GenerateIntention();
                }
            }
            else
            {
                intentionRect.Rotate(0, -5f, 0);
                if (intentionRect.rotation.y <= 0.05)
                {
                    firstState = true;
                    gM.actionSM.isUpdate = false;
                    EndState(gM, value);
                }
            }
        }
    }
    public override void AfterUpdate(GameMaster gM, int value)
    {
        throw new System.NotImplementedException();
    }
    public override void EndState(GameMaster gM, int value)
    {
        gM.combatSM.isUpdate = true;
    }
}
