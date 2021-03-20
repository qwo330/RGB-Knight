using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBossState
{

}

public class Boss : Monster
{
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
        _animator = GetComponentInChildren<Animator>();
        Init();
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
