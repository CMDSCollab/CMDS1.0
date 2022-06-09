using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EE_Streamer : BasicEnemy
{
    private int recordShieldP;
    private int defaultDmg = 15;
    private int sheildOnComment = 20;

    // �洢��λ��ɫ�ķ��� 0����δ���� 1�������� 2�������
    private Dictionary<CharacterType, int> charToScoreDic;
    private CharacterType characterToComment;
    private bool hasBeenImpressed = false;

    public override void Awake()
    {
        base.Awake();

        sheildOnComment = 20;

        charToScoreDic = new Dictionary<CharacterType, int>();
        charToScoreDic.Add(CharacterType.Designer, 0);
        charToScoreDic.Add(CharacterType.Artist, 0);
        charToScoreDic.Add(CharacterType.Programmmer, 0);
    }

    public override void TakeDamage(int dmgValue)
    {
        // �������в����޵У�Ҳ�����������۵�ʱ��
        if (gM.buffM.FindBuff(EnemyBuff.PartialInvincibility) != null)
        {
            if (true)
            //����˺���Դ��ָ����ɫ�������ܵ��˺�
            {
                base.TakeDamage(dmgValue);
                // ����ƶܣ�������hasBeenImpressed
                if (gM.buffM.FindBuff(EnemyBuff.Defence) != null)
                {
                    if (gM.buffM.FindBuff(EnemyBuff.Defence).value <= 0)
                    {
                        hasBeenImpressed = true;
                    }
                }
            }
            else
            // �����˺�Ϊ0
            {
                //base.TakeDamage(0);
                //gM.buffM.EnemyTakeDamage(0); 

            }
        }
        // ������������ܵ��˺�
        else
        {
            base.TakeDamage(dmgValue);
        }
    }

    public override void TakeAction()
    {
        if (!charToScoreDic.ContainsValue(0))
        {
            TakeDamage(999);
            Debug.Log("������������ɣ�ս������");
        }


        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                //gM.characterM.mainCharacter.TakeDamage(gM.buffM.EnemyAttack( defaultDmg));
                break;
            case EnemyIntention.ToComment:

                gM.buffM.SetBuff(EnemyBuff.Defence, BuffTimeType.Temporary, 1, BuffValueType.AddValue, sheildOnComment,BuffSource.Enemy);
                sheildOnComment += 10;
                
                hasBeenImpressed = false;
                gM.buffM.SetBuff(EnemyBuff.PartialInvincibility, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 1, BuffSource.Enemy);
                break;
                
            case EnemyIntention.Comment:
                if (hasBeenImpressed)
                {
                    gM.characterM.mainCharacter.HealSelf(10);
                    charToScoreDic[characterToComment] = 2;
                    Debug.Log(characterToComment.ToString()+"����˺���");
                }
                else
                {
                    //gM.buffM.SetCharacterBuff(CharacterBuff.Weak, false, 1);
                    gM.buffM.SetBuff(CharacterBuff.Weak, BuffTimeType.Temporary, 1, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    charToScoreDic[characterToComment] = 1;
                    Debug.Log(characterToComment.ToString() + "���������");
                }

                break;
        }
        this.GenerateEnemyIntention();
    }

    public override void GenerateEnemyIntention()
    {

        // ������غ��Ѿ�׼�������ˣ����»غ�����
        if (currentIntention == EnemyIntention.ToComment)
        {
            currentIntention = EnemyIntention.Comment;
        }
        else
        {
            // ��������ѡ���»غ�Intention
            base.GenerateEnemyIntention();
            // ����»غ���׼�����ۣ���ȷ�������۵Ķ���
            if (currentIntention == EnemyIntention.ToComment)
            {
                SelectCharToComment();
            }
        }

        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Value").GetComponent<Text>().text = defaultDmg.ToString();
                break;
            case EnemyIntention.ToComment:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
            case EnemyIntention.Comment:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                break;
        }
        SetIntentionUI();
    }

    private void SelectCharToComment()
    {
        if (!charToScoreDic.ContainsValue(0))
        {
            return;
        }

        bool hasResult = false;
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                if (charToScoreDic[CharacterType.Artist] == 0)
                {
                    characterToComment = CharacterType.Artist;
                    hasResult = true;
                }
                break;
            case 1:
                if (charToScoreDic[CharacterType.Designer] == 0)
                {
                    characterToComment = CharacterType.Designer;
                    hasResult = true;
                }
                break;
            case 2:
                if (charToScoreDic[CharacterType.Programmmer] == 0)
                {
                    characterToComment = CharacterType.Programmmer;
                    hasResult = true;
                }
                break;
        }

        if (!hasResult)
        {
            SelectCharToComment();
        }
    }
}
