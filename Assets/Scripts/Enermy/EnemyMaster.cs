using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyIntention { Attack, Defence, Heal, Taunt, Skill, MultiAttack, Charge, Block, ToComment, Comment, Revive, FireShoot, HoneyShoot, Sleep, Skip}
[System.Serializable]
public struct EnemyIntentionImages
{
    public EnemyIntention enemyIntention;
    public Sprite image;
}

public class EnemyMaster : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;
    public BasicEnemy enemyTarget;
    public GameObject enemyPrefab;
    public List<EnemyIntentionImages> intentionImages;

    private EnemyInfo enemyInfo; //这里的info和basicE里面的info用途的差异在于，这里info是为了根据该info执行addcomponent的方法，没有其他用处
    private GameObject enemyObj;

    public EnemyInfo testEnemy;
    public GameObject flow;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void SetEnemyInfo(EnemyInfo enemyInfoToSet)
    {
        enemyInfo = enemyInfoToSet;
    }

    public void InitializeEnemy()
    {
        enemyObj = Instantiate(enemyPrefab, gM.uiCanvas.transform, false);
        EnemyObjAddComponent(enemyObj);
        enemyObj.transform.SetAsFirstSibling();

        enemyTarget = enemyObj.GetComponent<BasicEnemy>();
        enemyTarget.enemyInfo = enemyInfo;
        InstantiateFlow();
        gM.cEffectSM.EnterCardState(gM.cEffectSM.skillCState, enemyInfo.defaultSkill);
        enemyTarget.enemyLv = (int)enemyInfo.enemyType;
        enemyTarget.InitializeEnemyUI();

        void InstantiateFlow()
        {
            flow = Instantiate(gM.characterM.flowPre);
            flow.GetComponent<FlowManager>().InitializeFlow();
        }
    }

    private void EnemyObjAddComponent(GameObject objToAdd)
    {
        switch (enemyInfo.enemyType)
        {
            case EnemyType.Minion:
                switch (enemyInfo.minionType)
                {
                    case MinionType.TechNerd:
                        objToAdd.AddComponent<EM_TechNerd>();
                        break;
                    case MinionType.ESPlayerMature:
                        objToAdd.AddComponent<EM_ESPlayerMature>();
                        break;
                    case MinionType.Hatchling:
                        objToAdd.AddComponent<EM_Hatchling>();
                        break;
                    case MinionType.TheHoarder:
                        objToAdd.AddComponent<EM_Hoarder>();
                        break;
                    case MinionType.Mc:
                        break;
                }
                break;
            case EnemyType.Elite:
                switch (enemyInfo.eliteType)
                {
                    case EliteType.Streamer:
                        //objToAdd.AddComponent<EE_Streamer>();
                        break;
                    case EliteType.SpeedRunner:
                        objToAdd.AddComponent<EE_SpeedRunner>();
                        break;
                    case EliteType.Eb:
                        break;
                    case EliteType.Ec:
                        break;
                }
                break;
            case EnemyType.Boss:
                switch (enemyInfo.bossType)
                {
                    case BossType.JosefFames:
                        objToAdd.AddComponent<EB_JosefFames>();
                        break;
                    case BossType.Ba:
                        break;
                    case BossType.Bb:
                        break;
                    case BossType.Bc:
                        break;
                }
                break;
        }
    }
}
