using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BaseFunctionType
{ Damage, Shield, Heal, ArtEnergy, DsgnEnergy, ProEnergy, ArtSlot, DsgnSlot, ProSlot, DrawCard }

public enum SpecialFunctionType 
{
    None,
    ArtIntentionChange,
    DsgnIntentionChange,
    ProIntentionChange,
    DrawSpecificCard,
    Exhausted
}

[System.Serializable]
public struct BaseFunction
{
    public BaseFunctionType functionType;
    public int value;
}

public abstract class CardInfo : ScriptableObject
{
    #region ���ƻ�����Ϣ
    [Header("Basic Info")]
    public string cardName;
    [TextArea]
    public string description;
    public List<BaseFunction> baseFunctions;
    public List<SpecialFunctionType> specialFunctions;
    #endregion
}
