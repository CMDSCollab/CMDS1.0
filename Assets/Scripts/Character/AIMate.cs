using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMate : BasicCharacter
{
    public int energyPoint = 0;
    public int energySlotAmount = 3;

    public List<IntentionManager> intentions = new List<IntentionManager>();
    [HideInInspector]
    public Intentions currentIntention;
    [HideInInspector]
    public int intentionValue = 5;

    public virtual void Start()
    {
        intentions = characterInfo.intentions;
        GenerateIntention();
        transform.Find("Energy").GetComponent<EnergyController>().InitializeEnergy();
    }

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
        int[] heal    = { 0, 1, 2, 3, 5, 7, 10 };
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
                    AudioManager.Instance.PlayAudio("Artist_Don't_Cross_Me");
                }
                else if (this is ProgrammerAI)
                {
                    AudioManager.Instance.PlayAudio("Programmer_Attack");
                    AudioManager.Instance.PlayAudio("Programmer_Incoming");
                }
                else if (this is DesignerAI)
                {
                    AudioManager.Instance.PlayAudio("Designer_Attack");
                }
                break;
            case Intentions.Heal:
                gM.actionSM.EnterActionState(gM.actionSM.healState, intentionValue);
                AudioManager.Instance.PlayAudio("Artist_Heal");
                AudioManager.Instance.PlayAudio("Artist_Need_A_Repair");
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
