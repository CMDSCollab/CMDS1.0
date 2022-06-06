using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ڹ������е�Node�͹ؿ���Ϣ����MapManager����
[CreateAssetMenu(fileName ="New MapConfig",menuName ="Scriptable Object/Map Config")]
public class MapConfig : ScriptableObject
{
    public List<MapNode> mapNodes;
    public List<MapLayer> mapLayers;
}
