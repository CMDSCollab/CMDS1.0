using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldController : MonoBehaviour
{
    GameMaster gM;
    private RectTransform rectTrans;
    private int number;
    private bool isGoldGet = false;
    private float localScale = 1;

    public void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        rectTrans = GetComponent<RectTransform>();
        InitializeGold();
    }

    private void Update()
    {
        if (isGoldGet)
        {
            StartCoroutine(GoldGet());
        }
    }

    public void InitializeGold()
    {
        number = Random.Range(50, 100);
        rectTrans.Find("Text").GetComponent<Text>().text = number.ToString();

    }

    public void OnMouseClick()
    {

        isGoldGet = true;
    }

    IEnumerator GoldGet()
    {
        localScale += 0.7f*Time.deltaTime;
        rectTrans.localScale = new Vector3(localScale, localScale, 0);
        yield return new WaitForSeconds(0.4f);
        gM.comStatusBar.GoldChange(number);
        Destroy(gameObject);
    }
}
