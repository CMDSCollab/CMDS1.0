using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EB_JosefFames : BasicEnemy
{
    public int maxHealth1P = 25;
    public int maxHealth2P = 60;
    public int damage1P = 10;
    public int damage2P = 5;
    private int enemySequence = 1;
    //private int defaultSkill = 2;

    public override void TakeAction()
    {
        switch (currentIntention)
        {
            case EnemyIntention.Defence:
                gM.actionSM.EnterActionState(gM.actionSM.defenceState, 10);
                break;
            case EnemyIntention.FireShoot:
                gM.actionSM.EnterActionState(gM.actionSM.attackState, damage1P);
                break;
            case EnemyIntention.HoneyShoot:
                gM.actionSM.EnterActionState(gM.actionSM.honeyShootState, damage2P);
                //gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack(damage2P));
                //gM.buffM.SetBuff(CharacterBuff.Inflammable, BuffTimeType.Temporary, 3, BuffValueType.NoValue, 1, BuffSource.Enemy);
                break;
        }
    }

    public override void GenerateEnemyIntention()
    {
        base.GenerateEnemyIntention();
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                if (enemySequence == 1)
                {
                    currentIntention = EnemyIntention.FireShoot;
                    transform.Find("Intention").Find("Value").GetComponent<Text>().text = damage1P.ToString();
                }
                else
                {
                    currentIntention = EnemyIntention.HoneyShoot;
                    transform.Find("Intention").Find("Value").GetComponent<Text>().text = damage2P.ToString();
                }             
                break;
            case EnemyIntention.Defence:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = 10.ToString();
                break;
        }
        SetIntentionUI();
    }

    public override void InitializeEnemyUI()
    {
        enemyName = transform.Find("Name").GetComponent<Text>();
        portrait = transform.Find("PortraitMask").Find("Portrait").GetComponent<Image>();
        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpRatio = transform.Find("HpBar").Find("HpRatio").GetComponent<Text>();

        if (enemySequence == 1)
        {
            portrait.sprite = enemyInfo.images[0];
            enemyName.text = "Josef Fames 1P";
            maxHp = maxHealth1P;
        }
        else
        {
            portrait.sprite = enemyInfo.images[1];
            enemyName.text = "Josef Fames 2P";
            maxHp = maxHealth2P;
        }
        healthPoint = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();

        GenerateEnemyIntention();
    }

    public override void EnemyDefeated()
    {
        if (gM.buffM.FindBuff(EnemyBuff.Revive) == null)
        {
            if (enemySequence == 1)
            {
                enemySequence = 2;
            }
            else
            {
                enemySequence = 1;
            }
            gM.actionSM.EnterActionState(gM.actionSM.reviveState, 1);
        }
        else
        {
            base.EnemyDefeated();
            FindObjectOfType<Panel_Reward>().transform.Find("BackToMap").gameObject.SetActive(false);
            FindObjectOfType<Panel_Reward>().transform.Find("BackToMain").gameObject.SetActive(true);
        }
    }
}
