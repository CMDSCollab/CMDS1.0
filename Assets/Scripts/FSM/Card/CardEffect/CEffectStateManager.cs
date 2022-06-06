using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEffectStateManager : MonoBehaviour
{
    private GameMaster gM;
    public CardInfo cardInUse;
    public CEffectBaseState currentState;
    public bool isCardEffectRunning = false;
    public int valueToCalculate;
    public bool isUpdate = false;

    public CES_AddEnergy addEnergyState = new CES_AddEnergy();
    public CES_ChangeIntention changeIntentionState = new CES_ChangeIntention();
    public CES_ChallengeC challengeCState = new CES_ChallengeC();
    public CES_SkillC skillCState = new CES_SkillC();
    public CES_DrawCardR DrawCRState = new CES_DrawCardR();
    public CES_AddSlot addSlotState = new CES_AddSlot();
    public CES_MagicCircle magicCircleState = new CES_MagicCircle();

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        //currentState = drawState;
    }

    void Update()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM, valueToCalculate);
        }
    }

    public void EnterCardState(CEffectBaseState state,int effectValue)
    {
        currentState = state;
        currentState.EnterState(gM, effectValue);
    }

    #region Card Effects Apply

    public enum AITarget
    {
        Des,
        Pro,
        Art
    }

    public int baseEffectSequence = 0;
    public int specialEffectSequence = 0;
    public int desEffectSequence = 0;
    public int proEffectSequence = 0;
    public int aetEffectSequence = 0;
    public AITarget aiTarget;

    public void CardEffectsApply(CardInfo cardInfo)
    {
        if (cardInfo.baseFunctions.Count != 0 && baseEffectSequence != cardInfo.baseFunctions.Count)
        {
            //Debug.Log("baseEntered");
            CommonEffectsApply(cardInfo.baseFunctions[baseEffectSequence]);
            baseEffectSequence++;
        }
        else
        {
            if (cardInfo.specialFunctions.Count != 0 && specialEffectSequence != cardInfo.specialFunctions.Count)
            {
                //Debug.Log("specialEntered");
                SpecialEffectsApply(cardInfo.specialFunctions[specialEffectSequence]);
                specialEffectSequence++;
            }
            else
            {
                if (cardInfo is CardInfoDsgn)
                {
                    CardInfoDsgn cardDes = (CardInfoDsgn)cardInfo;
                    if (cardDes.desSpecialFunctions.Count != 0 && desEffectSequence != cardDes.desSpecialFunctions.Count)
                    {
                        DesEffectsApply(cardDes.desSpecialFunctions[desEffectSequence]);
                        desEffectSequence++;
                    }
                    else
                    {
                        isUpdate = false;
                        isCardEffectRunning = false;
                        baseEffectSequence = 0;
                        specialEffectSequence = 0;
                        desEffectSequence = 0;
                        gM.cardSM.discardType = DiscardType.InUse;
                        gM.cardSM.EnterCardState(gM.cardSM.discardState);
                        //Debug.Log("end");
                    }
                }
                else if(cardInfo is CardInfoPro)
                {
                    CardInfoPro cardPro = (CardInfoPro)cardInfo;
                }
                else if (cardInfo is CardInfoArt)
                {
                    CardInfoArt cardArt = (CardInfoArt)cardInfo;
                }
            }
        }
    }

    public void CommonEffectsApply(BaseFunction baseFunction)
    {
        switch (baseFunction.functionType)
        {
            case BaseFunctionType.Damage:
                break;
            case BaseFunctionType.Shield:
                break;
            case BaseFunctionType.Heal:
                break;
            case BaseFunctionType.ArtEnergy:
                aiTarget = AITarget.Art;
                EnterCardState(addEnergyState, baseFunction.value);
                break;
            case BaseFunctionType.DsgnEnergy:
                break;
            case BaseFunctionType.ProEnergy:
                aiTarget = AITarget.Pro;
                EnterCardState(addEnergyState, baseFunction.value);
                break;
            case BaseFunctionType.ArtSlot:
                aiTarget = AITarget.Art;
                EnterCardState(addSlotState, baseFunction.value);
                break;
            case BaseFunctionType.DsgnSlot:
                break;
            case BaseFunctionType.ProSlot:
                aiTarget = AITarget.Pro;
                EnterCardState(addSlotState, baseFunction.value);
                break;
            case BaseFunctionType.DrawCard:
                //EnterCardState(DrawCRState, baseFunction.value);
                gM.deckM.DrawCardFromDeckRandomly(baseFunction.value);
                break;
        }
    }

    public void SpecialEffectsApply(SpecialFunctionType specialFunctionType)
    {
        switch (specialFunctionType)
        {
            case SpecialFunctionType.None:
                break;
            case SpecialFunctionType.ArtIntentionChange:
                aiTarget = AITarget.Art;
                EnterCardState(changeIntentionState, valueToCalculate);
                break;
            case SpecialFunctionType.DsgnIntentionChange:
                aiTarget = AITarget.Des;
                EnterCardState(changeIntentionState, valueToCalculate);
                break;
            case SpecialFunctionType.ProIntentionChange:
                aiTarget = AITarget.Pro;
                EnterCardState(changeIntentionState, valueToCalculate);
                break;
            case SpecialFunctionType.DrawSpecificCard:
                break;
            case SpecialFunctionType.Exhausted:
                break;
        }
    }

    public void DesEffectsApply(SpecialDesFunction specialDesFunction)
    {
        switch (specialDesFunction.desFunctionType)
        {
            case SpecialDesFunctionType.None:
                break;
            case SpecialDesFunctionType.ChangeChallenge:
                EnterCardState(challengeCState, specialDesFunction.value);
                break;
            case SpecialDesFunctionType.ChangeSkill:
                EnterCardState(skillCState, specialDesFunction.value);
                break;
            case SpecialDesFunctionType.IsTeamWork:
                gM.buffSM.AddOrAdjustBuff(CharacterBuff.IsTeamWork);
                break;
            case SpecialDesFunctionType.IsSycn:
                gM.buffSM.AddOrAdjustBuff(CharacterBuff.IsSycn);
                break;
            case SpecialDesFunctionType.IsPMH:
                break;
        }
    }

    public void ProEffectsApply(SpecialFunctionPro specialDesFunction)
    {
   
    }

    public void ArtEffectsApply(CardInfoArt cardInfo)
    {

    }
    #endregion
}
