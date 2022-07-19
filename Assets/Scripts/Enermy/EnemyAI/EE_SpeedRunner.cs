using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EE_SpeedRunner : BasicEnemy
{
    public int defaultDmg = 7;
    public bool isSkip = false;

    public override void GenerateEnemyIntention()
    {
        if (isSkip == true)
        {
            transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Attack";
            foreach (EnemyIntentionImages intentionImage in gM.enM.intentionImages)
            {
                if (intentionImage.enemyIntention == EnemyIntention.Attack)
                {
                    transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = intentionImage.image;
                }
            }
            transform.Find("Intention").Find("Value").gameObject.SetActive(true);
            transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultDmg.ToString();
        }
        else
        {
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
            case EnemyIntention.Attack:
                StartCoroutine(TypeText(enemyInfo.sentences[0]));
                gM.actionSM.EnterActionState(gM.actionSM.skipAttackState, defaultDmg);
                break;
            case EnemyIntention.Skip:
                StartCoroutine(TypeText(enemyInfo.sentences[1]));
                gM.actionSM.EnterActionState(gM.actionSM.skipCIState, defaultDmg);
                isSkip = true;
                break;
        }
    }
}
