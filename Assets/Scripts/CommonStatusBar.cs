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
        hpIntText.text = gM.characterM.mainCharacter.healthPoint.ToString() + "/" + gM.characterM.mainCharacter.maxHp;
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
    }

    public void ParametersInitilization()
    {
        GoldChange(currentInfo.initialGold);
        hpIntText.text = currentInfo.maxHp.ToString() + "/" + currentInfo.maxHp.ToString();

    }
}
