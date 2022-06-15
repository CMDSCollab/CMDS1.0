using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM_ESPlayerMature : BasicEnemy
{
    private int defaultShieldP = 10;
    private int defaultDmg = 10;

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.actionSM.EnterActionState(gM.actionSM.attackState, defaultDmg);
                break;
            case EnemyIntention.Defence:
                gM.actionSM.EnterActionState(gM.actionSM.defenceState, defaultShieldP);
                break;
            case EnemyIntention.Taunt:
                gM.actionSM.EnterActionState(gM.actionSM.tauntState, 1);
                break;
        }
    }

    public override void GenerateEnemyIntention()
    {
        base.GenerateEnemyIntention();
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultDmg.ToString();
                break;
            case EnemyIntention.Defence:
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultShieldP.ToString();
                break;
            //case EnemyIntention.Taunt:
            //    transform.Find("Intention").Find("Value").gameObject.SetActive(false);
            //    break;
        }
    }
}
