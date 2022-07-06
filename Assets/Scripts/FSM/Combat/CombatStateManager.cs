using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateManager : MonoBehaviour
{
    private GameMaster gM;
    public CombatBaseState currentState;
    public CS_Start startState = new CS_Start();
    public CS_AI1 ai1State = new CS_AI1();
    public CS_AI2 ai2State = new CS_AI2();
    public CS_Enemy enemyState = new CS_Enemy();
    public CS_End endState = new CS_End();

    public List<CombatBaseState> defaultSequence = new List<CombatBaseState>();
    public List<CombatBaseState> runningSequence = new List<CombatBaseState>();
    public int stateIndex = 0;

    public bool isUpdate = false;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        currentState = startState;

        defaultSequence.Add(startState);
        defaultSequence.Add(ai1State);
        defaultSequence.Add(ai2State);
        defaultSequence.Add(enemyState);
        defaultSequence.Add(endState);
        runningSequence = defaultSequence;
    }

    void FixedUpdate()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM);
        }
    }

    public void SwitchCombatState()
    {
        if (stateIndex >= runningSequence.Count - 1)
        {
            //Debug.Log("entered");
            stateIndex = 0;
        }
        stateIndex++;
        //Debug.Log(stateIndex);
        currentState = runningSequence[stateIndex];
        currentState.EnterState(gM);
    }

    public void CombatStateProcess()
    {
        stateIndex = 0;
        currentState = startState;
        currentState.EnterState(gM);
    }
}
