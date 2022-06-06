using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArtStyle 
{
    None,
    Pixel,
    LowPoly,
    ACG,
    LoveCraft,
    LaiZi
}


public enum SpecialArtFunctionType
{
    None,
    TrueDamage,
    GetIncome,
    CostConsistency,
    StyleEffect,//��������
    drawExpandCard //��ȡ��������value����DeckManager��ArtistExpandCard��Ӧ��List index
}

public enum DrawExpandCard 
{
    None,
    ����³��ģ,
    LowPolyСģ��,
    ��������,
    ���ض���֡
}




public enum SpecialArtPassiveEffectType
{
    None,
    ImmuneConsistency
}

[System.Serializable]
public struct drawExpandCardFunction
{
    public DrawExpandCard expandCardName;
    public int times; //�鼸��
}


[System.Serializable]

public struct specialArtFunction
{
    public SpecialArtFunctionType artFunctionType;
    public int value;
}

[System.Serializable]

public struct specialArtPassiveEffect
{
    public SpecialArtPassiveEffectType artPassiveEType;


}

[CreateAssetMenu(fileName = "New Artist Card", menuName = "Scriptable Object/New Artist Card")]

public class CardInfoArt : CardInfo
{

    [Header("Artist Area")]

    public ArtStyle style;
    public List<specialArtFunction> artSpecialFunctions;
    public List<specialArtPassiveEffect> artPassiveEffects;
    public List<drawExpandCardFunction> artDrawExpandCard;
}
