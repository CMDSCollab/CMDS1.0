using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    private GameMaster gM;
    public LineRenderer line;
    public GameObject dotPre;
    public List<Vector3> dotsPos = new List<Vector3>();
    public List<GameObject> dotsList = new List<GameObject>();
    private float gap = 0.1f;
    private Vector3 startPos;
    public List<bool> overRangeStage = new List<bool>() { false, false, false };

    public void InitializeFlow()
    {
        gM = FindObjectOfType<GameMaster>();
        line = GetComponent<LineRenderer>();
        startPos = transform.Find("StartPos").transform.position;
        BoundarySet(1);
        dotsPos.Add(startPos);
        dotsPos.Add(startPos);
        foreach (Vector3 dotPos in dotsPos)
        {
            GameObject dot = Instantiate(dotPre, dotPos, Quaternion.Euler(0, 0, 0), transform.Find("DotCollection"));
            dotsList.Add(dot);
        }
        dotsList[1].transform.Find("CurrentDot").gameObject.SetActive(true);
        dotsList[1].transform.Find("HistoryDot").gameObject.SetActive(false);
        line.positionCount = dotsPos.Count;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, dotsPos[i]);
        }
    }

    public void BoundarySet(int offset )
    {
        LineRenderer xLine = transform.Find("BoundaryX").GetComponent<LineRenderer>();
        LineRenderer yLine = transform.Find("BoundaryY").GetComponent<LineRenderer>();
        xLine.positionCount = 2;
        yLine.positionCount = 2;
        xLine.SetPosition(0, new Vector3(startPos.x + gap * 10, startPos.y, 0));
        yLine.SetPosition(0, new Vector3(startPos.x, startPos.y + gap * 10, 0));

        int xTargetGrid = 30 * offset;
        int yTargetGrid = 35 * offset;
        xLine.SetPosition(1, new Vector3(startPos.x + gap * xTargetGrid, startPos.y + gap * (xTargetGrid - 10), 0));
        yLine.SetPosition(1, new Vector3(startPos.x + gap * (yTargetGrid - 10), startPos.y + gap * yTargetGrid, 0));
    }

    public void ChangeDotPos()
    {
        int dotsIndex = dotsPos.Count - 1;
        float dotPosX = transform.Find("StartPos").transform.position.x + gM.enM.enemyTarget.skillLv * gap;
        float dotPosY = transform.Find("StartPos").transform.position.y + gM.aiM.des.challengeLv * gap;
        dotsPos[dotsIndex] = new Vector3(dotPosX, dotPosY, 0);
    }

    public void AddDot()
    {
        dotsList[dotsList.Count - 1].transform.Find("CurrentDot").gameObject.SetActive(false);
        dotsList[dotsList.Count - 1].transform.Find("HistoryDot").gameObject.SetActive(true);
        dotsPos.Add(new Vector3(dotsPos[dotsPos.Count - 1].x, dotsPos[dotsPos.Count - 1].y, 0));
        GameObject dot = Instantiate(dotPre, dotsPos[dotsPos.Count-1], Quaternion.Euler(0, 0, 0),transform.Find("DotCollection"));
        dotsList.Add(dot);
        line.positionCount = dotsPos.Count;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, dotsPos[i]);
        }
        dot.transform.Find("CurrentDot").gameObject.SetActive(true);
        dot.transform.Find("HistoryDot").gameObject.SetActive(false);
    }

    public void FlowOverRangeCheck()
    {
        if (overRangeStage[0] == false)
        {
            if (gM.aiM.des.challengeLv >= 30 || gM.enM.enemyTarget.skillLv >= 30)
            {
                overRangeStage[0] = true;
                gap = 0.05f;
                AllNodePosReset();
                BoundarySet(2);
            }
        }
        if (overRangeStage[1] == false)
        {
            if (gM.aiM.des.challengeLv >= 60 || gM.enM.enemyTarget.skillLv >= 60)
            {
                overRangeStage[1] = true;
                gap = 0.025f;
                AllNodePosReset();
                BoundarySet(4);
            }
        }

        void AllNodePosReset()
        {
            for (int i = 0; i < dotsList.Count; i++)
            {
                Vector3 dotDividePos = new Vector3((dotsPos[i].x - startPos.x) / 2 + startPos.x, (dotsPos[i].y - startPos.y) / 2 + startPos.y);
                dotsPos[i] = dotDividePos;
                dotsList[i].transform.position = dotsPos[i];
                line.SetPosition(i, dotsPos[i]);
            }
        }
    }
}
