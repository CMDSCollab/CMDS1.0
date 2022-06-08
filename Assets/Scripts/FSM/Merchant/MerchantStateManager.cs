using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantStateManager : MonoBehaviour
{
    public GameMaster gM;
    public MerchantBaseState currentState;
    public MerState_Start startState = new MerState_Start();
    public MerState_Default defaultState = new MerState_Default();
    public MerState_Select selectState = new MerState_Select();
    public MerState_Deselect deselectState = new MerState_Deselect();
    public MerState_Sold soldState = new MerState_Sold();
    public RectTransform currentItemRectTrans;

    public float moveSpeed = 1000;
    public Vector3 targetPos;
    public Vector3 originPos;

    public bool isUpdate = false;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();

    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = startState;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM);
        }
    }

    public void EnterMerchantState(MerchantBaseState state)
    {
        currentState = state;
        currentState.EnterState(gM);
    }

    public void DestoryItemApperance()
    {
        Destroy(currentItemRectTrans.gameObject);
    }
}
