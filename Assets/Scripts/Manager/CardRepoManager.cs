using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardRepoManager : MonoBehaviour
{
    private GameObject cardPrefab;
    public List<GameObject> cardPrefabs;
    public GameObject parentLayout;

    public Dictionary<int, CardInfo> drawPile = new Dictionary<int, CardInfo>();
    public Dictionary<int, CardInfo> discardPile = new Dictionary<int, CardInfo>();
    public Dictionary<int, CardInfo> deckPile = new Dictionary<int, CardInfo>();
    public Dictionary<int, CardInfo> cardsOnPresent = new Dictionary<int, CardInfo>();

    public DeckManager deckM;
    private int rowNumber = 5;
    private float yDistance = 400;
    private float xDistance = 200;

    private void Update()
    {
        CardScroll();
    }

    public void InstantiateCard(Dictionary<int,CardInfo> pile)
    {
        List<int> residueKeys = new List<int>();
        foreach (int key in pile.Keys)
        {
            residueKeys.Add(key);
        }
        for (int i = 0; i < residueKeys.Count; i++)
        {
            GameObject drawCard = Instantiate(cardPrefabs[0]);
            drawCard.gameObject.transform.SetParent(parentLayout.transform);
            drawCard.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            drawCard.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            drawCard.GetComponent<RectTransform>().anchoredPosition = CardPosSet();
            drawCard.GetComponent<CardManager>().cardInfo = pile[residueKeys[i]];
            drawCard.GetComponent<CardManager>().deckIndexRecord = residueKeys[i];
            drawCard.GetComponent<CardManager>().inWhere = CardInWhere.InDeck;
        }

        Vector3 CardPosSet()
        {
            int column = Mathf.CeilToInt((parentLayout.transform.childCount-1) / rowNumber);
            int row;
            if (parentLayout.transform.childCount < 6)
            {
                row = parentLayout.transform.childCount;
            }
            else
            {
                row = parentLayout.transform.childCount - column * rowNumber;
            }
            Debug.Log(row+","+column);
            Vector3 pos = new Vector3(xDistance * row, -yDistance * column, 0);
            return pos;
        }
    }

    public void PresentDrawPile()
    {
        gameObject.SetActive(true);
        drawPile = new Dictionary<int, CardInfo>(deckM.cardInDeckCopy);
        InstantiateCard(drawPile);
    }
    
    public void PresentDeck()
    {
        gameObject.SetActive(true);
        deckPile = new Dictionary<int, CardInfo>(deckM.cardInDeck);
        InstantiateCard(deckPile);  
    }

    public void PresentDiscardPile()
    {
        gameObject.SetActive(true);
        InstantiateCard(discardPile);
    }

    public void RemoveCardsFromLayout()
    {
        for (int i = 0; i < parentLayout.transform.childCount; i++)
        {
            Destroy(parentLayout.transform.GetChild(i).gameObject);
        }
    }

    public void CardScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Find("CardLayout").Translate(Vector3.up * 20f);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.Find("CardLayout").Translate(Vector3.up * -20f);
            }
        }
    }
}
