using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Merchant : PanelController
{
    public List<CardInfo> CurrentCardsForSell;
    public List<Transform> cardSlots;
    public List<Transform> relicSlots;
    public GameObject cardForSellPrefab;
    public GameObject relicPrefab;
    public int priceRange;
    public List<RelicInfo> unobtainedRelics = new List<RelicInfo>();

    public override void Start()
    {
        base.Start();
        unobtainedRelics = FindObjectOfType<GameMaster>().relicM.FindUnobtainedRelics();
        PrepareCardsForSellByRandom();
        PrepareRelicsForSellByRandom();
    }

    public void PrepareCardsForSellByRandom()
    {
        for (int i = 0; i < cardSlots.Count; i++)
        {
            PrepareOneCardForSellByRandom(cardSlots[i]);
        }

        void PrepareOneCardForSellByRandom(Transform slotTran)
        {
            GameObject newCard = Instantiate(cardForSellPrefab);
            newCard.GetComponent<CardManager>().inWhere = CardInWhere.InShop;
            newCard.GetComponent<CardManager>().cardInfo = PickOneCardByRandom();
            newCard.transform.SetParent(slotTran);
            newCard.transform.position = slotTran.position;
            slotTran.gameObject.GetComponentInChildren<Text>().text = MakePriceForOneCard(newCard).ToString();

            int MakePriceForOneCard(GameObject card)
            {
                int basePrice = card.GetComponent<CardManager>().cardInfo.merchantBaseValue;
                int change = Random.Range(-priceRange, priceRange);
                card.GetComponent<CardManager>().cardInfo.realValue = basePrice + change;

                return basePrice + change;
            }

            CardInfo PickOneCardByRandom()
            {
                int index = Random.Range(0, CurrentCardsForSell.Count);
                return CurrentCardsForSell[index];
            }
        }
    }

    public void PrepareRelicsForSellByRandom()
    {
        for (int i = 0; i < relicSlots.Count; i++)
        {
            if (unobtainedRelics.Count > 0)
            {
                PrepareOneRelic(relicSlots[i]);
            }
        }

        void PrepareOneRelic(Transform slotTran)
        {
            slotTran.gameObject.SetActive(true);
            GameObject newRelic = Instantiate(relicPrefab);
            RelicController relicController = newRelic.GetComponent<RelicController>();

            int index = Random.Range(0, unobtainedRelics.Count);
            relicController.relicInfo = unobtainedRelics[index];
            unobtainedRelics.RemoveAt(index);

            relicController.InitializeRelic();
            relicController.inWhere = RelicInWhere.inMerchant;
            newRelic.transform.SetParent(slotTran);
            newRelic.transform.position = slotTran.position;
            slotTran.Find("Price").GetComponent<Text>().text = MakePriceForRelic(relicController.relicInfo).ToString();

            int MakePriceForRelic(RelicInfo relicInfo)
            {
                int basePrice = relicInfo.price;
                int change = Random.Range(-priceRange, priceRange);
                relicController.realPrice = basePrice + change;
                return relicController.realPrice;
            }
        }
    }
}
