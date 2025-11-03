using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AnimalBehaviour : BaseLobbyCharacterBehaviour
{
    [SerializeField] float decoDetectRadius = 10f;
    private Transform targetDeco;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        targetDeco = null;
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        isMoving = true;
        while (isMoving)
        {
            FindNearestDeco();
            if (targetDeco != null)
            {
                charAgent.MoveRandomPosition(targetDeco);
            }
            else
            {
                charAgent.MoveRandomPosition(transform);
            }
            yield return waitU;
        }
    }

    private void FindNearestDeco()
    {
        // 나중에 로비 안에 활성화 된 데코 리스트가 있으면 대체
        GameObject[] decos = GameObject.FindGameObjectsWithTag("Decoration");
        float nearestDist = float.MaxValue;
        Transform nearest = null;

        foreach (GameObject deco in decos)
        {
            float dist = Vector3.Distance(transform.position, deco.transform.position);
            if (dist < decoDetectRadius && dist < nearestDist)
            {
                nearestDist = dist;
                nearest = deco.transform;
            }
        }

        targetDeco = nearest;
    }
    // private void FindNearestDeco()
    // {
    //     // 나중에 로비 안에 활성화 된 데코 리스트가 있으면 대체
    //     GameObject[] decoGObj = GameObject.FindGameObjectsWithTag("Decoration");

    //     float minDistance = 1000f;
    //     int closestIndex = 0;

    //     for (int i = 0; i < decoGObj.Length; i++)
    //     {
    //         float distance = Vector3.Distance(transform.position, decoGObj[i].transform.position);
    //         if (distance < decoDetectRadius && distance < minDistance)
    //         {
    //             minDistance = distance;
    //             closestIndex = i;
    //         }
    //     }
    //     targetDeco = decoGObj[closestIndex].transform;
    // }
    
    // 인터페이스 영역
    public override void OnCocoAnimalEmotion()
    {
        if (!agent.isStopped) agent.isStopped = true;

    }

    public override void OnCocoMasterEmotion()
    {
        
    }

    public override void OnLobbyEndDrag(Vector3 position)
    {
        base.OnLobbyEndDrag(position);
        StartCoroutine(Move());
    }

    public override void OnLobbyInteract()
    {
        base.OnLobbyInteract();
        AudioEvents.Raise(SFXKey.CocodoogyFootstep, pooled: true, pos: transform.position); // 각 동물 소리로
    }

    public override void InNormal()
    {
        base.InNormal();
        StartCoroutine(Move());
    }

    public override void InUpdate()
    {
        
    }

    public override void StartScene()
    {
        
    }

    public override void ExitScene()
    {
        
    }
}
