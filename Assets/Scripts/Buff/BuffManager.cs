using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region EnemyBuff说明
//Bored 
//Anxiety
//InFlow
//Vulnerable
//Weak
//Instability
//WeakMind
//Defence
//Skill 技能值 将与设计师的challenge比较 只存在于主要角色为设计师的情况下
//Block 该轮不会受到伤害
//Charge 下回合造成双倍伤害
#endregion

public class BuffInfo1 
{
    public CharacterBuff characterBuffType;
    public EnemyBuff enemyBuffType;
    public GameObject uiObj;
    public BuffTimeType timeType;
    public int lastTime;
    public BuffValueType valueType;
    public int value;
    public bool isReadyToRemove = false;
}

[System.Serializable]
public struct EnemyBuffImage { public EnemyBuff buffType; public Sprite buffImage; }
[System.Serializable]
public struct CharacterBuffImage { public CharacterBuff buffType; public Sprite buffImage; }

public class BuffManager : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;
    public List<BuffInfo> buffInfos;
    public List<RectTransform> chaBuffs = new List<RectTransform>();
    public List<RectTransform> enBuffs = new List<RectTransform>();

    public GameObject buffPrefab;
    public GameObject hoverTip;

    public void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    #region Instantiate Buff
    public void InstantiateBuff(CharacterBuff chaBuff)
    {
        RectTransform chaBuffRect;
        chaBuffRect = Instantiate(gM.buffM.buffPrefab, gM.characterM.mainCharacter.transform.Find("BuffArea"), false).GetComponent<RectTransform>();
        BuffController controller = chaBuffRect.GetComponent<BuffController>();
        controller.buffInfo = ReturnBuffInfo();
        controller.lastTime = ReturnBuffInfo().defaultTime;
        controller.currentValue = ReturnBuffInfo().defaultvalue;
        controller.BuffUISync();
        gM.buffSM.buffTrans = chaBuffRect;
        chaBuffRect.localScale = new Vector3(0, 0, 0);
        chaBuffs.Add(chaBuffRect);

        BuffInfo ReturnBuffInfo()
        {
            foreach (BuffInfo buffInfo in buffInfos)
            {
                if (chaBuff == buffInfo.characterBuffType)
                {
                    return buffInfo;
                }
            }
            return null;
        }
    }

    public void InstantiateBuff(EnemyBuff enBuff)
    {
        RectTransform enBuffRect;
        enBuffRect = Instantiate(gM.buffM.buffPrefab, gM.enM.enemyTarget.transform.Find("BuffArea"), false).GetComponent<RectTransform>();
        BuffController controller = enBuffRect.GetComponent<BuffController>();
        controller.buffInfo = ReturnBuffInfo();
        controller.lastTime = ReturnBuffInfo().defaultTime;
        controller.currentValue = ReturnBuffInfo().defaultvalue;
        controller.BuffUISync();
        gM.buffSM.buffTrans = enBuffRect;
        enBuffRect.localScale = new Vector3(0, 0, 0);
        enBuffs.Add(enBuffRect);

        BuffInfo ReturnBuffInfo()
        {
            foreach (BuffInfo buffInfo in buffInfos)
            {
                if (enBuff == buffInfo.enemyBuffType)
                {
                    return buffInfo;
                }
            }
            return null;
        }
    }
    #endregion

    public void RemoveBuff(EnemyBuff buffType)
    {
        if (FindBuff(buffType) != null)
        {
            Destroy(FindBuff(buffType).gameObject);
            enBuffs.Remove(FindBuff(buffType).GetComponent<RectTransform>());
            //activeEBuffs.Remove(FindBuff(buffType));
        }
    }

    public void RemoveBuff(CharacterBuff buffType)
    {
        if (FindBuff(buffType) != null)
        {
            Destroy(FindBuff(buffType).gameObject);
            chaBuffs.Remove(FindBuff(buffType).GetComponent<RectTransform>());
        }
    }

    public void TempBuffTimeDecrease(List<EnemyBuff> enemyBuffs)
    {
        for (int i = 0; i < enemyBuffs.Count; i++)
        {
            if (FindBuff(enemyBuffs[i])!=null)
            {
                FindBuff(enemyBuffs[i]).lastTime--;
                FindBuff(enemyBuffs[i]).transform.Find("Time").GetComponent<Text>().text = FindBuff(enemyBuffs[i]).lastTime.ToString();
                if (FindBuff(enemyBuffs[i]).lastTime <= 0)
                {
                    RemoveBuff(enemyBuffs[i]);
                }
            }
        }
    }

    public void TempBuffTimeDecrease(List<CharacterBuff> characterBuffs)
    {
        for (int i = 0; i < characterBuffs.Count; i++)
        {
            if (FindBuff(characterBuffs[i]) != null)
            {
                FindBuff(characterBuffs[i]).lastTime--;
                FindBuff(characterBuffs[i]).transform.Find("Time").GetComponent<Text>().text = FindBuff(characterBuffs[i]).lastTime.ToString();
                if (FindBuff(characterBuffs[i]).lastTime <= 0)
                {
                    RemoveBuff(characterBuffs[i]);
                }
            }
        }
    }

    public BuffController FindBuff(CharacterBuff characterBuff)
    {
        for (int i = 0; i < chaBuffs.Count; i++)
        {
            if (characterBuff == chaBuffs[i].GetComponent<BuffController>().buffInfo.characterBuffType)
            {
                return chaBuffs[i].GetComponent<BuffController>();
            }
        }
        return null;
    }

    public BuffController FindBuff(EnemyBuff enemyBuff)
    {
        for (int i = 0; i < enBuffs.Count; i++)
        {
            if (enemyBuff == enBuffs[i].GetComponent<BuffController>().buffInfo.enemyBuffType)
            {
                return enBuffs[i].GetComponent<BuffController>();
            }
        }
        return null;
    }
}
