using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CES_AddSlot : CEffectBaseState
{
    AIMate targetAI;
    int slotRecord;
    float timer = 0.5f;
    float timerRecord = 0.5f;

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
        slotRecord = targetAI.energySlotAmount;
        targetAI.energySlotAmount += value;
        gM.cEffectSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        timer -= Time.deltaTime;
        if (slotRecord > targetAI.energySlotAmount)
        {
            if (timer <= 0)
            {
                slotRecord--;
                timer = timerRecord;
            }
        }
        if (slotRecord < targetAI.energySlotAmount)
        {
            if (timer <= 0)
            {
                slotRecord++;
                timer = timerRecord;
            }
        }
        targetAI.transform.Find("EnergyPos").Find("SlotAmount").GetComponent<Text>().text = slotRecord.ToString();
        if (slotRecord == targetAI.energySlotAmount)
        {
            gM.cEffectSM.isUpdate = false;
            EndState(gM, value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        if (gM.buffM.FindBuff(CharacterBuff.IsSycn) != null)
        {
            targetAI.energyPoint += 1;
            if (targetAI.energyPoint > targetAI.energySlotAmount)
            {
                targetAI.energyPoint = targetAI.energySlotAmount;
            }
            targetAI.transform.Find("EnergyPos").Find("EnergyPoint").GetComponent<Text>().text = targetAI.energyPoint.ToString();
            targetAI.IntentionValueChangeAndUISync();
        }
        if (gM.cEffectSM.isCardEffectRunning == true)
        {
            gM.cEffectSM.CardEffectsApply(gM.cEffectSM.cardInUse);
        }
    }
}
