using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_HoneyShoot : AS_Attack
{
    public override void EndState(GameMaster gM, int value)
    {
        gM.actionSM.EnterActionState(gM.actionSM.takeDmgState, value);
        gM.buffSM.AddOrAdjustBuff(CharacterBuff.Inflammable);
    }
}
