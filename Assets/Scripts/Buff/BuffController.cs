using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffController : MonoBehaviour
{
    public BuffInfo buffInfo;
    public int lastTime;
    public int currentValue;

    public void BuffCurrentValueChange(int changeAmount)
    {
        switch (buffInfo.valueType)
        {
            case BuffValueType.SetValue:
                currentValue = changeAmount;
                break;
            case BuffValueType.AddValue:
                currentValue += changeAmount;
                break;
        }
        BuffUISync();
    }

    public void BuffUISync()
    {
        transform.Find("Image").GetComponent<Image>().sprite = buffInfo.image;
        switch (buffInfo.valueType)
        {
            case BuffValueType.NoValue:
                transform.Find("Value").gameObject.SetActive(false);
                break;
            case BuffValueType.SetValue:
                transform.Find("Value").GetComponent<Text>().text = currentValue.ToString();
                break;
            case BuffValueType.AddValue:
                transform.Find("Value").GetComponent<Text>().text = currentValue.ToString();
                break;
        }
        switch (buffInfo.timeType)
        {
            case BuffTimeType.Permanent:
                transform.Find("Time").GetComponent<Text>().text = "¡Þ";
                break;
            case BuffTimeType.Temporary:
                transform.Find("Time").GetComponent<Text>().text = lastTime.ToString();
                break;
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
        FindObjectOfType<HoverTipManager>().ShowTip(buffInfo.buffName, buffInfo.description, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
    #endregion
}
