using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantManager : MonoBehaviour
{
    public GameMaster gM;
    public GameObject panelPrefab;
    public List<CardInfo> DesignerCardsForSell;
    public GameObject cardForSellPrefab;//Private ¿¨ÅÆµÄÄ£°å 

    [Tooltip("¿¨ÅÆ¼Û¸ñ¸¡¶¯·¶Î§")]
    public int priceRange;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();

    }

    public void OnClickMapNode_Merchant()
    {
        GameObject merchantPanel = Instantiate(panelPrefab);
        merchantPanel.GetComponent<MerchantMaster>().merM = this;
        merchantPanel.transform.SetParent(gM.uiCanvas.transform);
        merchantPanel.transform.localPosition = new Vector3(0, 100, 0);
        gM.merchantSM.currentItemRectTrans = merchantPanel.GetComponent<RectTransform>();
        gM.merchantSM.EnterMerchantState(gM.merchantSM.startState);
    }
}
