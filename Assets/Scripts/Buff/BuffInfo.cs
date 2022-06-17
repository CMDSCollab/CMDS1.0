using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffBelonging
{
    Character,
    Enemy
}

public class BuffInfo : ScriptableObject
{
    public CharacterBuff characterBuffType;
    public EnemyBuff enemyBuffType;
    public GameObject uiObj;
    public BuffTimeType timeType;
    public int lastTime;
    public BuffValueType valueType;
    public int value;
    public bool isReadyToRemove = false;
    public BuffSource source;
}
