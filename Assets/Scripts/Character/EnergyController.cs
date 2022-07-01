using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    private GameMaster gM;
    public Sprite[] energySlots;
    public Sprite[] energyPoints;
    public GameObject preEnergy;
    public int energyIndex;
    private Vector3 startPos = new Vector3(-74, -94, 0);
    private int offset = 28;

    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void InitializeEnergy()
    {
        if (transform.GetComponentInParent<AIMate>() is ArtistAI)
        {
            energyIndex = 2;
        }
        if (transform.GetComponentInParent<AIMate>() is ProgrammerAI)
        {
            energyIndex = 1;
        }
        if (transform.GetComponentInParent<AIMate>() is DesignerAI)
        {
            energyIndex = 0;
        }
        for (int i = 0; i < transform.GetComponentInParent<AIMate>().energySlotAmount; i++)
        {
            GameObject slot = Instantiate(preEnergy, this.transform, false);
            slot.GetComponent<Image>().sprite = energySlots[energyIndex];
            slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos.x + offset * i, startPos.y, 0);
        }
    }

    public void InstantiateEnergy()
    {
        GameObject slot = Instantiate(preEnergy, this.transform, false);
        slot.GetComponent<Image>().sprite = energySlots[energyIndex];
        slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos.x + offset * (transform.childCount - 1), startPos.y, 0);
    }

    public void ChangeSprite(int index)
    {
        if (transform.GetChild(index).GetComponent<Image>().sprite == energySlots[energyIndex])
        {
            transform.GetChild(index).GetComponent<Image>().sprite = energyPoints[energyIndex];
        }
        else
        {
            transform.GetChild(index).GetComponent<Image>().sprite = energySlots[energyIndex];
        }
    }
}
