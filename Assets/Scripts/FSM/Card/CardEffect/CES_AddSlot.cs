using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CES_AddSlot : CEffectBaseState
{
    AIMate targetAI;
    EnergyController energy;

    public override void EnterState(GameMaster gM, int value)
    {
        switch (gM.cEffectSM.aiTarget)
        {
            case CEffectStateManager.AITarget.Des:
                targetAI = gM.aiM.desAI;
                break;
            case CEffectStateManager.AITarget.Pro:
                targetAI = gM.aiM.proAI;
                //slotRecordArray = proSlotRecord;
                AudioManager.Instance.PlayAudio("Programmer_Of_Course");
                break;
            case CEffectStateManager.AITarget.Art:
                targetAI = gM.aiM.artAI;
                //slotRecordArray = artSlotRecord;
                AudioManager.Instance.PlayAudio("Artist_Thank_You");
                break;
        }
        energy = targetAI.transform.Find("Energy").GetComponent<EnergyController>();
        targetAI.energySlotAmount += value;
        if (targetAI.energySlotAmount > 6)
        {
            targetAI.energySlotAmount = 6;
        }
        else
        {
            energy.InstantiateEnergy();
        }
        gM.cEffectSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        if (energy.transform.childCount == targetAI.energySlotAmount)
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
            energy.ChangeSprite(targetAI.energyPoint - 1);
            targetAI.IntentionValueChangeAndUISync();
        }
        if (gM.cEffectSM.isCardEffectRunning == true)
        {
            gM.cEffectSM.CardEffectsApply(gM.cEffectSM.cardInUse);
        }
    }
}
