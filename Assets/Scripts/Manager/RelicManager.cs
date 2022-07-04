using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RelicManager : MonoBehaviour
{
    public GameMaster gM;
    public List<RelicInfo> relicPool;
    public List<RelicInfo> activeRelics = new List<RelicInfo>();

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void RelicEffectApply(RelicEffectType effectType)
    {
        List<RelicEffectType> activeEffects = new List<RelicEffectType>();
        foreach (RelicInfo info in activeRelics)
        {
            activeEffects.Add(info.effectType);
        }
        if (activeEffects.Contains(effectType))
        {
            switch (effectType)
            {
                case RelicEffectType.HandCardDrawAmountPlus:
                    gM.deckM.drawCardAmount = 4;
                    break;
                case RelicEffectType.PlayerDmgPlus:
                    gM.buffSM.valueToCalculate += 1;
                    break;
                case RelicEffectType.HpRegenerationOnMapMove:
                    if (gM.characterM.mainCharacter!=null)
                    {
                        gM.characterM.mainCharacter.HealSelf(3);
                    }
                    break;
            }
        }
    }

    public List<RelicInfo> FindUnobtainedRelics()
    {
        List<RelicInfo> unobtainedRelics = new List<RelicInfo>();
        foreach (RelicInfo info in relicPool)
        {
            if (!activeRelics.Contains(info))
            {
                unobtainedRelics.Add(info);
            }
        }
        //List<RelicInfo> unobtainedRelics = relicPool.Union(activeRelics).ToList(); //剔除重复项
        //List<RelicInfo> unobtainedRelics = relicPool.Concat(activeRelics).ToList(); //保留重复项
        //List<RelicInfo> unobtainedRelics = relicPool.Where(a => activeRelics.Contains(a)!).ToList();
        return unobtainedRelics;
    }
}
