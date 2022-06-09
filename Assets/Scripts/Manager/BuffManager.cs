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
public enum BuffSource { Character,AI,Enemy}
public enum BuffTimeType { Permanent,Temporary }
public enum BuffValueType { NoValue, SetValue, AddValue}
public enum EnemyBuff { Bored, Anxiety, InFlow, Vulnerable, Weak, Instability, WeakMind, Defence, Block, Charge, PartialInvincibility,Revive }
public enum CharacterBuff { Defence, Vengeance, PowerUp, Vulnerable, Weak, Inflammable, IsTeamWork, IsSycn}
public class BuffInfo 
{
    public CharacterBuff characterBuffType;
    public EnemyBuff enemyBuffType;
    public GameObject uiObj;
    public BuffTimeType timeType;
    public int lastTime;
    public BuffValueType valueType;
    public int value;
    public bool isReadyToRemove = false;
    public BuffSource source;
}

[System.Serializable]
public struct EnemyBuffImage { public EnemyBuff buffType; public Sprite buffImage; }
[System.Serializable]
public struct CharacterBuffImage { public CharacterBuff buffType; public Sprite buffImage; }

public class BuffManager : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;
    public List<CharacterBuffImage> characterBuffs;
    public List<EnemyBuffImage> enemyBuffs;
    public GameObject buffPrefab;

    public List<BuffInfo> activeCBuffs = new List<BuffInfo>();
    public List<BuffInfo> activeEBuffs = new List<BuffInfo>();

    public void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void SetBuff(CharacterBuff buffType, BuffTimeType timeType, int lastTime, BuffValueType valueType, int value,BuffSource buffSource)
    {
        bool isBuffAlreadyExist = false;
        //判定当前是否存在该buff，已有的话对于数据内容进行改写
        for (int i = 0; i < activeCBuffs.Count; i++)
        {
            if (activeCBuffs[i].characterBuffType == buffType)
            {
                isBuffAlreadyExist = true;
                activeCBuffs[i].lastTime = lastTime;
                activeCBuffs[i].valueType = valueType;
                activeCBuffs[i].source = buffSource;
                switch (valueType)
                {
                    case BuffValueType.NoValue:
                        activeCBuffs[i].value = value;
                        break;
                    case BuffValueType.SetValue:
                        activeCBuffs[i].value = value;
                        break;
                    case BuffValueType.AddValue:
                        activeCBuffs[i].value += value;
                        break;
                }
                activeCBuffs[i].isReadyToRemove = CheckBuffIsReadyForRemove(activeCBuffs[i]);
                SyncBuffUI(activeCBuffs[i]);
                CheckBuffAndRemove(activeCBuffs[i].characterBuffType);
            }
        }
        //当判定为新buff，创建buff和初始化buff
        if (isBuffAlreadyExist == false)
        {
            BuffInfo buffInfo = new BuffInfo();
            buffInfo.characterBuffType = buffType;
            buffInfo.timeType = timeType;
            buffInfo.lastTime = lastTime;
            buffInfo.valueType = valueType;
            buffInfo.value = value;
            buffInfo.source = buffSource;
            buffInfo.uiObj = Instantiate(buffPrefab, gM.characterM.mainCharacter.transform.Find("BuffArea"), false);
            buffInfo.isReadyToRemove = CheckBuffIsReadyForRemove(buffInfo);
            foreach (CharacterBuffImage buff in characterBuffs)
            {
                if (buff.buffType == buffType)
                {
                    buffInfo.uiObj.GetComponent<Image>().sprite = buff.buffImage;
                }
            }
            SyncBuffUI(buffInfo);
            activeCBuffs.Add(buffInfo);
            CheckBuffAndRemove(buffInfo.characterBuffType);
        }
    }

    public void SetBuff(EnemyBuff buffType, BuffTimeType timeType, int lastTime, BuffValueType valueType, int value, BuffSource buffSource)
    {
        bool isBuffAlreadyExist = false;
        //判定当前是否存在该buff，已有的话对于数据内容进行改写
        for (int i = 0; i < activeEBuffs.Count; i++)
        {
            if (activeEBuffs[i].enemyBuffType == buffType)
            {
                isBuffAlreadyExist = true;
                activeEBuffs[i].lastTime = lastTime;
                activeEBuffs[i].valueType = valueType;
                activeEBuffs[i].source = buffSource;
                switch (valueType)
                {
                    case BuffValueType.NoValue:
                        activeEBuffs[i].value = value;
                        break;
                    case BuffValueType.SetValue:
                        activeEBuffs[i].value = value;
                        break;
                    case BuffValueType.AddValue:
                        activeEBuffs[i].value += value;
                        break;
                }
                activeEBuffs[i].isReadyToRemove = CheckBuffIsReadyForRemove(activeEBuffs[i]);
                SyncBuffUI(activeEBuffs[i]);
                CheckBuffAndRemove(activeEBuffs[i].enemyBuffType);
            }
        }
        //当判定为新buff，创建buff和初始化buff
        if (isBuffAlreadyExist == false)
        {
            BuffInfo buffInfo = new BuffInfo();
            buffInfo.enemyBuffType = buffType;
            buffInfo.timeType = timeType;
            buffInfo.lastTime = lastTime;
            buffInfo.valueType = valueType;
            buffInfo.value = value;
            buffInfo.source = buffSource;
            buffInfo.uiObj = Instantiate(buffPrefab, gM.enM.enemyTarget.transform.Find("BuffArea"), false);
            buffInfo.isReadyToRemove = CheckBuffIsReadyForRemove(buffInfo);
            foreach (EnemyBuffImage buff in enemyBuffs)
            {
                if (buff.buffType == buffType)
                {
                    buffInfo.uiObj.GetComponent<Image>().sprite = buff.buffImage;
                }
            }
            SyncBuffUI(buffInfo);
            activeEBuffs.Add(buffInfo);
            CheckBuffAndRemove(buffInfo.enemyBuffType);
        }
    }

    public bool CheckBuffIsReadyForRemove(BuffInfo buff)
    {
        switch (buff.valueType)
        {
            case BuffValueType.NoValue:
                if (buff.value == 0)
                {
                    return true;
                }
                break;
            case BuffValueType.SetValue:
                if (buff.value <= 0)
                {
                    return true;
                }
                break;
            case BuffValueType.AddValue:
                if (buff.value <= 0)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public void CheckBuffAndRemove(CharacterBuff buffType)
    {
        if (FindBuff(buffType).isReadyToRemove == true)
        {
            Destroy(FindBuff(buffType).uiObj);
            activeCBuffs.Remove(FindBuff(buffType));
        }
    }

    public void CheckBuffAndRemove(EnemyBuff buffType)
    {
        if (FindBuff(buffType).isReadyToRemove == true)
        {
            Destroy(FindBuff(buffType).uiObj);
            activeEBuffs.Remove(FindBuff(buffType));
        }
    }

    public void RemoveBuff(EnemyBuff buffType)
    {
        if (FindBuff(buffType) != null)
        {
            Destroy(FindBuff(buffType).uiObj);
            activeEBuffs.Remove(FindBuff(buffType));
        }
    }

    public void RemoveBuff(CharacterBuff buffType)
    {
        if (FindBuff(buffType) != null)
        {
            Destroy(FindBuff(buffType).uiObj);
            activeCBuffs.Remove(FindBuff(buffType));
        }
    }

    public void TempBuffTimeDecrease(List<EnemyBuff> enemyBuffs)
    {
        for (int i = 0; i < enemyBuffs.Count; i++)
        {
            if (FindBuff(enemyBuffs[i])!=null)
            {
                FindBuff(enemyBuffs[i]).lastTime--;
                FindBuff(enemyBuffs[i]).uiObj.transform.Find("Time").GetComponent<Text>().text = FindBuff(enemyBuffs[i]).lastTime.ToString();
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
                FindBuff(characterBuffs[i]).uiObj.transform.Find("Time").GetComponent<Text>().text = FindBuff(characterBuffs[i]).lastTime.ToString();
                if (FindBuff(characterBuffs[i]).lastTime <= 0)
                {
                    RemoveBuff(characterBuffs[i]);
                }
            }
        }
    }

    public void SyncBuffUI(BuffInfo buff)
    {
        switch (buff.valueType)
        {
            case BuffValueType.NoValue:
                buff.uiObj.transform.Find("Value").gameObject.SetActive(false);
                break;
            case BuffValueType.SetValue:
                buff.uiObj.transform.Find("Value").GetComponent<Text>().text = buff.value.ToString();
                break;
            case BuffValueType.AddValue:
                buff.uiObj.transform.Find("Value").GetComponent<Text>().text = buff.value.ToString();
                break;
        }
        switch (buff.timeType)
        {
            case BuffTimeType.Permanent:
                buff.uiObj.transform.Find("Time").GetComponent<Text>().text = "∞";
                break;
            case BuffTimeType.Temporary:
                //if (buff.source == BuffSource.Enemy)
                //{
                //    buff.uiObj.transform.Find("Time").GetComponent<Text>().text = (buff.lastTime - 1).ToString();
                //}
                //else
                //{
                    buff.uiObj.transform.Find("Time").GetComponent<Text>().text = buff.lastTime.ToString();
                //}
                //buff.uiObj.transform.Find("Time").GetComponent<Text>().text = buff.lastTime.ToString();
                break;
        }
    }

    public BuffInfo FindBuff(CharacterBuff characterBuff)
    {
        for (int i = 0; i < activeCBuffs.Count; i++)
        {
            if (characterBuff == activeCBuffs[i].characterBuffType)
            {
                return activeCBuffs[i];
            }
        }
        return null;
    }

    public BuffInfo FindBuff(EnemyBuff enemyBuff)
    {
        for (int i = 0; i < activeEBuffs.Count; i++)
        {
            if (enemyBuff == activeEBuffs[i].enemyBuffType)
            {
                return activeEBuffs[i];
            }
        }
        return null;
    }

    #region Buff Effect Apply

    //public int CharacterAttack(int valueToCalculate)
    //{
    //    //Debug.Log("CA before:" + valueToCalculate);
    //    if (FindBuff(CharacterBuff.Weak) != null)
    //    {
    //        valueToCalculate -= 3;
    //    }
    //    if (FindBuff(CharacterBuff.PowerUp) != null)
    //    {
    //        valueToCalculate += 3;
    //    }
    //    //Debug.Log("CA after:" + valueToCalculate);

    //    return valueToCalculate;
    //}

    //public int CharacterTakeDamage(int valueToCalculate)
    //{
    //    //Debug.Log("CTDEntered");
    //    if (FindBuff(CharacterBuff.Defence) != null)
    //    {
    //        int defenceValueRecord = FindBuff(CharacterBuff.Defence).value;
    //        if (FindBuff(CharacterBuff.Defence).value > valueToCalculate)
    //        {
    //            gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, -valueToCalculate,BuffSource.Character);
    //            valueToCalculate = 0;
    //        }
    //        else
    //        {
    //            gM.buffM.SetBuff(CharacterBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, -valueToCalculate, BuffSource.Character);
    //            valueToCalculate -= defenceValueRecord;
    //        }
    //    }

    //    return valueToCalculate;
    //}

    //public int EnemyTakeDamage(int valueToCalculate)
    //{
    //    //Debug.Log("ETD before:" + valueToCalculate);
    //    if (FindBuff(EnemyBuff.Vulnerable)!=null)
    //    {
    //        valueToCalculate += 3;
    //    }
    //    if (FindBuff(EnemyBuff.Defence) != null)
    //    {
    //        int defenceValueRecord = FindBuff(EnemyBuff.Defence).value;
    //        if (FindBuff(EnemyBuff.Defence).value > valueToCalculate)
    //        {
    //            gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, -valueToCalculate, BuffSource.Enemy);
    //            valueToCalculate = 0;
    //        }
    //        else
    //        {
    //            gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, -valueToCalculate, BuffSource.Enemy);
    //            valueToCalculate -= defenceValueRecord;
    //        }
    //    }
    //    //Debug.Log("ETD after:" + valueToCalculate);
    //    return valueToCalculate;
    //}

    //public int EnemyAttack(int valueToCalculate)
    //{
    //    if (FindBuff(EnemyBuff.Weak)!=null)
    //    {
    //        valueToCalculate -= 3;
    //    }
    //    if (FindBuff(CharacterBuff.Vengeance) != null)
    //    {
    //        gM.enM.enemyTarget.TakeDamage(4);
    //    }
    //    if (valueToCalculate <= 0)
    //    {
    //        valueToCalculate = 0;
    //    }
    //    return valueToCalculate;
    //}
    #endregion
}
