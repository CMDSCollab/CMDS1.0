using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardBaseState
{
    public abstract void EnterState(GameMaster gM);
    public abstract void UpdateState(GameMaster gM);
    public abstract void EndState(GameMaster gM);
}
