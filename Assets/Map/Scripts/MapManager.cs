using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������Canvas��Map�ϣ����ڹ������Node�����ɣ�������ֳ�16��layer��װ��¼
public class MapManager : MonoBehaviour
{
    public GameMaster gM;
    public MapConfig mapConfig;
    public GameObject mapNodePrefab;
    public GameObject linePrefab;
    public Dictionary<int, GameObject> singleLayerNodes = new Dictionary<int, GameObject>();
    public Dictionary<int, Dictionary<int, GameObject>> allLayerNodes = new Dictionary<int, Dictionary<int, GameObject>>();

    private int singleLayerNodesCount;
    public float nodePosXStartValue;
    public float nodeHoriMinApartDistance;
    public float nodePosYStartValue;
    public float nodeVerMinApartDistance;

    public List<EnemyInfo> levelEnemyPool;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        GenerateAllLayerNodes();
        LineConnectionSet();
        DrawLine();
    }

    #region MapGeneration
    public void GenerateAllLayerNodes() //��������Node
    {
        //float nodePosYRecord = nodePosYStartValue;
        for (int i = 0; i < mapConfig.mapLayers.Count; i++)
        {
            //����һ��Nodes
            GenerateSingleLayerNodes(mapConfig.mapLayers[i]);
            //�����ò�Nodes��Y�������
            for (int j = 0; j < singleLayerNodes.Count; j++)
            {
                singleLayerNodes[j].transform.position = new Vector3(singleLayerNodes[j].transform.position.x, nodePosYStartValue + i * nodeVerMinApartDistance+ Random.Range(0f,0.5f), -2);
                if (i == mapConfig.mapLayers.Count - 1)
                {
                    singleLayerNodes[j].transform.position = new Vector3(0, nodePosYStartValue + i * nodeVerMinApartDistance + Random.Range(0f, 0.5f), -2);
                }
            }
            //��allLayerNodes(Dic)��¼�ò�nodes��Ϣ����Ϊ�����������Ե����°�װ��Ȼ�����singleLayerNodes���ڰ�װ�²�nodes��Ϣ
            Dictionary<int, GameObject> newDic = new Dictionary<int, GameObject>(singleLayerNodes);
            allLayerNodes.Add(i, newDic);
            singleLayerNodes.Clear();
        }
    }

    public void GenerateSingleLayerNodes(MapLayer targetMapLayer) //���ɵ���Node
    {
        //������ɿ��ܵ�Node������3-5��
        if (allLayerNodes.Count < mapConfig.mapLayers.Count -1) //Boss��ֻ����һ��Node
        {
            singleLayerNodesCount = Random.Range(targetMapLayer.possibleEmergeNodesCount, targetMapLayer.possibleEmergeNodesCount + 2);
        }
        else
        {
            singleLayerNodesCount = 1;
        }
        float nodePosXRecord = nodePosXStartValue;
        for (int i = 0; i < singleLayerNodesCount; i++)
        {
            //�ж����ɵ�Node��Type�ǳ����趨�ĸò�Node�����Ǵ��������������
            NodeType determinedNodeType;
            float judgeAmount = Random.Range(0f, 1f);
            if (judgeAmount <= targetMapLayer.randomizeNodesType)
            {
                int randomSequence = Random.Range(0, targetMapLayer.possibleNodeType.Length);
                determinedNodeType = targetMapLayer.possibleNodeType[randomSequence];
            }
            else
            {
                determinedNodeType = targetMapLayer.nodeType;
            }
            //����Node
            GameObject mapNodeObj = Instantiate(mapNodePrefab);
            //��mapConfig��ȡ�͸�Typeͬ���͵�TypeRef���Ӷ���ö�ӦmapNode����Ϣ��ֵ����Node��MapNodeManager���mapNode���м�¼
            for (int j = 0; j < mapConfig.mapNodes.Count; j++)
            {
                if (mapConfig.mapNodes[j].nodeType == determinedNodeType)
                {
                    mapNodeObj.GetComponent<MapNodeManager>().mapNode = mapConfig.mapNodes[j];
                }
            }
            //���ø�mapNodeObj������е�mapNode�е�ͼƬ��Ϣ��ֵ��Image��Sprite
            mapNodeObj.GetComponent<SpriteRenderer>().sprite = mapNodeObj.GetComponent<MapNodeManager>().mapNode.iconImage;
            //�趨������
            mapNodeObj.transform.SetParent(transform.Find("NodeCollection"));
            //����ˮƽλ�ù�ϵ
            float maxNodeDistance = 2 * (-nodePosXStartValue) / singleLayerNodesCount;
            float randomXDistance = Random.Range(nodeHoriMinApartDistance, maxNodeDistance);
            nodePosXRecord += randomXDistance;
            mapNodeObj.transform.position = new Vector3(nodePosXRecord, 0, 0);
            //��ѭ��������Node��ֵ��singleLayerNodes���dic
            singleLayerNodes.Add(i, mapNodeObj);
        }
 
    }

    public void LineConnectionSet() //ȷ��ÿ����֮������ӹ�ϵ
    {
        //ȷ��ÿ��Node��Ŀ�����ӵ�����
        for (int i = 0; i < allLayerNodes.Count-1; i++)
        {
            int currentLayerNodesCount = allLayerNodes[i].Count;
            int nextLayerNodesCount = allLayerNodes[i + 1].Count;
            if (currentLayerNodesCount < nextLayerNodesCount)
            {
                int extraNodeToLink = nextLayerNodesCount - currentLayerNodesCount;
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    allLayerNodes[i][j].GetComponent<MapNodeManager>().linkTargetCount = 1;
                }
                do
                {
                    int whichNodeToAddExtra = Random.Range(0, currentLayerNodesCount);
                    allLayerNodes[i][whichNodeToAddExtra].GetComponent<MapNodeManager>().linkTargetCount += 1;
                    extraNodeToLink -= 1;
                } while (extraNodeToLink > 0);
            }
            else
            {
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    allLayerNodes[i][j].GetComponent<MapNodeManager>().linkTargetCount = 1;
                }
            }
        }
        //ȷ����Ҫ���ӵĵ���Ϣ
        for (int i = 0; i < allLayerNodes.Count-1; i++)
        {
            int targetNodeIndex = 0;
            int remainder = allLayerNodes[i].Count - allLayerNodes[i + 1].Count;
            List<int> nodesToEmerge = new List<int>();
            bool isNodeToEmerge = false;
            if (remainder > 0)
            {
                for (int j = 0; j < remainder; j++)
                {
                    int random = Random.Range(1, allLayerNodes[i].Count);
                    while (nodesToEmerge.Contains(random) == true)
                    {
                        random = Random.Range(1, allLayerNodes[i].Count);
                    }
                    nodesToEmerge.Add(random);
                }
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                    if (nodesToEmerge.Contains(j) == true)
                    {
                        isNodeToEmerge = true;
                    }
                    else
                    {
                        isNodeToEmerge = false;
                    }
                    if (isNodeToEmerge == true)
                    {
                        mapTemp.linkTargetIndexList.Add(targetNodeIndex - 1);
                    }
                    else
                    {
                        mapTemp.linkTargetIndexList.Add(targetNodeIndex);
                        targetNodeIndex++;
                    }
                }
            }
            else
            {
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                    for (int n = 0; n < mapTemp.linkTargetCount; n++)
                    {
                            mapTemp.linkTargetIndexList.Add(targetNodeIndex);
                            targetNodeIndex++;
                    }
                }
            }
            if (remainder == 0)
            {
               
                    int random = Random.Range(0, allLayerNodes[i].Count);
                    MapNodeManager mapTemp = allLayerNodes[i][random].GetComponent<MapNodeManager>();
                    mapTemp.linkTargetCount++;
                    if (mapTemp.linkTargetIndexList[0] == 0)
                    {
                        mapTemp.linkTargetIndexList.Add(1);
                    }
                    else
                    {
                        mapTemp.linkTargetIndexList.Add(mapTemp.linkTargetIndexList[0]--);
                    }
                
            }
        }
    }

    public void DrawLine() //�ڵ�ͼ����ʱ������Node������
    {
        for (int i = 0; i < allLayerNodes.Count - 1; i++)
        {
            for (int j = 0; j < allLayerNodes[i].Count; j++)
            {
                MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                for (int n = 0; n < mapTemp.linkTargetCount; n++)
                {
                    GameObject newObj = Instantiate(linePrefab, allLayerNodes[i][j].transform);
        
                    LineRenderer line = newObj.GetComponent<LineRenderer>();
                    line.SetPosition(0, newObj.transform.position);
                    line.SetPosition(1, allLayerNodes[i + 1][allLayerNodes[i][j].GetComponent<MapNodeManager>().linkTargetIndexList[n]].transform.position);
                    line.startWidth = 0.01f;
                    line.endWidth = 0.01f;
                }
            }
        }
    }

    public void LinePosReset(float changeAmount) //������������ʱ������ͬ����Ļ��Node�����ߵ�λ����Ϣ
    {
        for (int i = 0; i < allLayerNodes.Count - 1; i++)
        {
            for (int j = 0; j < allLayerNodes[i].Count; j++)
            {
                //MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                //List<LineRenderer> childLinePrefabList = new List<LineRenderer>();
                foreach (LineRenderer lineRenderer in allLayerNodes[i][j].GetComponentsInChildren<LineRenderer>())
                {
                    Vector3 startPos = lineRenderer.GetPosition(0);
                    Vector3 endPos = lineRenderer.GetPosition(1);
                    lineRenderer.SetPosition(0,new Vector3( startPos.x, startPos.y + changeAmount, startPos.z));
                    lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y + changeAmount, endPos.z));
                }
                
            }
        }
    }
    #endregion
}
