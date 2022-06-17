using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTip : MonoBehaviour
{
    public string tipToShow;
    private float timeToWait = 0.5f;

    public void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnMouseExit()
    {
        StopAllCoroutines();
    }

    private void ShowMessage()
    {
        if (CompareTag("Buff"))
        {

        }
        //HoverTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
