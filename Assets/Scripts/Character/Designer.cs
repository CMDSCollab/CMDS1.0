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
        AdjustFlowPos();
        flow.GetComponent<FlowManager>().InitializeFlow();
    }

    private void AdjustFlowPos()
    {
        RectTransform flowUIPosition = Transform.FindObjectOfType<Canvas>().transform.Find("Flow Chart Position").GetComponent<RectTransform>();

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(flowUIPosition.position);

        flow.transform.position = worldPos;
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
