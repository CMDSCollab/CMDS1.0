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

    void Start()
    {
   
    }

    public void InitializeFlow()
    {
        gM = FindObjectOfType<GameMaster>();
        line = GetComponent<LineRenderer>();
        startPos = transform.Find("StartPos").transform.position;
        BoundarySet();
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

    public void BoundarySet()
    {
        LineRenderer xLine = transform.Find("BoundaryX").GetComponent<LineRenderer>();
        LineRenderer yLine = transform.Find("BoundaryY").GetComponent<LineRenderer>();
        xLine.positionCount = 2;
        yLine.positionCount = 2;
        xLine.SetPosition(0, new Vector3(startPos.x + gap * 10, startPos.y, 0));
        xLine.SetPosition(1, new Vector3(startPos.x + gap * 30, startPos.y + gap * 20, 0));
        yLine.SetPosition(0, new Vector3(startPos.x, startPos.y + gap * 10, 0));
        yLine.SetPosition(1, new Vector3(startPos.x + gap * 25, startPos.y + gap * 35, 0));
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
}
