using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_HoneyShoot : AS_Attack
{
    private EnemyBuff[] eBuffs = { EnemyBuff.Weak, EnemyBuff.Anxiety};

    public override void EnterState(GameMaster gM, int value)
    {
        gM.buffSM.valueToCalculate = value;
        gM.buffSM.AddOrAdjustBuff(CharacterBuff.Inflammable);
        gM.buffSM.SetBuffList(eBuffs);
        gM.buffSM.BuffEffectsApply();
    }

    public override void EndState(GameMaster gM, int value)
    {
        gM.actionSM.EnterActionState(gM.actionSM.takeDmgState, value);
    }
}
