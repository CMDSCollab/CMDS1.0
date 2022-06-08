using UnityEngine;

public abstract class MerchantBaseState
{
    public abstract void EnterState(GameMaster gM);
    public abstract void UpdateState(GameMaster gM);
    public abstract void EndState(GameMaster gM);
}
