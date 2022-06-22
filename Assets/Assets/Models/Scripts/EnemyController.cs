using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private CharacterAnim enemy_Anim;
    private NavMeshAgent navAgent;

    private Transform playerTarget;

    public float move_speed = 3.5f;
    public float attack_distance=1f;

    public float chase_player_after_attack_dist = 1f;

    private float wait_before_attack_time = 3f;
    private float attack_timer;

    private EnemyState enemy_State;

    public GameObject attackPoint;

    private CharacterSound cs;
      void Awake()
    {
        enemy_Anim = GetComponent<CharacterAnim>();
        navAgent = GetComponent<NavMeshAgent>();
        cs=GetComponentInChildren<CharacterSound>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }
    private void Start()
    {
        enemy_State = EnemyState.CHASE;
        attack_timer = wait_before_attack_time;
    }
    void Update()
    {
        if (enemy_State == EnemyState.CHASE)
        {
            ChasePlayer();
        }
        if(enemy_State==EnemyState.ATTACK)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(playerTarget.position);
        navAgent.speed = move_speed;

        if(navAgent.velocity.sqrMagnitude ==0)
        {
            enemy_Anim.Walk(false);
        }
        else
        {
            enemy_Anim.Walk(true);
        }

        if(Vector3.Distance(transform.position,playerTarget.position) <= attack_distance)
        {
            enemy_State = EnemyState.ATTACK;
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        enemy_Anim.Walk(false);

        attack_timer += Time.deltaTime;

        if(attack_timer >wait_before_attack_time )
        {
            if(Random.Range(0,2)>0)
            {
                enemy_Anim.Attack1();
                cs.Attack_1();
            }
            else
            {
                enemy_Anim.Attack2();
                cs.Attack_2();
            }
            attack_timer = 0f;
        }
        if(Vector3.Distance(transform.position,playerTarget.position)> attack_distance+chase_player_after_attack_dist)
        {
            navAgent.isStopped = false;
            enemy_State = EnemyState.CHASE;
        }
    }
    void Activate_AttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void Deactivate_AttackPoint()
    {
        if (attackPoint.activeInHierarchy)
            attackPoint.SetActive(false);
    }

}
