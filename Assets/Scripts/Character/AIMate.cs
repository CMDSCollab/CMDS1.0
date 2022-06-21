using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMate : BasicCharacter
{
    public int energyPoint = 0;
    public int energySlotAmount = 3;
    [HideInInspector]
    public List<Image> energyPointImageList = new List<Image>();
    public List<IntentionManager> intentions = new List<IntentionManager>();
    [HideInInspector]
    public Intentions currentIntention;
    private GameObject energy;
    [HideInInspector]
    public int intentionValue = 5;

    public virtual void Start()
    {
        //base.Start();
        intentions = characterInfo.intentions;
        GenerateIntention();
        //EnergyInitialize();
        //for (int i = 0; i < energySlotAmount; i++)
        //{
        //    AddTheEnergySlot();
        //}
    }

    //public void Update()
    //{
        
    //}

    //public void EnergyInitialize()
    //{
    //    if (this is DesignerAI)
    //    {
    //        transform.Find("EnergyPos").Find("EnergyImage").GetComponent<Image>().sprite = gM.characterM.energyImages[0];
    //    }
    //    if (this is ProgrammerAI)
    //    {
    //        transform.Find("EnergyPos").Find("EnergyImage").GetComponent<Image>().sprite = gM.characterM.energyImages[1];
    //    }
    //    if (this is ArtistAI)
    //    {
    //        transform.Find("EnergyPos").Find("EnergyImage").GetComponent<Image>().sprite = gM.characterM.energyImages[2];
    //    }
    //    transform.Find("EnergyPos").Find("SlotAmount").GetComponent<Text>().text = energySlotAmount.ToString();
    //    transform.Find("EnergyPos").Find("EnergyPoint").GetComponent<Text>().text = energyPoint.ToString();
    //}

    //public void AddTheEnergySlot()
    //{
    //    energy = Instantiate(gM.characterM.energyPrefab);
    //    energy.transform.SetParent(transform.Find("EnergyPos"));

    //    if (this is DesignerAI)
    //    {
    //        energy.GetComponent<Image>().sprite = gM.characterM.energyImages[0];
    //    }
    //    if (this is ProgrammerAI)
    //    {
    //        energy.GetComponent<Image>().sprite = gM.characterM.energyImages[1];
    //    }
    //    if (this is ArtistAI)
    //    {
    //        energy.GetComponent<Image>().sprite = gM.characterM.energyImages[2];
    //    }
    //    energy.transform.localScale = new Vector3(1, 1, 1);
    //    energyPointImageList.Add(energy.GetComponent<Image>());
    //    //÷ÿ÷√EnergySlotµƒŒª÷√
    //    int singleUnitWidth = (int)energy.GetComponent<RectTransform>().rect.width;
    //    float unitStartGenPos = -(singleUnitWidth * energyPointImageList.Count / 2) + singleUnitWidth / 2;
    //    for (int i = 0; i < energyPointImageList.Count; i++)
    //    {
    //        float unitXPos = unitStartGenPos + singleUnitWidth * i;
    //        energyPointImageList[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(unitXPos, 0, 0);
    //    }
    //}

    //public void EnergyValueChange(int changeAmount)
    //{
    //    energyPoint += changeAmount;

    //    for (int i = 0; i < energyPointImageList.Count; i++)
    //    {
    //        if (i < energyPoint)
    //        {
    //            energyPointImageList[i].color = Color.blue;
    //        }
    //        else
    //        {
    //            energyPointImageList[i].color = Color.white;
    //        }
    //    }
    //    IntentionValueChangeAndUISync();
    //}

    #region Intention
    public void GenerateIntention()
    {
        int random = Random.Range(0, intentions.Count);
        IntentionManager intentionM = intentions[random];
        SyncIntention(intentionM);
        IntentionValueChangeAndUISync();
    }

    public void ChangeIntention()
    {
        int random = Random.Range(0, intentions.Count);
        IntentionManager intentionM = intentions[random];
        while (intentionM.intention == currentIntention)
        {
            random = Random.Range(0, intentions.Count);
            intentionM = intentions[random];
        }
        SyncIntention(intentionM);
        IntentionValueChangeAndUISync();
    }

    public void SyncIntention(IntentionManager intentionM)
    {
        switch (intentionM.intention)
        {
            case Intentions.None:
                break;
            case Intentions.Attack:
                currentIntention = Intentions.Attack;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                //transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Attack";
                break;
            case Intentions.Heal:
                currentIntention = Intentions.Heal;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                //transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Heal";
                break;
            case Intentions.Shield:
                currentIntention = Intentions.Shield;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                //transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Shield";
                break;
            case Intentions.Buff:
                currentIntention = Intentions.Buff;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                //transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Buff";
                break;
            case Intentions.Debuff:
                currentIntention = Intentions.Debuff;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                //transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Debuff";
                break;
        }
    }

    public void IntentionValueChangeAndUISync()
    {
        int[] atk     = { 1, 5, 7, 10, 14, 25, 40 };
        int[] heal    = { 0, 1, 2, 3, 4, 5, 6 };
        int[] defence = { 1, 5, 7, 10, 14, 19, 25 };

        switch (currentIntention)
        {
            case Intentions.None:
                break;
            case Intentions.Attack:
                transform.Find("IntentionPos").Find("Value").GetComponent<Text>().text = atk[energyPoint].ToString();
                intentionValue = atk[energyPoint];
                break;
            case Intentions.Heal:
                transform.Find("IntentionPos").Find("Value").GetComponent<Text>().text = heal[energyPoint].ToString();
                intentionValue = heal[energyPoint];
                break;
            case Intentions.Shield:
                transform.Find("IntentionPos").Find("Value").GetComponent<Text>().text = defence[energyPoint].ToString();
                intentionValue = defence[energyPoint];
                break;
            case Intentions.Buff:
                break;
            case Intentions.Debuff:
                break;
        }
        //transform.Find("IntentionPos").Find("Value").GetComponent<Text>().text = intentionValue.ToString();
    }
    #endregion
    public virtual void TakeAction()
    {
        switch (currentIntention)
        {
            case Intentions.Attack:
                gM.actionSM.EnterActionState(gM.actionSM.attackState, intentionValue);
                if (this is ArtistAI)
                {
                    AudioManager.Instance.PlayAudio("Artist_Cast");
                }
                else if (this is ProgrammerAI)
                {
                    AudioManager.Instance.PlayAudio("Programmer_Attack");
                }
                else if (this is DesignerAI)
                {
                    AudioManager.Instance.PlayAudio("Designer_Attack");
                }
                break;
            case Intentions.Heal:
                gM.actionSM.EnterActionState(gM.actionSM.healState, intentionValue);
                AudioManager.Instance.PlayAudio("Artist_Heal");
                break;
            case Intentions.Shield:
                gM.actionSM.EnterActionState(gM.actionSM.defenceState, intentionValue);
                AudioManager.Instance.PlayAudio("Shield01");
                break;
            case Intentions.Buff:
                break;
            case Intentions.Debuff:
                break;
        }
    }
}
