using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CES_SkillC : CEffectBaseState
{

    public override void EnterState(GameMaster gM, int value)
    {
        gM.enM.enemyTarget.skillLv += value;
        //if (gM.enM.enemyTarget.skillLv < 0)
        //{
        //    gM.enM.enemyTarget.skillLv = 0;
        //}
        //Object.FindObjectOfType<FlowPoint>().TargetPosSet();

        int chaSubtractSkill = gM.aiM.des.challengeLv - gM.enM.enemyTarget.skillLv;
        gM.aiM.des.flow.GetComponent<FlowManager>().ChangeDotPos(chaSubtractSkill);
        gM.cEffectSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {

        //RectTransform pointRect = gM.aiM.des.flowChart.transform.Find("Point").GetComponent<RectTransform>();
        //pointRect.anchoredPosition = Vector3.MoveTowards(pointRect.anchoredPosition, pointRect.GetComponent<FlowPoint>().targetPos, 50 * Time.deltaTime);
        //if (pointRect.anchoredPosition == pointRect.GetComponent<FlowPoint>().targetPos)
        //{
        //    gM.cEffectSM.isUpdate = false;
        //    EndState(gM, value);
        //}

        FlowManager flowM = gM.aiM.des.flow.GetComponent<FlowManager>();
        Transform point = flowM.dotsList[flowM.dotsList.Count - 1].transform;
        point.position = Vector3.MoveTowards(point.position, flowM.dotsPos[flowM.dotsPos.Count - 1], 0.3f * Time.deltaTime);
        flowM.line.SetPosition(flowM.dotsList.Count - 1, point.position);
        if (point.position == flowM.dotsPos[flowM.dotsPos.Count - 1])
        {
            gM.cEffectSM.isUpdate = false;
            EndState(gM, value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        //gM.cEffectSM.EnterCardState(gM.cEffectSM.magicCircleState, value);
        int chaLv = gM.aiM.des.challengeLv;
        int chaSubtractSkill = chaLv - gM.enM.enemyTarget.skillLv;
        int skillSubtractCha = gM.enM.enemyTarget.skillLv - chaLv;
        //Debug.Log(skillSubtractCha);
        if (skillSubtractCha > 10)
        {
            Debug.Log("entered");
            gM.buffM.RemoveBuff(EnemyBuff.Anxiety);
            gM.buffM.RemoveBuff(EnemyBuff.InFlow);
            gM.buffSM.AddOrAdjustBuff(EnemyBuff.Bored);
        }
        if (chaSubtractSkill <= 10 && skillSubtractCha <= 10)
        {
            gM.buffM.RemoveBuff(EnemyBuff.Anxiety);
            gM.buffM.RemoveBuff(EnemyBuff.Bored);
            gM.buffSM.AddOrAdjustBuff(EnemyBuff.InFlow);
        }
        if (chaSubtractSkill > 10)
        {
            gM.buffM.RemoveBuff(EnemyBuff.Bored);
            gM.buffM.RemoveBuff(EnemyBuff.InFlow);
            gM.buffSM.AddOrAdjustBuff(EnemyBuff.Anxiety);
        }
    }
}
