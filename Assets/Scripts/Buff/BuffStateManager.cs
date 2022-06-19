using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuffUsage
{
    AddNew,
    Adjust,
    EffectApply
}

public class BuffStateManager : MonoBehaviour
{
    public GameMaster gM;
    public BuffBaseState currentState;
    public RectTransform buffTrans;
    public List<EnemyBuff> enemyBuffs = new List<EnemyBuff>();
    public List<CharacterBuff> characterBuffs = new List<CharacterBuff>();
    public bool isUpdate = false;
    public int valueToCalculate = 0;
    public bool isSequenceEnd = false;
    public BuffUsage buffUsage = BuffUsage.EffectApply;

    public BS_Weak weakState = new BS_Weak();
    public BS_Vulnerable vulnerableState = new BS_Vulnerable();
    public BS_Bored boredState = new BS_Bored();
    public BS_Anxiety anxietyState = new BS_Anxiety();
    public BS_InFlow inFlowState = new BS_InFlow();
    public BS_Defence defenceState = new BS_Defence();
    public BS_Block blockState = new BS_Block();
    public BS_Charge chargeState = new BS_Charge();
    public BS_Sync syncState = new BS_Sync();
    public BS_TeamWork teamworkState = new BS_TeamWork();
    public BS_Inflammable inflammableState = new BS_Inflammable();
    public BS_Revive reviveState = new BS_Revive();

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    void Update()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM);
        }
    }

    public void EnterBuffState(BuffBaseState buffState)
    {
        currentState = buffState;
        buffState.EnterState(gM);
    }

    public void BuffEffectsApply()
    {
        buffUsage = BuffUsage.EffectApply;
        if (enemyBuffs.Count != 0)
        {
            if (gM.buffM.FindBuff(enemyBuffs[0]) != null)
            {
                buffTrans = GetBuffRectTrans(enemyBuffs[0]);
                currentState = GetBuffState(enemyBuffs[0]);
                //SetBuffStateFindTrans(enemyBuffs[0]);
                currentState.EnterState(gM);
                enemyBuffs.RemoveAt(0);
            }
            else
            {
                enemyBuffs.RemoveAt(0);
                BuffEffectsApply();
            }
        }
        else if (characterBuffs.Count != 0)
        {
            if (gM.buffM.FindBuff(characterBuffs[0]) != null)
            {
                buffTrans = GetBuffRectTrans(characterBuffs[0]);
                currentState = GetBuffState(characterBuffs[0]);
                //SetBuffStateFindTrans(characterBuffs[0]);
                currentState.EnterState(gM);
                characterBuffs.RemoveAt(0);
            }
            else
            {
                characterBuffs.RemoveAt(0);
                BuffEffectsApply();
            }
        }
        else
        {
            characterBuffs.Clear();
            enemyBuffs.Clear();
            gM.actionSM.currentState.BeforeUpdate(gM, valueToCalculate);
        }
    }

    public void AddOrAdjustBuff(EnemyBuff enemyBuff)
    {
        if (gM.buffM.FindBuff(enemyBuff) != null)
        {
            buffUsage = BuffUsage.Adjust;
            buffTrans = GetBuffRectTrans(enemyBuff);
            currentState = GetBuffState(enemyBuff);
        }
        else
        {
            buffUsage = BuffUsage.AddNew;
            currentState = GetBuffState(enemyBuff);
        }
        currentState.EnterState(gM);
    }

    public void AddOrAdjustBuff(CharacterBuff characterBuff)
    {
        if (gM.buffM.FindBuff(characterBuff) != null)
        {
            buffUsage = BuffUsage.Adjust;
            buffTrans = GetBuffRectTrans(characterBuff);
            currentState = GetBuffState(characterBuff);
        }
        else
        {
            buffUsage = BuffUsage.AddNew;
            currentState = GetBuffState(characterBuff);
        }
        currentState.EnterState(gM);
    }

    //public void AddNewBuff(CharacterBuff buffType, BuffTimeType timeType, int lastTime, BuffValueType valueType, int value)
    //{
    //    BuffInfo1 buffInfo = new BuffInfo1();
    //    buffInfo.characterBuffType = buffType;
    //    buffInfo.timeType = timeType;
    //    buffInfo.lastTime = lastTime;
    //    buffInfo.valueType = valueType;
    //    buffInfo.value = value;
    //    buffInfo.uiObj = Instantiate(gM.buffM.buffPrefab, gM.characterM.mainCharacter.transform.Find("BuffArea"), false);
    //    //buffInfo.isReadyToRemove = gM.buffM.CheckBuffIsReadyForRemove(buffInfo);
    //    foreach (CharacterBuffImage buff in gM.buffM.characterBuffs)
    //    {
    //        if (buff.buffType == buffType)
    //        {
    //            buffInfo.uiObj.GetComponent<Image>().sprite = buff.buffImage;
    //        }
    //    }
    //    //gM.buffM.SyncBuffUI(buffInfo);
    //    gM.buffM.activeCBuffs.Add(buffInfo);
    //    //gM.buffM.CheckBuffAndRemove(buffInfo.characterBuffType);
    //}

    //public void AddNewBuff(EnemyBuff buffType, BuffTimeType timeType, int lastTime, BuffValueType valueType, int value)
    //{
    //    BuffInfo1 buffInfo = new BuffInfo1();
    //    buffInfo.enemyBuffType = buffType;
    //    buffInfo.timeType = timeType;
    //    buffInfo.lastTime = lastTime;
    //    buffInfo.valueType = valueType;
    //    buffInfo.value = value;
    //    buffInfo.uiObj = Instantiate(gM.buffM.buffPrefab, gM.enM.enemyTarget.transform.Find("BuffArea"), false);
    //    //buffInfo.isReadyToRemove = gM.buffM.CheckBuffIsReadyForRemove(buffInfo);
    //    foreach (EnemyBuffImage buff in gM.buffM.enemyBuffs)
    //    {
    //        if (buff.buffType == buffType)
    //        {
    //            buffInfo.uiObj.GetComponent<Image>().sprite = buff.buffImage;
    //        }
    //    }
    //    //gM.buffM.SyncBuffUI(buffInfo);
    //    gM.buffM.activeEBuffs.Add(buffInfo);
    //    //gM.buffM.CheckBuffAndRemove(buffInfo.characterBuffType);
    //}

    #region Set Array To List && Get BuffState && Get RectTransform
    public RectTransform GetBuffRectTrans(CharacterBuff characterBuff)
    {
        switch (characterBuff)
        {
            case CharacterBuff.Defence:
                return gM.buffM.FindBuff(CharacterBuff.Defence).GetComponent<RectTransform>();
            case CharacterBuff.Vengeance:
                return gM.buffM.FindBuff(CharacterBuff.Vengeance).GetComponent<RectTransform>();
            case CharacterBuff.PowerUp:
                return gM.buffM.FindBuff(CharacterBuff.PowerUp).GetComponent<RectTransform>();
            case CharacterBuff.Weak:
                return gM.buffM.FindBuff(CharacterBuff.Weak).GetComponent<RectTransform>();
            case CharacterBuff.Inflammable:
                return gM.buffM.FindBuff(CharacterBuff.Inflammable).GetComponent<RectTransform>();
            case CharacterBuff.IsTeamWork:
                return gM.buffM.FindBuff(CharacterBuff.IsTeamWork).GetComponent<RectTransform>();
            case CharacterBuff.IsSycn:
                return gM.buffM.FindBuff(CharacterBuff.IsSycn).GetComponent<RectTransform>();
            default:
                return gM.buffM.FindBuff(CharacterBuff.Defence).GetComponent<RectTransform>();
        }
    }

    public RectTransform GetBuffRectTrans(EnemyBuff enemyBuff)
    {
        switch (enemyBuff)
        {
            case EnemyBuff.Bored:
                return gM.buffM.FindBuff(EnemyBuff.Bored).GetComponent<RectTransform>();
            case EnemyBuff.Anxiety:
                return gM.buffM.FindBuff(EnemyBuff.Anxiety).GetComponent<RectTransform>();
            case EnemyBuff.InFlow:
                return gM.buffM.FindBuff(EnemyBuff.InFlow).GetComponent<RectTransform>();
            case EnemyBuff.Vulnerable:
                return gM.buffM.FindBuff(EnemyBuff.Vulnerable).GetComponent<RectTransform>();
            case EnemyBuff.Weak:
                return gM.buffM.FindBuff(EnemyBuff.Weak).GetComponent<RectTransform>();
            case EnemyBuff.Instability:
                return gM.buffM.FindBuff(EnemyBuff.Instability).GetComponent<RectTransform>();
            case EnemyBuff.WeakMind:
                return gM.buffM.FindBuff(EnemyBuff.WeakMind).GetComponent<RectTransform>();
            case EnemyBuff.Defence:
                return gM.buffM.FindBuff(EnemyBuff.Defence).GetComponent<RectTransform>();
            case EnemyBuff.Block:
                return gM.buffM.FindBuff(EnemyBuff.Block).GetComponent<RectTransform>();
            case EnemyBuff.Charge:
                return gM.buffM.FindBuff(EnemyBuff.Charge).GetComponent<RectTransform>();
            case EnemyBuff.PartialInvincibility:
                return gM.buffM.FindBuff(EnemyBuff.PartialInvincibility).GetComponent<RectTransform>();
            case EnemyBuff.Revive:
                return gM.buffM.FindBuff(EnemyBuff.Revive).GetComponent<RectTransform>();
            default:
                return gM.buffM.FindBuff(EnemyBuff.Bored).GetComponent<RectTransform>();
        }
    }

    public BuffBaseState GetBuffState(CharacterBuff characterBuff)
    {
        switch (characterBuff)
        {
            case CharacterBuff.Defence:
                return defenceState;
            //case CharacterBuff.Vengeance:
            //    return boredState;
            //case CharacterBuff.PowerUp:
            //    return boredState;
            case CharacterBuff.Inflammable:
                return inflammableState;
            case CharacterBuff.Weak:
                return weakState;
            //case CharacterBuff.Inflammable:
            //    return boredState;
            case CharacterBuff.IsTeamWork:
                return teamworkState;
            case CharacterBuff.IsSycn:
                return syncState;
            default:
                return boredState;
        }
    }

    public BuffBaseState GetBuffState(EnemyBuff enemyBuff)
    {
        switch (enemyBuff)
        {
            case EnemyBuff.Bored:
                return boredState;
            case EnemyBuff.Anxiety:
                return anxietyState;
            case EnemyBuff.InFlow:
                return inFlowState;
            case EnemyBuff.Vulnerable:
                return vulnerableState;
            case EnemyBuff.Weak:
                return weakState;
            //case EnemyBuff.Instability:
            //    break;
            //case EnemyBuff.WeakMind:
            //    break;
            case EnemyBuff.Defence:
                return defenceState;
            case EnemyBuff.Block:
                return blockState;
            case EnemyBuff.Charge:
                return chargeState;
            //case EnemyBuff.PartialInvincibility:
            //    break;
            case EnemyBuff.Revive:
                return reviveState;
            default:
                return boredState;
        }
    }

    public void SetBuffList(EnemyBuff[] enemyBuffArray)
    {
        for (int i = 0; i < enemyBuffArray.Length; i++)
        {
            enemyBuffs.Add(enemyBuffArray[i]);
        }
    }

    public void SetBuffList(CharacterBuff[] characterBuffArray)
    {
        for (int i = 0; i < characterBuffArray.Length; i++)
        {
            characterBuffs.Add(characterBuffArray[i]);
        }
    }
#endregion
}
