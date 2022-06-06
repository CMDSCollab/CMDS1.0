using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������BaseMapNode���prefab�ϣ����������ǵ����Node����������Ч�����ͼ�¼��ǰNode����Ϣ
public class MapNodeManager : MonoBehaviour
{
    private MapManager mapM;
    public MapNode mapNode;
    public int linkTargetCount;
    public List<int> linkTargetIndexList;

    public void Awake()
    {
        mapM = FindObjectOfType<MapManager>();
    }

    public void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnMouseDown()
    {
        switch (mapNode.nodeType)
        {
            case NodeType.None:
                break;
            case NodeType.Minion:
                EnterBattleScene(EnemyType.Minion);
                break;
            case NodeType.Elite:
                EnterBattleScene(EnemyType.Elite);
                break;
            case NodeType.Boss:
                EnterBattleScene(EnemyType.Boss);
                break;
            case NodeType.Chest:
                break;
            case NodeType.Rest:
                break;
            case NodeType.Merchant:
                break;
            case NodeType.Uncertainty:
                break;
        }
    }

    public void EnterBattleScene(EnemyType enemyType)
    {
        List<EnemyInfo> certainTypeEnemyPool = new List<EnemyInfo>();
        foreach (EnemyInfo enemyInfo in mapM.levelEnemyPool)
        {
            if (enemyInfo.enemyType == enemyType)
            {
                certainTypeEnemyPool.Add(enemyInfo);
            }
        }
        int random = Random.Range(0, certainTypeEnemyPool.Count);
        mapM.gM.enM.SetEnemyInfo(certainTypeEnemyPool[random]);
        mapM.gM.enM.testEnemy = certainTypeEnemyPool[random];
        mapM.gM.uiCanvas.gameObject.SetActive(true);
        mapM.gM.PrepareFight();
        mapM.gameObject.SetActive(false);
    }
}
