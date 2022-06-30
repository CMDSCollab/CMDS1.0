using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuffType
{
    Shield,
    Vengeance,
    PowerUp,
    Challenge
}

public class CharacterMate : BasicCharacter
{
    public int maxHp;
    public int healthPoint;
    public int shieldPoint;
    public bool isDefeated;
    public Slider hpBar;
    public Text hpRatio;

    public virtual void Start()
    {
        //base.Start();
        InitializeCharacter();
    }

    public void CharacterDefeated()
    {
        //gM.uiCanvas.transform.Find("StatisticPanel").gameObject.SetActive(true);
        gM.panelM.InstantiatePanel(PanelType.Reward);
    }

    public void Update()
    {
    //    if (healthPoint <= 0 && !isDefeated)
    //    {
    //        isDefeated = true;
    //        CharacterDefeated();
    //}
    }

    private void InitializeCharacter()
    {
        //maxHp = characterInfo.maxHp;
        //healthPoint = maxHp;
        maxHp = gM.comStatusBar.maxHealth;
        healthPoint = gM.comStatusBar.healthPoint;
        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpRatio = hpBar.transform.Find("HpRatio").GetComponent<Text>();
        hpBar.maxValue = maxHp;
        SyncCharacterUI();
    }

    public virtual void SyncCharacterUI()
    {
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();
        //CommonStatusBar statusBar = FindObjectOfType<CommonStatusBar>();
    }

    public virtual void TakeDamage(int dmg)
    {
        healthPoint -= dmg;
        gM.comStatusBar.HealthUIUpdate();
    }

    public virtual void HealSelf(int healAmount)
    {
        healthPoint += healAmount;
        if (healthPoint >= maxHp)
        {
            healthPoint = maxHp;
        }
        gM.comStatusBar.HealthUIUpdate();
    }
}
