using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_TakeDmg : ActionBaseState
{
    private EnemyBuff[] eBuffs = { EnemyBuff.Vulnerable, EnemyBuff.Block, EnemyBuff.Defence};
    private CharacterBuff[] cBuffs = { CharacterBuff.Defence, CharacterBuff.Vengeance, CharacterBuff.Inflammable };

    public override void EnterState(GameMaster gM, int value)
    {
        gM.buffSM.valueToCalculate = value;
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.buffSM.SetBuffList(cBuffs);
        }
        else
        {
            gM.buffSM.SetBuffList(eBuffs);
        }
        gM.buffSM.BuffEffectsApply();
    }

    public override void BeforeUpdate(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            gM.characterM.mainCharacter.TakeDamage(gM.actionSM.changedValue);
        }
        else
        {
            gM.enM.enemyTarget.TakeDamage(gM.actionSM.changedValue);
        }
        gM.actionSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        if (gM.combatSM.currentState == gM.combatSM.enemyState)
        {
            if (gM.animC.isAnimEntered == false)
            {
                gM.animC.SetHpSliderMoveValue(gM.characterM.mainCharacter.hpBar, gM.characterM.mainCharacter.healthPoint);
                gM.animC.currentChoice = AnimChoice.HpSliderMove;
            }
            if (gM.animC.isAnimEnd == true)
            {
                gM.actionSM.isUpdate = false;
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
                gM.characterM.mainCharacter.hpRatio.text = gM.characterM.mainCharacter.healthPoint.ToString() + "/" + gM.characterM.mainCharacter.maxHp.ToString();
                EndState(gM, value);
            }
        }
        else
        {
            if (gM.animC.isAnimEntered == false)
            {
                gM.animC.SetHpSliderMoveValue(gM.enM.enemyTarget.hpBar, gM.enM.enemyTarget.healthPoint);
                gM.animC.currentChoice = AnimChoice.HpSliderMove;
            }
            if (gM.animC.isAnimEnd == true)
            {
                gM.actionSM.isUpdate = false;
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
                gM.enM.enemyTarget.hpRatio.text = gM.enM.enemyTarget.healthPoint.ToString() + "/" + gM.enM.enemyTarget.maxHp.ToString();
                EndState(gM, value);
            }
        }
    }

    public override void AfterUpdate(GameMaster gM, int value)
    {
        throw new System.NotImplementedException();
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
            gM.actionSM.EnterActionState(gM.actionSM.changeIState, value);
        }
    }
}
