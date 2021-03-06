using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Anxiety : BuffBaseState
{
    public override void EnterState(GameMaster gM)
    {
        switch (gM.buffSM.buffUsage)
        {
            case BuffUsage.AddNew:
                gM.buffM.InstantiateBuff(EnemyBuff.Anxiety);
                //gM.buffSM.AddNewBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1);
                //gM.buffSM.buffTrans = gM.buffSM.GetBuffRectTrans(EnemyBuff.Anxiety);
                //gM.buffSM.buffTrans.localScale = new Vector3(0, 0, 0);
                break;
            case BuffUsage.Adjust:
                break;
            case BuffUsage.EffectApply:
                gM.buffSM.valueToCalculate += 2;
                break;
        }
        gM.buffSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM)
    {
        switch (gM.buffSM.buffUsage)
        {
            case BuffUsage.EffectApply:
                if (gM.animC.isAnimEntered == false)
                {
                    gM.animC.SetScaleBounceValue(gM.buffSM.buffTrans, 1, 0.6f, 1);
                    gM.animC.currentChoice = AnimChoice.ScaleBounce;
                }
                if (gM.animC.isAnimEnd == true)
                {
                    gM.buffSM.isUpdate = false;
                    EndState(gM);
                    gM.animC.isAnimEnd = false;
                    gM.animC.isAnimEntered = false;
                }
                break;
            case BuffUsage.AddNew:
                if (gM.animC.isAnimEntered == false)
                {
                    gM.animC.SetBuffValueChangeValue(gM.buffSM.buffTrans, 0f, 1.2f, 1f, 1);
                    gM.animC.currentChoice = AnimChoice.BuffValueChange;
                }
                if (gM.animC.isAnimEnd == true)
                {
                    gM.buffSM.isUpdate = false;
                    EndState(gM);
                    gM.animC.isAnimEnd = false;
                    gM.animC.isAnimEntered = false;
                }
                break;
            case BuffUsage.Adjust:
                if (gM.animC.isAnimEntered == false)
                {
                    gM.animC.SetScaleBounceValue(gM.buffSM.buffTrans, 1, 0.8f, 1);
                    gM.animC.currentChoice = AnimChoice.ScaleBounce;
                }
                if (gM.animC.isAnimEnd == true)
                {
                    gM.buffSM.isUpdate = false;
                    EndState(gM);
                    gM.animC.isAnimEnd = false;
                    gM.animC.isAnimEntered = false;
                }
                break;
        }
    }

    public override void EndState(GameMaster gM)
    {
        switch (gM.buffSM.buffUsage)
        {
            case BuffUsage.AddNew:
                gM.buffM.RemoveBuff(EnemyBuff.Vulnerable);
                gM.enM.enemyTarget.magicCircleState = MagicCircleState.Out;
                gM.cEffectSM.EnterCardState(gM.cEffectSM.magicCircleState, 0);
                break;
            case BuffUsage.Adjust:
                gM.cEffectSM.EnterCardState(gM.cEffectSM.magicCircleState, 0);
                break;
            case BuffUsage.EffectApply:
                gM.buffSM.BuffEffectsApply();
                break;
        }
    }
}
