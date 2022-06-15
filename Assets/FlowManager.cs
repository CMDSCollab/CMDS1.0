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
    private float xGap = 0.15f;
    private float yGap = 0.14f;

    void Start()
    {
   
    }

    public void InitializeFlow()
    {
        gM = FindObjectOfType<GameMaster>();
        line = GetComponent<LineRenderer>();
        dotsPos.Add(transform.Find("StartPos").transform.position);
        dotsPos.Add(new Vector3(transform.Find("StartPos").transform.position.x + xGap, transform.Find("StartPos").transform.position.y));
        dotsPos.Add(new Vector3(transform.Find("StartPos").transform.position.x + xGap * 2, transform.Find("StartPos").transform.position.y));
        foreach (Vector3 dotPos in dotsPos)
        {
            GameObject dot = Instantiate(dotPre, dotPos, Quaternion.Euler(0, 0, 0), transform.Find("DotCollection"));
            dotsList.Add(dot);
        }
        line.positionCount = dotsPos.Count;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, dotsPos[i]);
        }
    }

    public void ChangeDotPos(int difference)
    {
        int dotsIndex = dotsPos.Count - 1;
        float yAxisOffset = dotsPos[0].y;
        dotsPos[dotsIndex] = new Vector3(transform.Find("StartPos").transform.position.x + dotsIndex * xGap, difference * yGap + yAxisOffset, 0);
    }

    public void AddDot()
    {
        dotsPos.Add(new Vector3(dotsPos[dotsPos.Count - 1].x + xGap, dotsPos[dotsPos.Count - 1].y, 0));
        GameObject dot = Instantiate(dotPre, dotsPos[dotsPos.Count-1], Quaternion.Euler(0, 0, 0),transform.Find("DotCollection"));
        dotsList.Add(dot);
        line.positionCount = dotsPos.Count;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, dotsPos[i]);
        }
    }
}
