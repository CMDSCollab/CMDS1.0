using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEffectBaseState
{
    public abstract void EnterState(GameMaster gM, int value);
    public abstract void UpdateState(GameMaster gM, int value);
    public abstract void EndState(GameMaster gM, int value);
}
