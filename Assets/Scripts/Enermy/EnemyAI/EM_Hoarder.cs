using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM_Hoarder : BasicEnemy
{
    private int defaultDmg = 6;

    private int goldBonus = 50;
    private int sheildOnSleep = 15;
    private bool hasBeenAwaken = false;

    void Start()
    {
      
    }

    public override void TakeDamage(int dmgValue)
    {
        base.TakeDamage(dmgValue);
        if (!hasBeenAwaken)
        {
            if (gM.buffM.FindBuff(EnemyBuff.Defence) != null)
            {
                if (gM.buffM.FindBuff(EnemyBuff.Defence).value <= 0)
                {
                    this.SetAwaken();
                }
            }
            else
            {
                this.SetAwaken();
            }

        }

    }

    private void SetAwaken()
    {
        hasBeenAwaken = true;
        GenerateEnemyIntention();
        //Debug.Log("Ï²+1µ³ ÒÑËÕÐÑ£¡");
    }

    public override void GenerateEnemyIntention()
    {
        if (!hasBeenAwaken)
        {
            List<CombatBaseState> enemyFirstSequence = new List<CombatBaseState>();
            enemyFirstSequence.Add(gM.combatSM.startState);
            enemyFirstSequence.Add(gM.combatSM.enemyState);
            enemyFirstSequence.Add(gM.combatSM.ai1State);
            enemyFirstSequence.Add(gM.combatSM.ai2State);
            enemyFirstSequence.Add(gM.combatSM.endState);
            gM.combatSM.runningSequence = enemyFirstSequence;
            currentIntention = EnemyIntention.Sleep;
            transform.Find("Intention").Find("Value").gameObject.SetActive(false);
            SetIntentionUI();
        }
        else
        {
            gM.combatSM.runningSequence = gM.combatSM.defaultSequence;
            base.GenerateEnemyIntention();
            switch (currentIntention)
            {
                case EnemyIntention.Attack:
                    transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultDmg.ToString();
                    break;
            }
        }
    }

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Sleep:
                gM.actionSM.EnterActionState(gM.actionSM.defenceState, sheildOnSleep);
                sheildOnSleep -= 2;
                goldBonus -= 5;
                break;
            case EnemyIntention.Attack:
                gM.actionSM.EnterActionState(gM.actionSM.attackState, defaultDmg);
                break;
            case EnemyIntention.Taunt:
                gM.actionSM.EnterActionState(gM.actionSM.tauntState, 1);
                break;
        }
    }
}
