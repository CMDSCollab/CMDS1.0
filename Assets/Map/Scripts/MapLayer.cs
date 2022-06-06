using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��MapConfig��scriptable obj�������ã���Ϸһ�ؿ���16�㣬���ڹ滮ÿһ����ֵ�Node���ͣ����ʣ�λ�ù�ϵ��
[System.Serializable]
public class MapLayer
{
    [Tooltip("�ò���Ҫ���ֵ�Node����")]
    public NodeType nodeType;

    [Tooltip("�ò���ܻ���ֵ�Node����")]
    public NodeType[] possibleNodeType;

    [Tooltip("�ò���ܻ���ֵ�Node����")]
    public int possibleEmergeNodesCount;

    [Tooltip("�ò������������Node�ĸ���")]
    [Range(0f, 1f)]
    public float randomizeNodesType;
}
