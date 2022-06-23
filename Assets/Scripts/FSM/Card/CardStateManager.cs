using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiscardType
{
    InUse,
    AllHand,
    Random,
    Specific
}

public class CardStateManager : MonoBehaviour
{
    private GameMaster gM;
    public Sprite[] cardTemplateImage;
    public CardBaseState currentState;
    public CardS_Default defaultState = new CardS_Default();
    public CardS_Draw drawState = new CardS_Draw();
    public CardS_Select selectState = new CardS_Select();
    public CardS_EndSelect endSelectState = new CardS_EndSelect();
    [HideInInspector]
    public int selectCardIndex;
    [HideInInspector]
    public Vector3 selectedTargetPos;
    [HideInInspector]
    public float selectMoveAmount = 50;
    [HideInInspector]
    public RectTransform selectedRect;
    public CardS_Use useState = new CardS_Use();
    public CardManager cardInUse;
    public CardS_Discard discardState = new CardS_Discard();
    public DiscardType discardType;

    public Vector3 cardTargetPos;
    public bool isUpdate = false;
    public int changedValue;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        currentState = drawState;
    }

    void Update()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM);
        }
    }

    public void EnterCardState(CardBaseState state)
    {
        currentState = state;
        currentState.EnterState(gM);
    }

    public List<Vector3> cardsPos = new List<Vector3>();
    private float targetXPos;
    private float targetYPos = -400;
    public float moveSpeed = 1000;
    public List<bool> movingRecord = new List<bool>();

    public void CalculateAllCardsDefaultPos(GameMaster gM)
    {
        movingRecord.Clear();
        cardsPos.Clear();
        float cardAreaWidth = CardAreaWidthSet(gM.handM.handCardList.Count);
        float singleCardAreaWitdth = cardAreaWidth / gM.handM.handCardList.Count;
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            targetXPos = -(cardAreaWidth / 2) + singleCardAreaWitdth / 2 + singleCardAreaWitdth * i;
            cardsPos.Add(new Vector3(targetXPos, targetYPos, 0));
            movingRecord.Add(false);
        }
    }

    public List<Vector3> seperateCardsPos = new List<Vector3>();
    public List<bool> seperateMovingRecord = new List<bool>();
    private float seperateXPos;
    private float seperateYPos = -400;

    public void CalculateAllCardsSeperatePos(GameMaster gM)
    {
        seperateCardsPos.Clear();
        seperateMovingRecord.Clear();
        float cardAreaWidth = CardAreaWidthSet(gM.handM.handCardList.Count);
        float singleCardAreaWitdth = cardAreaWidth / gM.handM.handCardList.Count;
        for (int i = 0; i < gM.handM.handCardList.Count; i++)
        {
            if (i < selectCardIndex)
            {
                seperateXPos = -(cardAreaWidth / 2) + singleCardAreaWitdth / 2 + singleCardAreaWitdth * i;
                seperateCardsPos.Add(new Vector3(seperateXPos - 50, seperateYPos, 0));
                movingRecord.Add(false);
            }
            else
            {
                seperateXPos = -(cardAreaWidth / 2) + singleCardAreaWitdth / 2 + singleCardAreaWitdth * i;
                seperateCardsPos.Add(new Vector3(seperateXPos + 50, seperateYPos, 0));
                movingRecord.Add(false);
            }
        }
    }

    public float CardAreaWidthSet(int cardNumber)
    {
        switch (cardNumber)
        {
            case 0:
                return 0;
            case 1:
                return 150;
            case 2:
                return 300;
            case 3:
                return 450;
            case 4:
                return 600;
            case 5:
                return 750;
            case 6:
                return 900;
            default:
                return 1000;
        }
    }
}
