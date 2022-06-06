using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStateManager : MonoBehaviour
{
    private GameMaster gM;
    public ActionBaseState currentState;
    public AS_Attack attackState = new AS_Attack();
    public AS_TakeDmg takeDmgState = new AS_TakeDmg();
    public AS_Heal healState = new AS_Heal();
    public AS_Defence defenceState = new AS_Defence();
    public AS_Taunt tauntState = new AS_Taunt();
    public AS_ChangeIntention changeIState = new AS_ChangeIntention();
    public AS_Block blockState = new AS_Block();
    public AS_MultiAtk multiAtkState = new AS_MultiAtk();
    public AS_Charge chargeState = new AS_Charge();

    public int changedValue;
    public bool isUpdate;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        currentState = attackState;
    }

    void Update()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM,changedValue);
        }
    }

    public void EnterActionState(ActionBaseState state, int value)
    {
        currentState = state;
        currentState.EnterState(gM, value);
    }
}
