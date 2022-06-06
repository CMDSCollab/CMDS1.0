using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    Designer,
    Programmmer,
    Artist
}

public class BasicCharacter : MonoBehaviour
{
    public GameMaster gM;
    public CharacterInfo characterInfo;

    public void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
        
        // Start()�Ļ������������ǵ�LoadScene������ò�ƻᵼ�¿�ָ��
        // �˴�����ɷ�OnNewGameStarted()�����̳���override��OnNewGameStarted()������base.NewGameStarted()����
    }



    public virtual void OnNewGameStarted()
    {
        gM = FindObjectOfType<GameMaster>();
        gM.cardFunctionM.FunctionBoolValueReset();
    }

    public virtual void OnPlayerTurnEnded()
    {
        
    }
}
