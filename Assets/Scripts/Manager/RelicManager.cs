using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelicName
{
    HandCardDrawAmountPlus,
    PlayerDmgPlus,
    HpRegenerationOnMapMove
}

public class RelicManager : MonoBehaviour
{
    public GameMaster gM;
    public List<RelicName> activeRelics = new List<RelicName>();
    public delegate void RelicDelegate();


    RelicDelegate relicDelegate;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        relicDelegate = DebugH;
        //relicDelegate = new RelicDelegate { Debug.Log("asdasda")};
        relicDelegate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RelicEffectApply(RelicName relicName)
    {
        if (activeRelics.Contains(relicName))
        {
            switch (relicName)
            {
                case RelicName.HandCardDrawAmountPlus:
                    gM.deckM.drawCardAmount = 4;
                    break;
                case RelicName.PlayerDmgPlus:
                    gM.buffSM.valueToCalculate += 1;
                    break;
                case RelicName.HpRegenerationOnMapMove:
                    break;
            }
        }
    }

    private void DebugH()
    {
        //Debug.Log("hhhhhh");
    }
}
