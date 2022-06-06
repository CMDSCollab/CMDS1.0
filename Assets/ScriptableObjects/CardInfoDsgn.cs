using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SpecialDesFunctionType
{
    None,
    ChangeChallenge,
    ChangeSkill,
    IsTeamWork,
    IsSycn,
    IsPMH
}

public enum SpecialPassiveEffectType 
{ 
    None,
    IsTeamWork,
    IsSycn,
    IsPMH
}


[System.Serializable]

public struct SpecialDesFunction
{
    public SpecialDesFunctionType desFunctionType;
    public int value;
}

//[System.Serializable]

//public struct SpecialPassiveEffect 
//{
//    public SpecialPassiveEffectType desPassiveEType;
//}


[CreateAssetMenu(fileName = "New Designer Card", menuName = "Scriptable Object/New Designer Card")]
public class CardInfoDsgn : CardInfo
{
    [Header("Designer Area")]

    public List<SpecialDesFunction> desSpecialFunctions;
    //public List<SpecialPassiveEffect> desPassiveEffects;

}
