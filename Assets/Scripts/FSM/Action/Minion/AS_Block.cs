using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_Block : ActionBaseState
{
    public override void EnterState(GameMaster gM, int value)
    {
        gM.actionSM.isUpdate = true;
    }
    public override void BeforeUpdate(GameMaster gM, int value)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        if (gM.animC.isAnimEntered == false)
        {
            gM.animC.SetScaleBounceValue(gM.enM.enemyTarget.transform.Find("Intention").Find("Image").GetComponent<RectTransform>(), 1f, 0.6f, 1f);
            gM.animC.currentChoice = AnimChoice.ScaleBounce;
        }
        if (gM.animC.isAnimEnd == true)
        {
            gM.actionSM.isUpdate = false;
            gM.animC.isAnimEnd = false;
            gM.animC.isAnimEntered = false;
            AfterUpdate(gM, value);
        }
    }
    public override void AfterUpdate(GameMaster gM, int value)
    {
        gM.buffSM.AddOrAdjustBuff(EnemyBuff.Block);
    }
    public override void EndState(GameMaster gM, int value)
    {
        gM.actionSM.EnterActionState(gM.actionSM.changeIState, value);
    }
}
