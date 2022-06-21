using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CES_SkillC : CEffectBaseState
{

    public override void EnterState(GameMaster gM, int value)
    {
        gM.enM.enemyTarget.skillLv += value;
        if (gM.enM.enemyTarget.skillLv<=0)
        {
            gM.enM.enemyTarget.skillLv = 0;
        }
        gM.aiM.des.flow.GetComponent<FlowManager>().ChangeDotPos();
        gM.enM.enemyTarget.transform.Find("Coordinate").GetComponent<Text>().text = "(" + gM.aiM.des.challengeLv + " , " + gM.enM.enemyTarget.skillLv + ")";
        gM.cEffectSM.isUpdate = true;
    }

    public override void UpdateState(GameMaster gM, int value)
    {
        FlowManager flowM = gM.aiM.des.flow.GetComponent<FlowManager>();
        Transform point = flowM.dotsList[flowM.dotsList.Count - 1].transform;
        point.position = Vector3.MoveTowards(point.position, flowM.dotsPos[flowM.dotsPos.Count - 1], 0.3f * Time.deltaTime);
        flowM.line.SetPosition(flowM.dotsList.Count - 1, point.position);
        gM.enM.enemyTarget.transform.Find("Coordinate").GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2(point.position.x, point.position.y+0.2f));

        if (point.position == flowM.dotsPos[flowM.dotsPos.Count - 1])
        {
            gM.cEffectSM.isUpdate = false;
            EndState(gM, value);
        }
    }

    public override void EndState(GameMaster gM, int value)
    {
        //Debug.Log("entered");
        int chaSubtractSkill = gM.aiM.des.challengeLv - gM.enM.enemyTarget.skillLv;
        int skillSubtractCha = gM.enM.enemyTarget.skillLv - gM.aiM.des.challengeLv;
        //Debug.Log(skillSubtractCha);
        //Debug.Log(chaSubtractSkill);
        if (skillSubtractCha > 10)
        {
            //Debug.Log("entered1");
            gM.buffM.RemoveBuff(EnemyBuff.Anxiety);
            gM.buffM.RemoveBuff(EnemyBuff.InFlow);
            gM.buffSM.AddOrAdjustBuff(EnemyBuff.Bored);
        }
        if (chaSubtractSkill <= 10 && skillSubtractCha <= 10)
        {
            //Debug.Log("entered2");
            gM.buffM.RemoveBuff(EnemyBuff.Anxiety);
            gM.buffM.RemoveBuff(EnemyBuff.Bored);
            gM.buffSM.AddOrAdjustBuff(EnemyBuff.InFlow);
        }
        if (chaSubtractSkill > 10)
        {
            //Debug.Log("entered3");
            gM.buffM.RemoveBuff(EnemyBuff.Bored);
            gM.buffM.RemoveBuff(EnemyBuff.InFlow);
            gM.buffSM.AddOrAdjustBuff(EnemyBuff.Anxiety);
        }
    }
}
