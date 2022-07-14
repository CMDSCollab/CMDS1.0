using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameMaster gM;
    public GlobalMaster globalM;
    public CardRepoManager cardRepoM;

    public Button drawPileButton;
    public Button discardPileButton;
    public Button nextTurnButton;
    public Button deckCardButton;
    public GameObject startScene;
    public GameObject chooseCharacterPanel;
  

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();

    }

    void Update()
    {
        
    }

    public void OnDeckClicked()
    {
        cardRepoM.PresentDeck();
    }
    public void OnDrawPileClicked()
    {
        cardRepoM.gameObject.transform.SetAsLastSibling();
        cardRepoM.PresentDrawPile();
    }

    public void OnDicardPileClicked()
    {
        cardRepoM.PresentDiscardPile();
    }

    public void OnCardLayoutReturnClicked()
    {
        cardRepoM.RemoveCardsFromLayout();
        cardRepoM.gameObject.SetActive(false);
    }

    public void SynchronizeCardsCountInPileButton(string whichPileNumber)
    {
        if (whichPileNumber == "Draw")
        {
            drawPileButton.transform.Find("DrawPileNumber").GetComponent<Text>().text = gM.deckM.cardInDeckCopy.Count.ToString();
        }
        else if (whichPileNumber == "Discard")
        {
            discardPileButton.transform.Find("DiscardPileNumber").GetComponent<Text>().text = cardRepoM.discardPile.Count.ToString();
        }
        else if(whichPileNumber == "Deck")
        {
            //Debug.Log("Show" + cardRepoM.deckPile.Count.ToString());
            deckCardButton.transform.Find("DeckCardNumber").GetComponent<Text>().text = cardRepoM.deckPile.Count.ToString();
        }
       
    }

    public void OnClickNextTurn()
    {
        //gM.fightM.FightProcessManager();
        if (gM.cardSM.currentState == gM.cardSM.defaultState && gM.combatSM.currentState == gM.combatSM.startState)
        {
            gM.combatSM.CombatStateProcess();
        }

    }

    public void onClickStartButton()
    {
        startScene.SetActive(false);
        GameObject.Find("Canvas").transform.Find("VideoPlayer").gameObject.SetActive(false);
        chooseCharacterPanel.SetActive(true);
    }

    public void OnClickDesigner()
    {
        globalM.characterType = CharacterType.Designer;
        SceneManager.LoadScene("FightScene");
    }

    public void OnClickProgrammer()
    {
        globalM.characterType = CharacterType.Programmmer;
        SceneManager.LoadScene("FightScene");
    }

    public void OnClickArtist()
    {
        globalM.characterType = CharacterType.Artist;
        SceneManager.LoadScene("FightScene");
    }

    public void EndFightBackToMap()
    {
        gM.uiCanvas.transform.Find("RewardPanel").gameObject.SetActive(false);
        gM.FightEndReset();
        gM.mapM.gameObject.SetActive(true);
        //gM.uiCanvas.gameObject.SetActive(false);
    }
}
