using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy : CombatBaseState
{
    bool isBeforeAction = false;
    EnemyBuff[] enterEListRecord = { EnemyBuff.Block, EnemyBuff.Defence, EnemyBuff.Charge };
    CharacterBuff[] endCListRecord = { CharacterBuff.IsTeamWork, CharacterBuff.IsSycn, CharacterBuff.Defence };
    List<EnemyBuff> enterEList = new List<EnemyBuff>();
    List<CharacterBuff> enterCList = new List<CharacterBuff>();
    List<EnemyBuff> endEList = new List<EnemyBuff>();
    List<CharacterBuff> endCList = new List<CharacterBuff>();


    public override void EnterState(GameMaster gM)
    {
        TempBuffDecreaseDetermination(gM);
        gM.buffM.TempBuffTimeDecrease(enterEList);
        gM.buffM.TempBuffTimeDecrease(enterCList);
        gM.enM.enemyTarget.TakeAction();
    }

    public override void UpdateState(GameMaster gM)
    {
        if (gM.combatSM.isUpdate == true)
        {
            gM.combatSM.isUpdate = false;
            EndState(gM);
        }
    }

    public override void EndState(GameMaster gM)
    {
        gM.buffM.TempBuffTimeDecrease(endEList);
        gM.buffM.TempBuffTimeDecrease(endCList);
        gM.combatSM.SwitchCombatState(gM.combatSM.endState);
    }

    public void TempBuffDecreaseDetermination(GameMaster gM)
    {
        enterEList.Clear();
        enterCList.Clear();
        endEList.Clear();
        endCList.Clear();
        for (int i = 0; i < enterEListRecord.Length; i++)
        {
            enterEList.Add(enterEListRecord[i]);
        }
        for (int i = 0; i < endCListRecord.Length; i++)
        {
            endCList.Add(endCListRecord[i]);
        }
        switch (gM.enM.enemyTarget.currentIntention)
        {
            case EnemyIntention.Attack:
                enterEList.Remove(EnemyBuff.Charge);
                endEList.Add(EnemyBuff.Charge);
                break;
            case EnemyIntention.Defence:
                break;
            case EnemyIntention.Charge:
                break;
            case EnemyIntention.Block:
                break;
            case EnemyIntention.Taunt:
                break;
            case EnemyIntention.Heal:
                break;
            case EnemyIntention.ToComment:
                break;
            case EnemyIntention.Comment:
                break;
            case EnemyIntention.Revive:
                break;
            case EnemyIntention.FireShoot:
                break;
            case EnemyIntention.HoneyShoot:
                break;
            case EnemyIntention.Sleep:
                break;
        }
    }
}
