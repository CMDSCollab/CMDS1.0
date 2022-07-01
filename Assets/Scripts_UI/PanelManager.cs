using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType { Reward, Merchant, Chest, Rest, Uncertainty, Pause}

[System.Serializable]
public struct PanelObj { public PanelType type; public GameObject obj; }

public class PanelManager : MonoBehaviour
{
    GameMaster gM;
    public List<PanelObj> panelObjList;
    public bool isPause = false;
    public bool isPanelOpen = false;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && isPause == false)
        {
            InstantiatePanel(PanelType.Pause);
            isPause = true;
        }
    }

    public void InstantiatePanel(PanelType panelType)
    {
        isPanelOpen = true;
        GameObject panel = Instantiate(FindPanel(panelType));
        panel.transform.SetParent(gM.uiCanvas.transform);
        panel.transform.SetAsLastSibling();
        gM.comStatusBar.transform.SetAsLastSibling();

        GameObject FindPanel(PanelType type)
        {
            foreach (PanelObj panelObj in panelObjList)
            {
                if (panelObj.type == type)
                {
                    return panelObj.obj;
                }
            }
            return null;
        }
    }
}
