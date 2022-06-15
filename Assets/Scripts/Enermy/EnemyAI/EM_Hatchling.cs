using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM_Hatchling : BasicEnemy
{
    private int defaultDmg = 8;
    private int sheildOnLowHP = 10;

    private bool hasTaunted = false;
    private bool hasDefenced = false;
  

    public override void GenerateEnemyIntention()
    {
        if (!hasTaunted)
        {
            currentIntention = EnemyIntention.Taunt;
            SetIntentionUI();
        }
        else 
        {
            if (healthPoint < 0.5 * maxHp && !hasDefenced)
            {
                currentIntention = EnemyIntention.Defence;
                SetIntentionUI();
            }
            else
                base.GenerateEnemyIntention();
        }
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultDmg.ToString();
                break;
        }
    }

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.actionSM.EnterActionState(gM.actionSM.attackState, defaultDmg);
                break;
            case EnemyIntention.Defence:
                gM.actionSM.EnterActionState(gM.actionSM.defenceState, sheildOnLowHP);
                hasDefenced = true;
                break;
            case EnemyIntention.Taunt:
                gM.actionSM.EnterActionState(gM.actionSM.tauntState, 1);
                hasTaunted = true;
                break;
        }
    }
}
