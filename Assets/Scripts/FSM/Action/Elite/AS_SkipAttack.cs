using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_SkipAttack : AS_Attack
{
    public override void EnterState(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        gM.actionSM.isUpdate = true;
    }

    public override void EndState(GameMaster gM, int value)
    {
        gM.actionSM.EnterActionState(gM.actionSM.skipTakeDmgState, value);
    }
}
