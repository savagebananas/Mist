using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : State
{
    EnemyBase enemy;
    NavMeshAgent agent;

    GameObject player;

    private void Awake()
    {
        enemy = parent.GetComponent<EnemyBase>();
        agent = parent.GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        player = PlayerManager.instance.player;
    }

    public override void OnUpdate()
    {
        agent.SetDestination(player.transform.position);
    }
}
