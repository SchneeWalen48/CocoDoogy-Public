using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CocoDoogyBehaviour : BaseLobbyCharacterBehaviour
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void MoveRandomPosition()
    {
        Vector3 randomDir = Random.insideUnitSphere * moveRadius;
        randomDir += transform.position;

        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, moveRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public override void OnCocoAnimalEmotion()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCocoMasterEmotion()
    {
        throw new System.NotImplementedException();
    }
}
