using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;
    private Vector3 targetPos = new Vector3(0, 0, 0);
    private Vector3 spawnPos = new Vector3(0, 850, 0);
    private float speed = 1500;
    private bool isBegin;
    [HideInInspector]
    public bool isEnd;
    private float transValue = 0;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public virtual void Start()
    {
        transform.localPosition = spawnPos;
        isBegin = true;
    }
    public virtual void OnClickLeave()
    {
        isEnd = true;
    }

    private void Update()
    {
        if (isBegin)
        {
            if (transValue <= 0.8)
            {
                transValue += 2*Time.deltaTime;
                gM.uiCanvas.transform.Find("TransCover").gameObject.SetActive(true);
                gM.uiCanvas.transform.Find("TransCover").GetComponent<Image>().color = new Color(1, 1, 1, transValue);
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);
            if (transform.localPosition == targetPos)
            {
                isBegin = false;
            }
        }
        if (isEnd)
        {
            transValue -= 2*Time.deltaTime;
            gM.uiCanvas.transform.Find("TransCover").GetComponent<Image>().color = new Color(1, 1, 1, transValue);
            if (transValue <= 0.05)
            {
                gM.uiCanvas.transform.Find("TransCover").gameObject.SetActive(false);
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, spawnPos, speed * Time.deltaTime);
            if (transform.localPosition == spawnPos)
            {
                isEnd = false;
                gM.panelM.isPanelOpen = false;
                Destroy(gameObject);
            }
        }
    }
}
