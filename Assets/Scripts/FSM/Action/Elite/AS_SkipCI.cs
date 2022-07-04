using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_SkipCI : AS_ChangeIntention
{
    private bool firstState = true;
    private static int skipAttackCount = 0;
    private EE_SpeedRunner ee;

    public override void EnterState(GameMaster gM, int value)
    {
        if (gM.enM.enemyTarget is EE_SpeedRunner)
        {
            ee = (EE_SpeedRunner)gM.enM.enemyTarget;
        }
        if (skipAttackCount >= 2)
        {
            ee.isSkip = false;
            skipAttackCount = 0;
        }
        gM.actionSM.changedValue = value;
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
                break;
            case CharacterType.Programmmer:
                gM.actionSM.isUpdate = true;
                break;
            case CharacterType.Artist:
                gM.actionSM.isUpdate = true;
                break;
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
    }

    public override void EndState(GameMaster gM, int value)
    {
        if (ee.isSkip == false)
        {
            gM.combatSM.isUpdate = true;
        }
        else
        {
            skipAttackCount++;
            gM.actionSM.EnterActionState(gM.actionSM.skipAttackState, value);
        }
    }
}
