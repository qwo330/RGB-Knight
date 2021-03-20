using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EBossState
{
    Phase1,
    Phase2,
    Phase3,
}

public class Boss : Monster
{
    Vector3 _lookDIr;
    Vector3 _leftScale = new Vector3(-1, 1, 1);
    Vector2 _rightScale = new Vector3(1, 1, 1);

    EBossState _eBossState;

    Animator _animator;
    Actor _player;
    GameObject _attackCollider; // 공격용 콜라이더
    BossPhase _currentPhase;

    //public UnityAction

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
        _animator = GetComponentInChildren<Animator>();
        Init();
    }

    public new void Init()
    {
        _HP = _MaxHP;
        ChangeState(EBossState.Phase1);
        ChangePhase();

        _lookDIr = Vector2.left;
        _currentPhase = new BossPhase01(this, _animator);
        _nextAttackTime = Time.time;
    }

    void Update()
    {
        _currentPhase.Action();
    }

    //public override void Move()
    //{
    //    Trace();
    //}

    public override void Attacked()
    {
        _HP -= 1;

        if (_HP <= 0)
        {
            Dead();
        }
    }

    public override void Dead()
    {
        // todo : 사망처리
    }



    #region Phase 1
    new void Attack()
    {
        _animator.SetTrigger("Attack");
        if (_isParring)
        {
            _animator.SetBool("Parring", true);
        }
        else
        {
            _animator.SetBool("Parring", false);
        }

        _nextAttackTime = Time.time + 0.5f;

    }

    void SummonHand()
    {
        GameObject preb = Resources.Load<GameObject>("SummonHand");
        GameObject go = Instantiate(preb);

    }
    #endregion Phase 1


    #region Phase 2


    #endregion Phase 2


    #region Phase 3


    #endregion Phase 3

    public void ChangeState(EBossState nextState)
    {
        _eBossState = nextState;
    }

    public void ChangePhase()
    {
        // todo : 다음 패턴으로 교체
        Util.Log("Change Phase");
        if (_eBossState == EBossState.Phase1)
        {
            _currentPhase = new BossPhase01(this, _animator);
        }
        else if (_eBossState == EBossState.Phase2)
        {
            _currentPhase = new BossPhase02(this, _animator);
        }
        else if (_eBossState == EBossState.Phase3)
        {
            _currentPhase = new BossPhase03(this, _animator);
        }
    }

    public float GetNextAttackTime()
    {
        return _nextAttackTime;
    }

    public void SetNextAttackTime(float time)
    {
        _nextAttackTime = time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Util.Log("boss collider enter");

    }
}

public abstract class BossPhase : MonoBehaviour
{
    protected Boss _boss;
    protected Animator _animator;
    protected int combo = 0;

    public abstract void Action();

    public virtual void Pattern1()
    {
        Util.Log("Pattern1");
        combo++;
    }

    public virtual void Pattern2()
    {
        Util.Log("Pattern2");
        combo++;
    }

    public virtual void Pattern3()
    {
        Util.Log("Pattern3");
        combo++;
    }

    public virtual void Pattern4()
    {
        Util.Log("Pattern4");
        combo = 0;
    }
}

public class BossPhase01 : BossPhase
{
    public BossPhase01(Boss boss, Animator animator)
    {
        _boss = boss;
        _animator = animator;
        combo = 0;
        _boss._power = 1;
    }

    public override void Action()
    {
        if (_boss.IsPlayerInSight() == false)
            return;

        if (_boss.GetNextAttackTime() < Time.time)
        {
            /*if (combo == 4)
            {
                Pattern4();
                combo = 0;
            }
            else*/ if (_boss._HP < _boss._MaxHP * 0.67f)
            {
                Pattern3();
                _boss.ChangeState(EBossState.Phase2);
                _boss.ChangePhase();
            }
            else
            {
                combo++;

                if (true)//_boss.IsPlayerInAttackRange())
                {
                    Pattern1();
                }
                else
                {
                    Pattern2();
                }
            }
        }
        else
        {
            _boss.Trace();
        }
    }

    public override void Pattern1()
    {
        base.Pattern1();
        _boss.SetNextAttackTime(Time.time + 0.5f);
        _animator.SetTrigger("Attack01");
    }

