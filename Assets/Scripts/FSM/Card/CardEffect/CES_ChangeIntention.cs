using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CES_ChangeIntention : CEffectBaseState
{
    AIMate targetAI;
    private bool firstState = true;

    public override void EnterState(GameMaster gM, int value)
    {
        switch (gM.cEffectSM.aiTarget)
        {
            case CEffectStateManager.AITarget.Des:
                targetAI = gM.aiM.desAI;
                break;
            case CEffectStateManager.AITarget.Pro:
                targetAI = gM.aiM.proAI;
                AudioManager.Instance.PlayAudio("Programmer_Consider_It_Done");
                break;
            case CEffectStateManager.AITarget.Art:
                targetAI = gM.aiM.artAI;
                AudioManager.Instance.PlayAudio("Artist_Copy_That");
                break;
        }
        gM.cEffectSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        RectTransform intentionRect = targetAI.transform.Find("IntentionPos").GetComponent<RectTransform>();
        if (firstState == true)
        {
            intentionRect.Rotate(0, 5f, 0);
            if (intentionRect.rotation.y >= 0.5)
            {
                firstState = false;
                targetAI.ChangeIntention();
            }
        }
        else
        {
            intentionRect.Rotate(0, -5f, 0);
            if (intentionRect.rotation.y <= 0.05)
            {
                firstState = true;
                gM.cEffectSM.isUpdate = false;
                EndState(gM, value);
            }
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
