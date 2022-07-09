using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CES_MagicCircle : CEffectBaseState
{
    public override void EnterState(GameMaster gM, int value)
    {
        Animator anim = gM.enM.enemyTarget.transform.Find("MagicCircle").GetComponent<Animator>();
        switch (gM.enM.enemyTarget.magicCircleState)
        {
            case MagicCircleState.In:
                if (gM.buffM.FindBuff(EnemyBuff.Vulnerable) == null)
                {
                    gM.buffSM.AddOrAdjustBuff(EnemyBuff.Vulnerable);
                }
                anim.SetBool("IsMagicCircleIn", true);
                break;
            case MagicCircleState.Out:
                anim.SetBool("IsMagicCircleIn", false);
                break;
        }
        EndState(gM, value);
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        
    }

    public override void EndState(GameMaster gM, int value)
    {
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.actionSM.isUpdate = true;
        }
        if (gM.cEffectSM.isCardEffectRunning == true)
        {
            gM.cEffectSM.CardEffectsApply(gM.cEffectSM.cardInUse);
        }
    }

    public void MagicCirleRecapture(GameMaster gM)
    {
        //gM.enM.enemyTarget.magicCircleState = MagicCircleState.In;
        //gM.enM.enemyTarget.transform.Find("MagicCircle").gameObject.SetActive(true);
        //gM.buffM.SetBuff(EnemyBuff.Weak, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
        //gM.buffM.SetBuff(EnemyBuff.Vulnerable, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
    }
}
