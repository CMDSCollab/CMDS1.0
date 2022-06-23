using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EM_TechNerd : BasicEnemy
{
    private int defaultDmg = 7;
    private int defaultMultAttackTimes = 10;

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                gM.actionSM.EnterActionState(gM.actionSM.attackState, defaultDmg);
                StartCoroutine(TypeText(enemyInfo.sentences[0]));
                break;
            case EnemyIntention.Charge:
                gM.actionSM.EnterActionState(gM.actionSM.chargeState, 1);
                StartCoroutine(TypeText(enemyInfo.sentences[1]));
                break;
            case EnemyIntention.Block:
                gM.actionSM.EnterActionState(gM.actionSM.blockState, 1);
                StartCoroutine(TypeText(enemyInfo.sentences[2]));
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
            case EnemyIntention.Charge:
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultMultAttackTimes.ToString();
                break;
            //case EnemyIntention.Block:
            //    transform.Find("Intention").Find("Value").gameObject.SetActive(false);
            //    break;
        }
    }
}