    public override void Pattern2()
    {
        base.Pattern2();
        _boss.SetNextAttackTime(Time.time + 1f);
        _animator.SetTrigger("SummonHand");
    }

    public override void Pattern3()
    {
        base.Pattern3();
        _boss.SetNextAttackTime(Time.time + 2f);
        _animator.SetTrigger("DownAttack");
    }

    public override void Pattern4()
    {
        base.Pattern4();
    }
}

public class BossPhase02 : BossPhase
{
    public BossPhase02(Boss boss, Animator animator)
    {
        _boss = boss;
        _animator = animator;
        combo = 0;
        _boss._power = 2;
    }

    public override void Action()
    {
        if (_boss.IsPlayerInSight() == false)
            return;

        if (_boss.GetNextAttackTime() < Time.time)
        {
            /*if (combo == 4)
            {
                Pattern4();
                combo = 0;
            }
            else*/ if (_boss._HP < _boss._MaxHP * 0.33f)
            {
                Pattern3();
                _boss.ChangeState(EBossState.Phase3);
                _boss.ChangePhase();
            }
            else
            {
                combo++;

                if (_boss.IsPlayerInAttackRange())
                {
                    Pattern1();
                }
                else
                {
                    Pattern2();
                }
            }
        }
    }

    public override void Pattern1()
    {
        base.Pattern1();
        _boss.SetNextAttackTime(Time.time + 0.5f);
    }

    public override void Pattern2()
    {
        base.Pattern2();
        _boss.SetNextAttackTime(Time.time + 1f);
    }

    public override void Pattern3()
    {
        base.Pattern3();
        _boss.SetNextAttackTime(Time.time + 2f);
        _animator.SetTrigger("MassiveAttack");
    }

    public override void Pattern4()
    {
        base.Pattern4();
    }
}

public class BossPhase03 : BossPhase
{
    public BossPhase03(Boss boss, Animator animator)
    {
        _boss = boss;
        _animator = animator;
        combo = 0;
        _boss._power = 2;
    }

    public override void Action()
    {
        if (_boss.GetNextAttackTime() < Time.time)
        {
            if (combo == 4)
            {
                Pattern4();
                combo = 0;
            }
            else
            {
                combo++;

                if (_boss.IsPlayerInAttackRange())
                {
                    Pattern1();
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        Pattern2();
                    }
                    else
                    {
                        Pattern3();
                    }
                }
            }
        }
    }

    public override void Pattern1()
    {
        base.Pattern1();
        _boss.SetNextAttackTime(Time.time + 0.3f);
    }

    public override void Pattern2()
    {
        base.Pattern2();
        _boss.SetNextAttackTime(Time.time + 0.6f);
    }

    public override void Pattern3()
    {
        base.Pattern3();
        _boss.SetNextAttackTime(Time.time + 1f);
        _animator.SetTrigger("DownAttack");
    }

    public override void Pattern4()
    {
        base.Pattern4();
        _boss.SetNextAttackTime(Time.time + 1f);
        _animator.SetTrigger("MassiveAttack");
    }
}


/*
 * 
 * 보스 몬스터
페이즈
체력에 따라 변경되고
몬스터 공격 + 이동 속도에 맞춰 난이도 조절

1페 : 무기 없이(피격 당 데미지 1)
1 패턴 : 단순 손 휘두르기(패링용)
2 패턴 : 바닥에서 손 튀어나오기(회피용)

페이즈 변경기 : 땅 내려찍기 후 딜레이 + 페이즈 변경

2페 : 무기 1개(피격 당 데미지 2)
1 패턴 : 단순 칼 휘두르기(패링용) + 데미지 업
2 패턴 : 돌진(회피)
돌진 후 딜레이 -> 보스 전투 맵을 한정 -> 벽에 박으면 스턴

페이즈 변경기 : 가로로 베는 모션 여러번 후에 페이즈 변경
포효 사운드 + 모션 추가

3페 : 쌍검 (속도 상승)
1 패턴 : 빠른 공격 여러번(연속 패링)
2 패턴 : 돌진(스턴 x) or 연속 돌진
3 패턴 : 땅 내려찍기 후 딜레이(짧게)
4 패턴 : 가로로 베는 모션 여러번
5 패턴 : 가능하면 구상해서 추가하기 =====

 * */
