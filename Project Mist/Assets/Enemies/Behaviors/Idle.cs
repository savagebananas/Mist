using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    // Behavior Params
    [SerializeField] float aggroDistance = 15;
    [SerializeField] float attackDistance = 2f;

    // General
    EnemyBase enemy;
    NavMeshAgent agent;
    GameObject player;

    // Animation
    Animator animator;

    // Next State
    [SerializeField] State followPlayer;
    [SerializeField] State meleeAttackState;

    private void Awake()
    {
        enemy = parent.GetComponent<EnemyBase>();
        agent = parent.GetComponent<NavMeshAgent>();
        animator = parent.GetComponent<Animator>();
        player = PlayerManager.instance.player;
    }

    public override void OnStart()
    {
        animator.SetTrigger("idle");
        agent.isStopped = true;
    }

    public override void OnUpdate()
    {
        var distanceFromPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);
        if (distanceFromPlayer <= attackDistance) stateMachine.SetNewState(meleeAttackState);
        else if (distanceFromPlayer <= aggroDistance) stateMachine.SetNewState(followPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(parent.transform.position, aggroDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(parent.transform.position, attackDistance);
    }

}
