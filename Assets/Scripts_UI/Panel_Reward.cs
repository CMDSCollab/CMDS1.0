using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panel_Reward : PanelController
{
    public int goldPercentage;
    public GameObject preGold;
    public GameObject preCard;
    public bool isPlayerWin = false;
    public List<CardInfo> rewardCardPool = new List<CardInfo>();

    public override void Start()
    {
        if (isPlayerWin == true)
        {
            InstantiateDrops();
        }
        else
        {
            transform.Find("YouWinText").GetComponent<Text>().text = "Ê§°Ü";
            transform.Find("BackToMap").gameObject.SetActive(false);
            transform.Find("BackToMain").gameObject.SetActive(true);
        }
        base.Start();
    }

    void InstantiateDrops()
    {
        if (gM.relicM.FindUnobtainedRelics().Count == 0)
        {
            InstantiateGold();
        }
        else
        {
            int random = Random.Range(0, 100);
            if (random < goldPercentage)
            {
                InstantiateGold();
            }
            else
            {
                InstantiateCard();
            }
        }

        void InstantiateGold()
        {
            GameObject gold = Instantiate(preGold);
            gold.transform.SetParent(this.transform);
            gold.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }

        void InstantiateCard()
        {
            GameObject newCard = Instantiate(preCard);
            CardManager cardM = newCard.GetComponent<CardManager>();
            int random = Random.Range(0, rewardCardPool.Count);
            cardM.cardInfo = rewardCardPool[random];
            cardM.IntializeCard();
            cardM.inWhere = CardInWhere.InChest;
            newCard.transform.SetParent(this.transform);
            newCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }
    }

    public override void OnClickLeave()
    {
        gM.FightEndReset();
        gM.mapM.gameObject.SetActive(true);
        base.OnClickLeave();
    }

    public void OnClickBackToMainMenu()
    {
        SceneManager.LoadScene("SelectCharacter");
    }
}
