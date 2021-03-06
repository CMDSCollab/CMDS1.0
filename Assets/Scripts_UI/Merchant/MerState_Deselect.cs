using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerState_Deselect : MerchantBaseState
{
    public override void EnterState(GameMaster gM)
    {
        gM.merchantSM.targetPos = gM.merchantSM.originPos;
        gM.merchantSM.isUpdate = true;

    }

    public override void UpdateState(GameMaster gM)
    {
        gM.merchantSM.currentItemRectTrans.anchoredPosition = Vector3.MoveTowards(gM.merchantSM.currentItemRectTrans.anchoredPosition, gM.merchantSM.targetPos, gM.merchantSM.moveSpeed * Time.deltaTime);

        if (gM.merchantSM.currentItemRectTrans != null)
        {
            if (gM.merchantSM.currentItemRectTrans.localPosition.y == gM.merchantSM.originPos.y)
            {
                gM.merchantSM.isUpdate = false;
                EndState(gM);
            }
        }
    }

    public override void EndState(GameMaster gM)
    {
        gM.merchantSM.EnterMerchantState(gM.merchantSM.defaultState);
    }
}
