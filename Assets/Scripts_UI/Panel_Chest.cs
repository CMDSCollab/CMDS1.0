using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Chest : PanelController
{
    public int goldPercentage;
    public GameObject preGold;
    public GameObject preRelic;

    public override void Start()
    {
        InstantiateDrops();
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
                InstantiateRelic();
            }
        }

        void InstantiateGold()
        {
            GameObject gold = Instantiate(preGold);
            gold.transform.SetParent(this.transform);
            gold.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }

        void InstantiateRelic()
        {
            GameObject newRelic = Instantiate(preRelic);
            RelicController relicController = newRelic.GetComponent<RelicController>();
            relicController.relicInfo = PickOneRelicByRandom(gM.relicM.FindUnobtainedRelics());
            relicController.InitializeRelic();
            relicController.inWhere = RelicInWhere.inChest;
            newRelic.transform.SetParent(this.transform);
            newRelic.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            RelicInfo PickOneRelicByRandom(List<RelicInfo> relicsToPick)
            {
                int index = Random.Range(0, relicsToPick.Count);
                return relicsToPick[index];
            }
        }
    }
}
