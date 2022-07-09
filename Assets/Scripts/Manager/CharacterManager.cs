using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    #region ��ɫͨ�ñ���
    [Header("ͨ��")]
    public GameMaster gM;
    public CharacterType mainCharacterType = CharacterType.Designer;
    public GameObject templateAI;
    public GameObject templateCha;
    public List<CharacterInfo> characters = new List<CharacterInfo>();
    public List<Sprite> characterImages;
    [HideInInspector]
    public CharacterMate mainCharacter;
    public GameObject energyPrefab;
    public List<Sprite> energyImages;
    #endregion

    #region ��Ʊ���
    [Header("���")]
    public GameObject flowPre;
    #endregion

    #region �������
    [Header("����")]
    public ProDebugUI debugUIPrefab;
    public List<Error> potentialErrors;
    #endregion

    #region ��������
    [Header("����")]
    public GameObject hitsPanel;//����UI���

    #endregion


    public void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void InitializeCharacters()
    {
        switch (mainCharacterType)
        {
            case CharacterType.Designer:
                MainChaGenerateAndInitialize(CharacterType.Designer);
                AIGenerateAndInitialize(CharacterType.Artist, "Left");
                AIGenerateAndInitialize(CharacterType.Programmmer, "Right");
                mainCharacter = gM.aiM.des;
                break;
            case CharacterType.Programmmer:
                MainChaGenerateAndInitialize(CharacterType.Programmmer);
                AIGenerateAndInitialize(CharacterType.Artist, "Left");
                AIGenerateAndInitialize(CharacterType.Designer, "Right");

                mainCharacter = gM.aiM.pro;
                gM.aiM.pro.OnNewGameStarted();
                break;
            case CharacterType.Artist:
                MainChaGenerateAndInitialize(CharacterType.Artist);
                AIGenerateAndInitialize(CharacterType.Designer, "Left");
                AIGenerateAndInitialize(CharacterType.Programmmer, "Right");

                mainCharacter = gM.aiM.art;
                break;
            default:
                break;
        }
    }

    public void AIGenerateAndInitialize(CharacterType characterType,string leftOrRight)
    {
        GameObject aiObj = Instantiate(templateAI, gM.uiCanvas.transform, false);
        aiObj.transform.SetAsFirstSibling();
        if (leftOrRight == "Left")
        {
            aiObj.transform.Find("LeftPanel").gameObject.SetActive(true);
            aiObj.transform.Find("RightPanel").gameObject.SetActive(false);
            aiObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-826, -125, 0);
        }
        else
        {
            aiObj.transform.Find("LeftPanel").gameObject.SetActive(false);
            aiObj.transform.Find("RightPanel").gameObject.SetActive(true);
            aiObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-605, -125, 0);
        }
        switch (characterType)
        {
            case CharacterType.Designer:
                aiObj.AddComponent<DesignerAI>();
                aiObj.GetComponent<DesignerAI>().characterInfo = characters[0];
                aiObj.transform.Find("ChaImageMask").Find("DesImage").gameObject.SetActive(true);
                aiObj.transform.Find("NameArea").Find("Name").GetComponent<Text>().text = "Designer";
                gM.aiM.desAI = aiObj.GetComponent<DesignerAI>();
                break;
            case CharacterType.Programmmer:
                aiObj.AddComponent<ProgrammerAI>();
                aiObj.GetComponent<ProgrammerAI>().characterInfo = characters[1];
                aiObj.transform.Find("ChaImageMask").Find("ProImage").gameObject.SetActive(true);
                aiObj.transform.Find("NameArea").Find("Name").GetComponent<Text>().text = "Programmer";
                gM.aiM.proAI = aiObj.GetComponent<ProgrammerAI>();
                break;
            case CharacterType.Artist:
                aiObj.AddComponent<ArtistAI>();
                aiObj.GetComponent<ArtistAI>().characterInfo = characters[2];
                aiObj.transform.Find("ChaImageMask").Find("ArtImage").gameObject.SetActive(true);
                aiObj.transform.Find("NameArea").Find("Name").GetComponent<Text>().text = "Artist";
                gM.aiM.artAI = aiObj.GetComponent<ArtistAI>();
                break;
        }
    }

    public void MainChaGenerateAndInitialize(CharacterType characterType)
    {
        GameObject chaObj = Instantiate(templateCha, gM.uiCanvas.transform, false);
        chaObj.transform.SetAsFirstSibling();
        chaObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-710, 225, 0);
        
        switch (characterType)
        {
            case CharacterType.Designer:
                chaObj.AddComponent<Designer>();
                chaObj.GetComponent<Designer>().characterInfo = characters[0];
                mainCharacter = chaObj.GetComponent<Designer>();
                chaObj.transform.Find("ChaImageMask").Find("DesImage").gameObject.SetActive(true);
                gM.aiM.des = chaObj.GetComponent<Designer>();
                break;
            case CharacterType.Programmmer:
                chaObj.AddComponent<Programmer>();
                chaObj.GetComponent<Programmer>().characterInfo = characters[1];
                mainCharacter = chaObj.GetComponent<Programmer>();
                chaObj.transform.Find("ChaImageMask").Find("ProImage").gameObject.SetActive(true);
                gM.aiM.pro = chaObj.GetComponent<Programmer>();
                break;
            case CharacterType.Artist:
                chaObj.AddComponent<Artist>();
                chaObj.GetComponent<Artist>().characterInfo = characters[2];
                mainCharacter = chaObj.GetComponent<Artist>();
                chaObj.transform.Find("ChaImageMask").Find("ArtImage").gameObject.SetActive(true);
                gM.aiM.art = chaObj.GetComponent<Artist>();
                break;
        }
    }
}
