using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ĵ���Node��Ϣ��¼������sprite��type����MapConfig����
public enum NodeType
{
    None,
    Minion,
    Elite,
    Boss,
    Chest,
    Rest,
    Merchant,
    Uncertainty
}

[CreateAssetMenu(fileName = "Map Node",menuName ="Scriptable Object/Map Node")]
public class MapNode : ScriptableObject
{
    public Sprite iconImage;
    public NodeType nodeType;
}
