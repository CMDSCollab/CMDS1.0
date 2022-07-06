using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CES_AddEnergy : CEffectBaseState
{
    AIMate targetAI;
    EnergyController energy;
    int energyRecord;
    float timer = 0.1f;
    float timerRecord = 0.1f;

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
        energy = targetAI.transform.Find("Energy").GetComponent<EnergyController>();
        energyRecord = targetAI.energyPoint;
        targetAI.energyPoint += value;
        if (targetAI.energyPoint > targetAI.energySlotAmount)
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
            if (timer <= 0)
            {
                energyRecord--;
                energy.ChangeSprite(energyRecord);
                timer = timerRecord;
            }
        }
        if (energyRecord < targetAI.energyPoint)
        {
            if (timer <= 0)
            {
                energy.ChangeSprite(energyRecord);
                energyRecord++;
          
                timer = timerRecord;
            }
        }
        //targetAI.transform.Find("EnergyBar").GetComponent<Slider>().value = energyRecord;

        //targetAI.transform.Find("EnergyPos").Find("EnergyPoint").GetComponent<Text>().text = energyRecord.ToString();
        targetAI.IntentionValueChangeAndUISync();
        if (energyRecord == targetAI.energyPoint)
        {
            gM.cEffectSM.isUpdate = false;
            EndState(gM, value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        if (gM.cEffectSM.isCardEffectRunning == true)
        {
            gM.cEffectSM.CardEffectsApply(gM.cEffectSM.cardInUse);
        }
    }

}
