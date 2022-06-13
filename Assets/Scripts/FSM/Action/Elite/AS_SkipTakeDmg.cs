using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_SkipTakeDmg :AS_TakeDmg
{
    public override void EnterState(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        gM.characterM.mainCharacter.TakeDamage(gM.actionSM.changedValue);
        gM.actionSM.isUpdate = true;
    }

    public override void BeforeUpdate(GameMaster gM, int value)
    {

    }

    public override void EndState(GameMaster gM, int value)
    {
        if (gM.enM.enemyTarget.healthPoint <= 0)
        {
            gM.enM.enemyTarget.EnemyDefeated();
        }
        else if (gM.characterM.mainCharacter.healthPoint <= 0)
        {
            gM.characterM.mainCharacter.CharacterDefeated();
        }
        else
        {
            EE_SpeedRunner ee = (EE_SpeedRunner)gM.enM.enemyTarget;
            if (ee.isSkip == false)
            {
                gM.actionSM.EnterActionState(gM.actionSM.changeIState, value);
            }
            else
            {
                gM.actionSM.EnterActionState(gM.actionSM.skipCIState, value);
            }
        }
    }
}
