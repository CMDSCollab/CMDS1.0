using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Minion, Elite,  Boss}

public enum MinionType { TechNerd, ESPlayerMature, Hatchling, TheHoarder, Mc}

public enum EliteType { Streamer,SpeedRunner, Ea, Eb, Ec}

public enum BossType { JosefFames, Ba, Bb, Bc}

[System.Serializable]
public struct EnemyIntentionRatio { public EnemyIntention intention; [Range(0, 100)] public int tendency; public int skillUp; }

public enum EnemySpecialIntention { MultipleAtk, Revive,FireShoot,HoneyShoot}

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Object/Enemy")]
public class EnemyInfo : ScriptableObject
{
    public EnemyType enemyType;
    public MinionType minionType;
    public BossType bossType;
    public EliteType eliteType;

    public string enemyName;
    public List<Sprite> images;
    public int maxHealth;
    public int defaultSkill;
    public List<EnemyIntentionRatio> basicIntentions;
    public List<EnemySpecialIntention> specialFunctions;
}
