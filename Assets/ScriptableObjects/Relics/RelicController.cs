using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RelicInWhere
{
    inPanel,
    inMerchant,
    inChest
}

public class RelicController : MonoBehaviour
{
    GameMaster gM;
    public  RelicInfo relicInfo;
    public RelicInWhere inWhere;
    private RectTransform rectTrans;
    public int realPrice;
    private bool isSold;
    private Vector3 targetPos;

    public void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        rectTrans = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isSold)
        {
            RelicSold();
        }
    }

    public void InitializeRelic()
    {
        transform.Find("Image").GetComponent<Image>().sprite = relicInfo.relicImage;
    }

    public void OnMouseClick()
    {
        switch (inWhere)
        {
            case RelicInWhere.inPanel:
                break;
            case RelicInWhere.inMerchant:
                if (realPrice <= gM.comStatusBar.gold)
                {
                    isSold = true;
                    gM.relicM.activeRelics.Add(relicInfo);
                    gM.comStatusBar.GoldChange(-realPrice);
                    targetPos = gM.comStatusBar.RelicPosSet();
                    //AudioManager.Instance.PlayAudio("Coins_Purchase");
                }
                break;
            case RelicInWhere.inChest:
                isSold = true;
                gM.relicM.activeRelics.Add(relicInfo);
                targetPos = gM.comStatusBar.RelicPosSet();
                break;
        }
    }


    private bool isScaleChangeOver = false;
    private float localScale = 1;
    void RelicSold()
    {
        transform.SetParent(gM.comStatusBar.transform.Find("RelicArea"));
        rectTrans.localPosition = Vector3.MoveTowards(rectTrans.localPosition, targetPos, 1000*Time.deltaTime);

        if (isScaleChangeOver == false)
        {
            localScale -= Time.deltaTime;
            rectTrans.localScale = new Vector3(localScale, localScale, 0);
            if (localScale <= 0.6f)
            {
                rectTrans.localScale = new Vector3(0.6f, 0.6f, 0);
                isScaleChangeOver = true;
            }
        }


        if (rectTrans.localPosition == targetPos && isScaleChangeOver == true)
        {
            isSold = false;
        }
    }

    #region Show Tip
    private float timeToWait = 0.5f;

    public void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnMouseExit()
    {
        StopAllCoroutines();
        FindObjectOfType<GameMaster>().buffM.hoverTip.SetActive(false);
    }

    private void ShowMessage()
    {
        FindObjectOfType<GameMaster>().buffM.hoverTip.SetActive(true);
        FindObjectOfType<HoverTipManager>().ShowTip(relicInfo.relicName, relicInfo.description, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
    #endregion
}
