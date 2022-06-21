using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Designer : CharacterMate
{
    public int challengeLv = 0;
    public List<bool> statusPerTurn;
    public Text challengeIntText;
    public GameObject flow;

    public override void Start()
    {
        base.Start();
    }

    public void PrepareFlowChart()
    {
        flow = Instantiate(gM.characterM.flowPre);
        flow.GetComponent<FlowManager>().InitializeFlow();
    }

    public void ChallengeDMG()
    {
        int difference = challengeLv - gM.enM.enemyTarget.skillLv;
        if (challengeLv > gM.enM.enemyTarget.skillLv && difference < 10)
        {
            gM.enM.enemyTarget.TakeDamage(difference);
        }
    }
}
