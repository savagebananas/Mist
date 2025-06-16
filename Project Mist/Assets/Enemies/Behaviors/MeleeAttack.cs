using ExternPropertyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : State
{

    // Behavior Params
    [Range(0, 100)]
    [SerializeField] private int damage = 50;
    [SerializeField] Transform center;
    [SerializeField] float radius;

    // General
    EnemyBase enemy;
    NavMeshAgent agent;
    GameObject player;

    // Animation
    Animator animator;
    [SerializeField] AnimationClip attackAnimation;

    // Next State
    [SerializeField] State idleState;

    private void Awake()
    {
        enemy = parent.GetComponent<EnemyBase>();
        agent = parent.GetComponent<NavMeshAgent>();
        animator = parent.GetComponent<Animator>();
        player = PlayerManager.instance.player;
    }

    public override void OnStart()
    {
        agent.isStopped = true; // stop moving
        animator.SetTrigger("attack"); // swing animation
        Invoke(nameof(DamageTick), attackAnimation.length / 2); // damage after half the time
        Invoke(nameof(TransitionToIdleState), attackAnimation.length); // switch states after animation ends
    }

    public override void OnUpdate() { }

    private void DamageTick()
    {
        Collider[] players = Physics.OverlapSphere(center.position, radius, 3); // Layer Mask 3 = player
        foreach (var player in players)
        {
            this.player.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }

    private void TransitionToIdleState()
    {
        stateMachine.SetNewState(idleState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, radius);
    }


}
