using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CES_AddEnergy : CEffectBaseState
{
    AIMate targetAI;
    int energyRecord;
    float timer = 0.2f;
    float timerRecord = 0.2f;

    public override void EnterState(GameMaster gM, int value)
    {
        switch (gM.cEffectSM.aiTarget)
        {
            case CEffectStateManager.AITarget.Des:
                targetAI = gM.aiM.desAI;
                break;
            case CEffectStateManager.AITarget.Pro:
                targetAI = gM.aiM.proAI;
                break;
            case CEffectStateManager.AITarget.Art:
                targetAI = gM.aiM.artAI;
                break;
        }
        energyRecord = targetAI.energyPoint;
        targetAI.energyPoint += value;
        if (targetAI.energyPoint>targetAI.energySlotAmount)
        {
            targetAI.energyPoint = targetAI.energySlotAmount;
        }
        gM.cEffectSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        timer -= Time.deltaTime;
        if (energyRecord > targetAI.energyPoint)
        {
            if (timer<=0)
            {
                energyRecord--;
                timer = timerRecord;
            }
        }
        if (energyRecord < targetAI.energyPoint)
        {
            if (timer<=0)
            {
                energyRecord++;
                timer = timerRecord;
            }
        }
        targetAI.transform.Find("EnergyPos").Find("EnergyPoint").GetComponent<Text>().text = energyRecord.ToString();
        targetAI.IntentionValueChangeAndUISync();
        if (energyRecord == targetAI.energyPoint)
        {
            gM.cEffectSM.isUpdate = false;
            EndState(gM, value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        //if (gM.combatSM.currentState == gM.combatSM.ai1State)
        //{
        //    Debug.Log("1entered");
        //    gM.combatSM.SwitchCombatState(gM.combatSM.ai2State);
        //}
        //if (gM.combatSM.currentState == gM.combatSM.ai2State)
        //{
        //    Debug.Log("2entered");
        //    gM.combatSM.SwitchCombatState(gM.combatSM.enemyState);
        //}
        if (gM.cEffectSM.isCardEffectRunning == true)
        {
            //    Debug.Log("3entered");
            gM.cEffectSM.CardEffectsApply(gM.cEffectSM.cardInUse);
        }
    }

}
