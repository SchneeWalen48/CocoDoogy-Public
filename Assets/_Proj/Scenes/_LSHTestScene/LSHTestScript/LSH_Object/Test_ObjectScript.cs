using UnityEngine;
using UnityEngine.AI;

public class ObjectScript : MonoBehaviour, IInteractable, IDraggable, ILongPressable
{
    [Header("NavMeshAgent")]
    [SerializeField] float moveSpeed = 3.5f; // 이동 속도
    [SerializeField] float angularSpeed = 120f; // 턴 속도
    [SerializeField] float acceleration = 8f; // 가속도
    [Header("Move")]
    [SerializeField] float moveRadius = 10f; // 이동 범위
    [SerializeField] float waitTime = 2f; // 목표 지점 도달 후 대기 시간

    private NavMeshAgent agent;
    private Animator anim;
    private ObjectAnimationController oAC;
    private ObjectNavMeshAgentController oNMAC;
    private float timer;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        oAC = new ObjectAnimationController(anim);
        oNMAC = new ObjectNavMeshAgentController(agent, moveSpeed, angularSpeed, acceleration, moveRadius, waitTime, timer, transform);
    }

    private void Start()
    {
        oNMAC.MoveToRandomPosition();
    }

    // 아마 로비에 생성되는 순간에는 Enable이 맞을지도?
    //private void OnEnable()
    //{
    //    oNMAC.MoveToRandomPosition();
    //}

    private void Update()
    {
        oNMAC.MoveValueChanged();
        oAC.MoveAnim(oNMAC.ValueOfMagnitude());
        oNMAC.WaitAndMove();
    }

    public void OnBeginDrag(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnLongPress()
    {
        throw new System.NotImplementedException();
    }
}
