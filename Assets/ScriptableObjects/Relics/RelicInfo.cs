using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelicEffectType
{
    HandCardDrawAmountPlus,
    PlayerDmgPlus,
    HpRegenerationOnMapMove
}

[CreateAssetMenu(fileName = "New Relic", menuName = "Scriptable Object/New Relic")]
public class RelicInfo : ScriptableObject
{
    public string relicName;
    public Sprite relicImage;
    [TextArea(2,5)]
    public string description;
    public RelicEffectType effectType;
    public int price;
}
