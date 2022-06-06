using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardS_Default : CardBaseState
{
    public override void EnterState(GameMaster gM)
    {
        gM.cardSM.isUpdate = false;
    }

    public override void EndState(GameMaster gM)
    {

    }



    public override void UpdateState(GameMaster gM)
    {

    }
}
