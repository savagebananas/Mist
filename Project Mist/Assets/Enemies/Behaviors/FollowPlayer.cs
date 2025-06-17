using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class FollowPlayer : State
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

    // Audio
    AudioSource audioSource;

    // Next state
    [SerializeField] State idleState;
    [SerializeField] State meleeAttackState;

    private void Awake()
    {
        enemy = parent.GetComponent<EnemyBase>();
        agent = parent.GetComponent<NavMeshAgent>();
        animator = parent.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = PlayerManager.instance.player;
    }

    public override void OnStart()
    {
        agent.isStopped = false; // allow movement
        animator.SetTrigger("run");
        audioSource.Play();
    }

    public override void OnUpdate()
    {
        agent.SetDestination(player.transform.position); // track and go towards player

        var distanceFromPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

        // Attack player when close
        if (distanceFromPlayer <= attackDistance)
        {
            audioSource.Stop();
            stateMachine.SetNewState(meleeAttackState);
        }

        // Idle when too far away
        else if (distanceFromPlayer > aggroDistance)
        {
            audioSource.Stop();
            stateMachine.SetNewState(idleState);
        }


    }
}
