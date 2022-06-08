using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ExpandArtistCards {public string cardname; public CardInfoArt card;}

public class DeckManager : MonoBehaviour
{
    public List<CardInfo> designerBaseCard;
    public List<CardInfo> programmerBaseCard;
    public List<CardInfo> artistBaseCard;
    public List<ExpandArtistCards> artistExpandCard;
    public Dictionary<string,CardInfoArt> artistExpandCardDic;
    public Dictionary<int,CardInfo> cardInDeck = new Dictionary<int, CardInfo>(); //��Ϊ������ܻ���ֿ��ƿ���ǿ���������ͬ��һ�ſ����ܳ�������һ��������Ч����һ���������������Ҫ��������
    public Dictionary<int,CardInfo> cardInDeckCopy = new Dictionary<int, CardInfo>(); //����Ϊscriptable obj����Ŀ�ļ�������ֻ����һ��ʵ����������Ҫ�ڳ����ڽ�һ������ÿ��ʵ����������
    public GameObject cardPrefab;
    public List<GameObject> cardPrefabs;

    public GameMaster gM;
    public int initialCardAmount;
    public int drawCardAmount;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
        MakeArtistExpandCardDic();
        InitializeDicCardInDeck();
    }

    public void MakeArtistExpandCardDic()
    {
        artistExpandCardDic = new Dictionary<string, CardInfoArt>();

        foreach(ExpandArtistCards expandCard in artistExpandCard)
        {
            artistExpandCardDic.Add(expandCard.cardname, expandCard.card);
        }
    }

    public void PrepareDeckAndHand()
    {
        //InitializeDicCardInDeck();
        GetNewCopyDeck();
        SwitchActiveStatusForDeckAndDiscardPile(true);
        DrawCardFromDeckRandomly(initialCardAmount);
    }

    public void InitializeDicCardInDeck()
    {
        if (gM.characterM.mainCharacterType == CharacterType.Designer)
        {
            cardPrefab = cardPrefabs[0];

            for (int i = 0; i < designerBaseCard.Count; i++)
            {
                cardInDeck[i] = designerBaseCard[i];
            }
        }
        else if (gM.characterM.mainCharacterType == CharacterType.Programmmer)
        {
            cardPrefab = cardPrefabs[1];
            for (int i = 0; i < programmerBaseCard.Count; i++)
            {
                cardInDeck[i] = programmerBaseCard[i];
            }
        }
        else if(gM.characterM.mainCharacterType == CharacterType.Artist)
        {
            cardPrefab = cardPrefabs[2];
            for (int i = 0; i < artistBaseCard.Count; i++)
            {
                cardInDeck[i] = artistBaseCard[i];
            }
        }

        gM.cardRepoM.deckPile = new Dictionary<int, CardInfo>(cardInDeck);
        gM.buttonM.SynchronizeCardsCountInPileButton("Deck");
    }

    public void SwitchActiveStatusForDeckAndDiscardPile(bool status)
    {
        gM.buttonM.discardPileButton.gameObject.SetActive(status);
        gM.buttonM.drawPileButton.gameObject.SetActive(status);
    }
    
    public void GetNewCopyDeck()
    {
        cardInDeckCopy = new Dictionary<int, CardInfo>(cardInDeck);
    }

    public void DrawCardFromDeckRandomly (int amount)
    {
        if(amount < cardInDeckCopy.Count)
        {
            for (int i = 0; i < amount; i++)
            {
                DrawRandomSingleCard();
            }
        }
        else
        {
            int extra = amount - cardInDeckCopy.Count;
            for(int i  = cardInDeckCopy.Count; i > 0; i = cardInDeckCopy.Count)
            {
                DrawRandomSingleCard();
            }

            ShuffleDiscardPileToDeck();

            for(int i = 0; i < extra; i++)
            {
                DrawRandomSingleCard();
            }
        }
        gM.cardSM.EnterCardState(gM.cardSM.drawState);
    }

    public void DrawRandomSingleCard()
    {
        List<int> residueKeys = new List<int>(); //��Ϊ����dic�����Բ���ֱ��random��������6��Ӧ��ֵ��ȡ����Ȼ����0-8���ֳ�ȡ��6�ͻ�ȡ������Ӧֵ
        foreach (int key in cardInDeckCopy.Keys)
        {
            residueKeys.Add(key);
        }
        int residueKeysIndex = Random.Range(0, residueKeys.Count);

        GameObject drawCard = Instantiate(cardPrefab);
        drawCard.gameObject.transform.SetParent(gM.handM.transform);
        drawCard.GetComponent<CardManager>().cardInfo = cardInDeckCopy[residueKeys[residueKeysIndex]];

        drawCard.GetComponent<CardManager>().handIndex = gM.handM.handCardList.Count + 1;
        drawCard.GetComponent<CardManager>().deckIndexRecord = residueKeys[residueKeysIndex];
        drawCard.GetComponent<CardManager>().cardMovement = CardMovement.Draw;
        gM.handM.handCardList.Add(drawCard.gameObject);
        //gM.handM.OrganizeHand();
        cardInDeckCopy.Remove(residueKeys[residueKeysIndex]);
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw"); //ͬ�����ƶѿ�������չʾText

        //Debug.Log(drawCard.GetComponent<CardManager>().cardInfo.cardName);
        //Debug.Log(drawCard.GetComponent<CardManager>().handIndex);
    }

    public void ShuffleDiscardPileToDeck()
    {
        cardInDeckCopy = new Dictionary<int, CardInfo>(gM.cardRepoM.discardPile);
        gM.cardRepoM.discardPile.Clear();
        gM.buttonM.SynchronizeCardsCountInPileButton("Discard"); //ͬ�����ƶѿ�������չʾText
        gM.buttonM.SynchronizeCardsCountInPileButton("Draw");
    }

    public void DrawSpecificSingleCard(int deckIndexRecord, Dictionary<int,CardInfo> targetCardList) //���ض�CardList��ȡ�ض���
    {
        GameObject drawCard = Instantiate(cardPrefab);
        drawCard.gameObject.transform.SetParent(gM.handM.transform);
        drawCard.GetComponent<CardManager>().cardInfo = cardInDeckCopy[deckIndexRecord];

        drawCard.GetComponent<CardManager>().handIndex = gM.handM.handCardList.Count + 1;
        drawCard.GetComponent<CardManager>().deckIndexRecord = deckIndexRecord;
        gM.handM.handCardList.Add(drawCard.gameObject);
        //gM.handM.OrganizeHand();
        cardInDeckCopy.Remove(deckIndexRecord);

        gM.buttonM.SynchronizeCardsCountInPileButton("Draw"); //ͬ�����ƶѿ�������չʾText
    }

    public void DrawSpecificSingleArtistExpandCard(string cardName)
    {
        GameObject drawCard = Instantiate(cardPrefab);
        drawCard.gameObject.transform.SetParent(gM.handM.transform);
        drawCard.GetComponent<CardManager>().cardInfo = artistExpandCardDic[cardName];

        drawCard.GetComponent<CardManager>().handIndex = gM.handM.handCardList.Count + 1;
        //drawCard.GetComponent<CardManager>().deckIndexRecord = index;
        gM.handM.handCardList.Add(drawCard.gameObject);
        //gM.handM.OrganizeHand();
    }
}
