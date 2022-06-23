using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public AIManager aiM;
    public EnemyMaster enM;
    public HandManager handM;
    public DeckManager deckM;
    public CardRepoManager cardRepoM;
    public ButtonManager buttonM;
    public CardFuntionManager cardFunctionM;
    public CharacterManager characterM;
    public BuffManager buffM;
    public MapManager mapM;
    public MerchantManager merM;
    public CampManager campM;
    public RelicManager relicM;

    public CombatStateManager combatSM;
    public ActionStateManager actionSM;
    public BuffStateManager buffSM;
    public CardStateManager cardSM;
    public CEffectStateManager cEffectSM;
    public MerchantStateManager merchantSM;
    public AnimCollection animC;
    public CommonStatusBar comStatusBar;
    

    public Canvas uiCanvas;

    public void PrepareFight()
    {
        if (GameObject.Find("GlobalManager") != null)
        {
            characterM.mainCharacterType = GameObject.Find("GlobalManager").GetComponent<GlobalMaster>().characterType;
        }
        characterM.InitializeCharacters();
        enM.InitializeEnemy();
        relicM.RelicEffectApply(RelicName.HandCardDrawAmountPlus);
        deckM.PrepareDeckAndHand();
    }

    public void FightEndReset()
    {
        buffM.chaBuffs.Clear();
        buffM.enBuffs.Clear();
        handM.handCardList.Clear();
        cardRepoM.discardPile.Clear();
        cardRepoM.drawPile.Clear();
        deckM.SwitchActiveStatusForDeckAndDiscardPile(false);
        switch (characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                Destroy(aiM.proAI.gameObject);
                Destroy(aiM.artAI.gameObject);
                Destroy(aiM.des.flow);
                Destroy(aiM.des.gameObject);
                break;
            case CharacterType.Programmmer:
                Destroy(aiM.desAI.gameObject);
                Destroy(aiM.artAI.gameObject);
                Destroy(aiM.pro.gameObject);
                Destroy(aiM.pro.debugUI.gameObject);
                break;
            case CharacterType.Artist:
                break;
        }
        Destroy(enM.enemyTarget.gameObject);
        GameObject handObj = uiCanvas.transform.Find("Hand").gameObject;
        for (int i = 0; i < handObj.transform.childCount; i++)
        {
            Destroy(handObj.transform.GetChild(i).gameObject);
        }
        combatSM.currentState = combatSM.startState;
        cardSM.currentState = cardSM.defaultState;
    }
}
