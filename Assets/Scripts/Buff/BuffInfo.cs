using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffTimeType { Permanent, Temporary }
public enum BuffValueType { NoValue, SetValue, AddValue }
public enum EnemyBuff { Null, Bored, Anxiety, InFlow, Vulnerable, Weak, Instability, WeakMind, Defence, Block, Charge, PartialInvincibility, Revive }
public enum CharacterBuff { Null, Defence, Vengeance, PowerUp, Vulnerable, Weak, Inflammable, IsTeamWork, IsSycn }

[CreateAssetMenu(fileName = "New Buff", menuName = "Scriptable Object/New Buff")]
public class BuffInfo : ScriptableObject
{
    public Sprite image;
    public string buffName;
    [TextArea]
    public string description;
    public CharacterBuff characterBuffType;
    public EnemyBuff enemyBuffType;
    public BuffTimeType timeType;
    public int defaultTime;
    public BuffValueType valueType;
    public int defaultvalue;
}
