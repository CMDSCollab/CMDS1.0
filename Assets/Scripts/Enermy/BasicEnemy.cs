using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#region �������˽�ɫ˵��
// 1. ���ȣ��½�һ���ű�������EM��EE��EBΪǰ׺����׺Ϊenemy���֣����»��� _ ���ӣ�eg��EB_JosefFames���ýű��̳�BasicEnemy
// 2. ���ݸ�enemy��Ϣ��ʵBuffManager��EnemyBuff�⣨���Enemyû����Ҫ����Buff������һ����
//    2.1 �����ڽű��е�EnemyBuffö�����Buff����
//    2.2 Ȼ���ڳ�����BuffManager��Inspector�ϵ�EnemyBuffs���list�У���Ӹ�buff�ļ�¼�Ͷ�Ӧ��BuffImage��icon�زĿ���ȥhttps://game-icons.net/��
// 3. ����EnemyInfo�ű��е�MinionType��EliteType��BossType������Ӷ�Ӧ���½�ɫ
// 4. ���ɫ��������ͼ�����ڴ�EnemyMaster��EnemyIntention��ӣ�����Ҫȥ�����ڵ�EnemyMaster�������Intention������Image
// 4. �����EnemyMaster�е�EnemyObjAddComponent��������Ӿ����SwitchCase����addComponent�ı�д
// 5. �½�Enemy��Scriptable obj���趨��ز�����������scriptable obj���ص�������EnemyManager��TestEnemy��
//    5.1 ͼƬ��������ң����ǵ���������
//    5.2 Intention��Tendency��ֵ����������100
// 6. �����½���Enemy�ű������ݽ��и��ģ�����ο�EM_ESPlayerMature
#endregion

public enum MagicCircleState { In, Out }

