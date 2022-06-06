using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypePro {Normal, Debug}
public enum SpecialFunctionPro
{
    None,
    DamageEqualsShield,
    DoubleShield,
    UseHandCardsGainShield,
    DiscardAllHandCard,
    Vengeance,
    ConsumeShieldDoubleDamage
}
public enum DebugType
{
    None,
    Repass,
    TryParse,
    GarbageCollect,
    NewThread,
    DefaultValue,
    GetSet
}

[CreateAssetMenu(fileName = "New Programmer Card", menuName = "Scriptable Object/New Programmer Card")]
public class CardInfoPro : CardInfo
{
    [Header("Programmer Area")]
    public CardTypePro cardType;
    public DebugType debugType;
    [Tooltip("���ƶ���������Ӱ�죨����3��ʾ����3�㣬-2��ʾ����2�㣩")]
    public int codeRedundancy;
    public SpecialFunctionPro proSpecialFunction;
}


