using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//放置在BaseMapNode这个prefab上，后续功能是当这个Node点击后产生的效果，和记录当前Node的信息
public class MapNodeManager : MonoBehaviour
{
    private MapManager mapM;
    public MapNode mapNode;
    public int linkTargetCount;
    public List<int> linkTargetIndexList;
    public int step;
    public bool ifApproachable;
    public bool ifExplored;
    public float scaleChangeSpeed;
    public float time;

    public void Awake()
    {
        mapM = FindObjectOfType<MapManager>();
    }

    public void OnMouseEnter()
    {
        if (ifApproachable)
        {
            //this.GetComponent<SpriteRenderer>().sprite = mapNode.activeSprite;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            transform.localScale = new Vector3(0.12f, 0.12f, 1);
        }
    }

    public void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().sprite = mapNode.inactiveSprite;

        if (ifExplored)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        transform.localScale = new Vector3(0.1f, 0.1f, 1);

    }

    public void OnMouseDown()
    {
        if (!ifApproachable)
        {
            return;
        }
        else
        {
            ifExplored = true;
        }

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
                EnterCamp();
                break;
            case NodeType.Merchant:
                EnterMerchant();
                break;
            case NodeType.Uncertainty:
                break;
        }

        ActiveNextLevelNode();
        ResetCurrentLevelNode();
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

    public void EnterMerchant()
    {
        mapM.gM.merM.OnClickMapNode_Merchant();    
    }

    public void EnterCamp()
    {
        mapM.gM.campM.OnClickMapnode_Camp();
    }

    public void ResetCurrentLevelNode()
    {
        for(int i = 0; i < mapM.allLayerNodes[step].Count; i++)
        {
            mapM.allLayerNodes[step][i].GetComponent<MapNodeManager>().ResetToInactive();
        }
    }

    public void ResetToInactive()
    {
        ifApproachable = false;
        transform.localScale = new Vector3(0.1f, 0.1f, 1);

        if (ifExplored)
        {
            GetComponent<SpriteRenderer>().color = Color.black;

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void ActiveNextLevelNode()
    {
        for (int i = 0; i < linkTargetCount; i++)
        {
            mapM.allLayerNodes[step + 1][linkTargetIndexList[i]].GetComponent<MapNodeManager>().ifApproachable = true;
        }
    }

    public void ScaleChangeWhenActive(float change)
    {
        float scaleChange = ScaleFunction(change);
        transform.localScale = new Vector3(scaleChange, scaleChange, 1);
    }

    public float ScaleFunction(float angle)
    {
        return (Mathf.Cos(angle) + 2) * 0.1f;
    }

    private void Update()
    {
        if (ifApproachable)
        {
            time += Time.deltaTime;
            ScaleChangeWhenActive(scaleChangeSpeed * time);
        }
    }
}
