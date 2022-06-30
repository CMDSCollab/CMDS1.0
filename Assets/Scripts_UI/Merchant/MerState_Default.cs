using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerState_Default : MerchantBaseState
{
    public override void EnterState(GameMaster gM)
    {
        gM.merchantSM.isUpdate = false;
    }

    public override void UpdateState(GameMaster gM)
    {

    }

    public override void EndState(GameMaster gM)
    {

    }



}
