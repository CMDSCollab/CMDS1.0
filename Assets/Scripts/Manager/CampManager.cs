using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampManager : MonoBehaviour
{
    public GameObject campPanelPrefab;
    public GameMaster gM;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void OnClickMapnode_Camp()
    {
        GameObject campPanel = Instantiate(campPanelPrefab);
        campPanel.transform.SetParent(gM.uiCanvas.transform);
    }
}
