using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMonsterState
{
    Idle,
    Roam,
    Trace,
    Attck,
    Dead,
    Max,
}

public class Monster : Actor
{
    public float _SightRange = 1f;
    public float _AttackRange = 1f;

    [SerializeField]
    protected float _AttackTerm = 5f;
    protected float _nextAttackTime;
    public float _power = 1f;

    public float _WallRay = 1f;
    public float _GroundRay = 2f;

    EMonsterState _eEnemyState;

    //[SerializeField] float _RoamingRange = 5f;
    [SerializeField] bool ShowGizmo = true;

    Vector3 _lookDIr;
    Vector3 _leftScale = new Vector3(-1, 1, 1);
    Vector2 _rightScale = new Vector3(1, 1, 1);

    //float _leftValue, _rightValue;

    Animator _animator;
    Actor _player;
    GameObject _attackCollider; // 공격용 콜라이더

    protected bool _isParring = false;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
        _animator = GetComponentInChildren<Animator>();
        Init();
    }

    public void Init()
    {
        _HP = _MaxHP;
        ChangeState(EMonsterState.Roam);

        //float tx = transform.position.x;
        //_leftValue = tx - _RoamingRange;
        //_rightValue = tx + _RoamingRange;

        _lookDIr = Vector2.left;
        _nextAttackTime = Time.time;
    }

    void Update()
    {
        if (_eEnemyState == EMonsterState.Roam)
        {
            if (IsPlayerInSight())
            {
                ChangeState(EMonsterState.Trace);
            }
            else
            {
                if (IsMoveable() == false)
                {
                    ChangeLookDir();
                }

                Move();
            }
        }

        else if (_eEnemyState == EMonsterState.Trace)
        {
            if (IsPlayerInAttackRange())
            {
                ChangeState(EMonsterState.Attck);
            }
            else if (IsPlayerInSight() == false)
            {
                ChangeState(EMonsterState.Roam);
            }
            else
            {
                Trace();
            }
        }

        else if (_eEnemyState == EMonsterState.Attck)
        {
            if (IsPlayerInAttackRange())
            {
                if (_nextAttackTime < Time.time)
                {
                    Attack();
                }
                else
                {
                    // todo : idle
                }
            }
            else
            {
                ChangeState(EMonsterState.Trace);
            }
        }
    }

    public void ChangeState(EMonsterState nextState)
    {
        _eEnemyState = nextState;
    }

    public override void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (_lookDIr.x > 0) // 오른쪽
        {
            transform.localScale = _rightScale;
            moveVelocity = Vector3.right;
        }
        else // 왼쪽
        {
            transform.localScale = _leftScale;
            moveVelocity = Vector3.left;
        }

        transform.position += moveVelocity * Time.deltaTime * _MoveSpeed;
    }

    public void ChangeLookDir()
    {
        _lookDIr = _lookDIr.x > 0 ? Vector3.left : Vector3.right;
    }

    public bool IsMoveable()
    {
        // todo : 레이캐스트를 쏴서 앞에 바닥이 있는지 & 벽이 있는지 -> 이동 가능한지 판단한다.
        Vector3 frontPoint = transform.position + _lookDIr;
        int layerMask = 1 << LayerMask.NameToLayer("Map");

        // 바닥 판정
        RaycastHit2D hit = Physics2D.Raycast(frontPoint, Vector3.down, _GroundRay, layerMask);
        if (hit != null)
        {
            if (hit.collider == null)
            {
                return false;
            }
        }

        // 벽 판정
        hit = Physics2D.Raycast(transform.position, _lookDIr, _WallRay, layerMask);
        if (hit != null)
        {
            var col = hit.collider;
            if (col != null)
            {
                // 벽 등으로 가로막혀 있을 때
                return false;
            }
        }

        return true;
    }

    public virtual void Trace()
    {
        if (_player == null)
            return;

        Util.Log("trace");

        // x로 방향만 판별
        _lookDIr = _player.transform.position - transform.position;
        _lookDIr = _lookDIr.x > 0 ? Vector3.right : Vector3.left;
        Move();
    }

    public void Attack()
    {
        Util.Log("attack");
        _nextAttackTime = Time.time + _AttackTerm;
        _animator.SetTrigger("Attack");
    }

    public bool IsPlayerInAttackRange()
    {
        // todo : 연산량이 많음
        if (_player == null)
        {
            //return false;
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
        }
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        return distance < _AttackRange;
    }

    public bool IsPlayerInSight()
    {
        // todo : 연산량이 많음
        if (_player == null)
        {
            //return null;
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
        }

        float distance = Vector2.Distance(transform.position, _player.transform.position);
        return distance < _SightRange;
    }

    public override void Attacked()
    {
        _HP -= 1;

        if (_HP <= 0)
        {
            Dead();
        }

        // todo : 피격 효과처리
    }

    public void Parrying()
    {
        // todo : 패링 판정처리
        if (true)
        {
            Util.Log("Parring");
            _isParring = true;
        }
        else
        {
            _isParring = false;
        }
    }

    public override void Dead()
    {
        // todo 사망 처리
        ChangeState(EMonsterState.Dead);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Util.Log("mon trigger enter");
    //    if (collision.CompareTag("Player"))
    //    {
    //        Util.Log("find player");
    //        ChangeState(EEnemyState.Trace);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Util.Log("mon trigger exit");
    //    if (collision.CompareTag("Player"))
    //    {
    //        Util.Log("lost player");
    //        ChangeState(EEnemyState.Roam);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Util.Log("mon collider enter");

    }

#if UNITY_EDITOR
    //Vector3 centorPos = Vector3.zero;
    //Vector3 roamingSize = Vector3.zero;
    private void OnDrawGizmos()
    {
        if (ShowGizmo == false)
            return;

        Vector3 pos = transform.position;

        // 레이캐스트
        Vector3 frontPoint = transform.position + _lookDIr * _WallRay;
        Vector3 groundPoint = frontPoint + Vector3.down * _GroundRay;

        Gizmos.DrawLine(pos, frontPoint);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(frontPoint, groundPoint);

        // 시야
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pos, _SightRange);

        // 공격
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, _AttackRange);

        //// 로밍
        //if (centorPos == Vector3.zero)
        //{
        //    centorPos = pos;
        //}

        //roamingSize = new Vector3(_RoamingRange * 2, 1, 0);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(centorPos, roamingSize);
    }
#endif
}
