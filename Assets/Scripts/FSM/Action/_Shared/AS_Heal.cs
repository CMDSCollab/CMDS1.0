using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_Heal : ActionBaseState
{
    private bool isHpHealEnd = false;

    public override void EnterState(GameMaster gM, int value)
    {
        gM.actionSM.changedValue = value;
        gM.characterM.mainCharacter.HealSelf(gM.actionSM.changedValue);
        //gM.characterM.mainCharacter.healthPoint += gM.actionSM.changedValue;
        //if (gM.characterM.mainCharacter.healthPoint >= gM.characterM.mainCharacter.maxHp)
        //{
        //    gM.characterM.mainCharacter.healthPoint = gM.characterM.mainCharacter.maxHp;
        //}
        gM.actionSM.isUpdate = true;
    }

    public override void BeforeUpdate(GameMaster gM, int value)
    {
        //gM.actionSM.changedValue = value;

    }

    public override void UpdateState(GameMaster gM, int value)
    {
        if (isHpHealEnd == false)
        {
            if (gM.animC.isAnimEntered == false)
            {
                gM.animC.SetScaleBounceValue(CurrentAITarget(gM).Find("IntentionPos").Find("Image").GetComponent<RectTransform>(), 1f, 0.6f, 1f);
                gM.animC.currentChoice = AnimChoice.ScaleBounce;
            }
            if (gM.animC.isAnimEnd == true)
            {
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
                isHpHealEnd = true;
            }
        }
        else
        {
            if (gM.animC.isAnimEntered == false)
            {
                gM.animC.SetHpSliderMoveValue(gM.characterM.mainCharacter.hpBar, gM.characterM.mainCharacter.healthPoint);
                gM.animC.currentChoice = AnimChoice.HpSliderMove;
            }
            if (gM.animC.isAnimEnd == true)
            {
                gM.actionSM.isUpdate = false;
                gM.animC.isAnimEnd = false;
                gM.animC.isAnimEntered = false;
                isHpHealEnd = false;
                gM.characterM.mainCharacter.hpRatio.text = gM.characterM.mainCharacter.healthPoint.ToString() + "/" + gM.characterM.mainCharacter.maxHp.ToString();
                EndState(gM, value);
            }
        }

    }

    public override void AfterUpdate(GameMaster gM, int value)
    {
        throw new System.NotImplementedException();
    }

    public override void EndState(GameMaster gM, int value)
    {
        gM.actionSM.EnterActionState(gM.actionSM.changeIState, value);
    }
}
