using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonStatusBar : MonoBehaviour
{
    public Text goldIntText;
    public GameMaster gM;
    public int gold = 0;
    public Text hpIntText;
    public int healthPoint;
    public int maxHealth;
    private int highestRelicCount = 9;

    public List<CharacterInfo> chaInfos;
    public CharacterInfo currentInfo;
    

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        DetectChaType();
    }

    public void GoldChange(int changeAmount)
    {
        gold += changeAmount;
        goldIntText.text = gold.ToString();
    }

    public void HealthUIUpdate()
    {
        healthPoint = gM.characterM.mainCharacter.healthPoint;
        hpIntText.text = healthPoint + "/" + maxHealth;
    }

    public void DetectChaType()
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                currentInfo = chaInfos[0];
                break;
            case CharacterType.Programmmer:
                break;
            case CharacterType.Artist:
                break;
            default:
                break;
        }
        ParametersInitilization();

        void ParametersInitilization()
        {
            GoldChange(currentInfo.initialGold);
            maxHealth = currentInfo.maxHp;
            healthPoint = currentInfo.maxHp;
            hpIntText.text = healthPoint.ToString() + "/" + maxHealth.ToString();
        }
    }

    public Vector3 RelicPosSet()
    {
        float relicAreaWidth = transform.Find("RelicArea").GetComponent<RectTransform>().rect.width;
        float singleRelicRange = relicAreaWidth / highestRelicCount;
        float startPos = -relicAreaWidth / 2 + singleRelicRange / 2;
        Vector3 targetPos = new Vector3((gM.relicM.activeRelics.Count-1) * singleRelicRange + startPos, 0, 0);
        return targetPos;
    }
}