public class BasicEnemy : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;

    public int maxHp;
    public int healthPoint;
    public int enemyLv; //���˵ĵȼ��Ѷ� ��Ӱ�쵽ս���������reward����
    public bool isDefeated;//����ÿ֡����
    public EnemyInfo enemyInfo;
    public EnemyIntention currentIntention;

    public Text enemyName;
    public Image portrait;
    public Slider hpBar;
    public Text hpRatio;
    public MagicCircleState magicCircleState = MagicCircleState.In;

    #region ���ʦ��ر���
    public int skillLv;
    #endregion

    public virtual void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void Update()
    {
        if (healthPoint<=0 && !isDefeated)
        {
            isDefeated = true;
            EnemyDefeated();
        }
    }

    public virtual void InitializeEnemyUI()
    {
        enemyName = transform.Find("Name").GetComponent<Text>(); 
        portrait = transform.Find("Portrait").GetComponent<Image>();
        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpRatio = transform.Find("HpBar").Find("HpRatio").GetComponent<Text>();

        portrait.sprite = enemyInfo.images[0];
        enemyName.text = enemyInfo.enemyName;
        maxHp = enemyInfo.maxHealth;
        healthPoint = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();

        gM.cEffectSM.EnterCardState(gM.cEffectSM.skillCState, enemyInfo.defaultSkill);

        GenerateEnemyIntention();
    }

    public virtual void EnemyDefeated()
    {
        gM.uiCanvas.transform.Find("RewardPanel").GetComponent<RewardPanel>().SetRewardPanel(enemyLv);
        gM.uiCanvas.transform.Find("RewardPanel").gameObject.SetActive(true);
    }

    public virtual void TakeAction()
    {

    }

    public virtual void TakeDamage(int dmgValue)
    {
        healthPoint -= dmgValue;
    }

    //������ʵ�˺����������м���buff
    public void TakeTrueDamage(int dmg)
    {
        healthPoint -= dmg;
    }

    #region Intention
    public virtual void GenerateEnemyIntention()
    {
        List<int> tendencyValues = new List<int>();
        foreach (EnemyIntentionRatio ratio in enemyInfo.basicIntentions)
        {
            tendencyValues.Add(ratio.tendency);
        }
        int random = Random.Range(0, 100);
        for (int i = 0; i < tendencyValues.Count; i++)
        {
            if (random < tendencyValues[i])
            {
                currentIntention = enemyInfo.basicIntentions[i].intention;
                break;
            }
            else
            {
                random -= tendencyValues[i];
            }
        }
        //EnemyIntention lastIntention = currentIntention;
        //Debug.Log(currentIntention);
        //do
        //{
        //    int random = Random.Range(0, 100);
        //    for (int i = 0; i < tendencyValues.Count; i++)
        //    {
        //        if (random < tendencyValues[i])
        //        {
        //            currentIntention = enemyInfo.basicIntentions[i].intention;
        //            break;
        //        }
        //        else
        //        {
        //            random -= tendencyValues[i];
        //        }
        //    }
        //} while (lastIntention == currentIntention);
        SetIntentionUI();
    }

    public void SetIntentionUI()
    {
        Sprite imageToSet = null;
        foreach (EnemyIntentionImages intentionImage in gM.enM.intentionImages)
        {
            if (intentionImage.enemyIntention == currentIntention)
            {
                imageToSet = intentionImage.image;
            }
        }
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Attack";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Defence:
                transform.Find("Intention").Find("Value").gameObject.SetActive(true);
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Defence";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Heal:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Heal";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Taunt:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Taunt";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Charge:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Charge";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Block:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Block";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.HoneyShoot:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "HoneyShoot";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.FireShoot:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "FireShoot";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Comment:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Comment";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.ToComment:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "ToComment";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Sleep:
                transform.Find("Intention").Find("Value").gameObject.SetActive(false);
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Sleep";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
        }
    }
    #endregion

    #region Magic Circle
    //���ʦMC�߼������� ���ʦ��cha �� ����skillLv ���߽��бȽ� diff = cha - skillLv
    //�� diff > 10 ʱ�����˻��anxiety ������MC�ж� 30%����
    //��10 > diff > 0ʱ�����˻��MC ����ÿ�غϳ����������ʦ��cha�˺�
    //��diff < 0 ʱ�����˻��bored ������MC�ж� 60%����
    //�����˴���MCʱ��inflow����������������ˡ�

    //����ԳMC�߼�����currentErrors.Count >= 2ʱ����ʼ����MC���ж�
    //�� Count = 2ʱ��MC�ж�ÿ�غ�10%���� 
    //�ڴ�֮�ϣ�Countÿ��1��MC�������ʸ�10%�����6��BUG����������50%��������¼���ֵ��highestDropWeight��
    //ֻ������Count��0ʱ���������ʲŻ����ã�ͬʱRecapture��������ȡ���ֵ��������6��BUG����1��BUG������������ȻΪ50%��
    private int highestDropWeight = 0;

    public void MainChaMCChange()
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                int chaLv = gM.aiM.des.challengeLv;
                int chaSubtractSkill = chaLv - skillLv;
                int skillSubtractCha = skillLv - chaLv;
                if (skillSubtractCha > 10)
                {
                    gM.buffM.SetBuff(EnemyBuff.InFlow, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Bored, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    MagicCircleDropOut(30);
                }
                if (chaSubtractSkill <= 10  && skillSubtractCha <= 10)
                {
                    gM.buffM.SetBuff(EnemyBuff.InFlow, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Bored, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    MagicCirleRecapture();
                }
                if (chaSubtractSkill > 10)
                {
                    gM.buffM.SetBuff(EnemyBuff.InFlow, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Bored, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    MagicCircleDropOut(60);
                }
                break;
            case CharacterType.Programmmer:
                Programmer programmer = gM.aiM.pro;
                int dropWeight = 0;
                // ��highestDropWeight == 0��˵���ո����ã���ʱֻҪ���Ƿ�Count>=2������¼���ֵ
                if (highestDropWeight == 0)
                {
                    if (programmer.currentErrors.Count >= 2)
                    {
                        dropWeight = 10 * programmer.currentErrors.Count;
                        highestDropWeight = dropWeight;
                    }
                }
                else
                {
                    // �����ж�Count�Ƿ��Ѿ�����0���������������ã������¹���MC
                    if (programmer.currentErrors.Count == 0)
                    {
                        dropWeight = 0;
                        highestDropWeight = 0;
                        MagicCirleRecapture();
                    }
                    // ���Count>0����Ƚ�һ�µ�ǰֵ�����ֵ����¼���ֵ�����ֵ����dropWeight
                    else
                    {
                        dropWeight = 10 * programmer.currentErrors.Count;
                        if (dropWeight >= highestDropWeight)
                        {
                            highestDropWeight = dropWeight;
                        }
                        else
                        {
                            dropWeight = highestDropWeight;
                        }
                    }
                    
                }
                // ����MC�����ж�
                MagicCircleDropOut(dropWeight);
                //Debug.Log("��ǰMC�������ʣ�" + dropWeight);
                break;
            case CharacterType.Artist:
                break;
        }
    }

    public void MagicCircleDropOut(int weight)
    {
        int dice = Random.Range(0, 100);
        if (dice < weight)
        {
            magicCircleState = MagicCircleState.Out;
            transform.Find("MagicCircle").gameObject.SetActive(false);
            gM.buffM.SetBuff(EnemyBuff.Weak, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
            gM.buffM.SetBuff(EnemyBuff.Vulnerable, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
        }
    }

    public void MagicCirleRecapture()
    {
            magicCircleState = MagicCircleState.In;
            transform.Find("MagicCircle").gameObject.SetActive(true);
            gM.buffM.SetBuff(EnemyBuff.Weak, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
            gM.buffM.SetBuff(EnemyBuff.Vulnerable, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
    }

    public void MagicCirleStateControl(string pro)
    {
       
    }

    public void MagicCirleStateControl(float art)
    {

    }
    #endregion
}
