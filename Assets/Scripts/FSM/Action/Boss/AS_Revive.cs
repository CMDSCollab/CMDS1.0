using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AS_Revive : ActionBaseState
{
    public override void EnterState(GameMaster gM, int value)
    {
        Debug.Log("Entered");
        gM.enM.enemyTarget.InitializeEnemyUI();
        gM.buffSM.AddOrAdjustBuff(EnemyBuff.Revive);
        gM.actionSM.isUpdate = true;
    }
    public override void BeforeUpdate(GameMaster gM, int value)
    {

    }
    public override void UpdateState(GameMaster gM, int value)
    {
        if (gM.buffM.FindBuff(EnemyBuff.Revive))
        {
            gM.actionSM.isUpdate = false;
            EndState(gM, value);
        }
    }
    public override void AfterUpdate(GameMaster gM, int value)
    {
        throw new System.NotImplementedException();
    }
    public override void EndState(GameMaster gM, int value)
    {
        if (gM.combatSM.currentState == gM.combatSM.ai1State)
        {
            gM.combatSM.isUpdate = true;
        }
        else if (gM.combatSM.currentState == gM.combatSM.ai2State)
        {
            gM.combatSM.isUpdate = true;
        }
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.enM.enemyTarget.TakeAction();
        }
    }
}
