using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampMaster : MonoBehaviour
{
    public GameMaster gM;
    public RectTransform panelTrans;
    public Vector3 targetPos;
    public float speed;
    public int restPoint;
    public bool isUpdate;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }
    private void Start()
    {
        gM.characterM.mainCharacter.TakeDamage(20);
        panelTrans.transform.localPosition = new Vector3(0, 850, 0); 
        isUpdate = true;
    }
    public void OnClickLeave()
    {
        Destroy(this.gameObject);
    }

    public void OnClickRest()
    {
        gM.characterM.mainCharacter.HealSelf(restPoint);
    }

    private void Update()
    {
        if (isUpdate)
        {
            panelTrans.localPosition = Vector3.MoveTowards(panelTrans.localPosition, targetPos, speed * Time.deltaTime);
            if (panelTrans.localPosition == targetPos)
            {
                isUpdate = false;
            } 
        }
    }
}
