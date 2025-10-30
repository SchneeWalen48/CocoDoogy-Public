using UnityEngine;
using UnityEngine.AI;

// 뭘 빼고 뭘 넣어야 이쁠까 
[RequireComponent(typeof(GameObjectData), typeof(UserInteractionHandler), typeof(Draggable))]
public abstract class BaseLobbyCharacterBehaviour : MonoBehaviour, ILobbyInteractable, ILobbyDraggable, ILobbyPressable, ILobbyCharactersEmotion
{
    [Header("NavMeshAgent")]
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] protected float angularSpeed = 120f;
    [SerializeField] protected float acceleration = 8f;
    [Header("Move")]
    [SerializeField] protected float moveRadius = 10f; // 웨이포인트에서 범위
    [SerializeField] protected float waitTime = 2f; // 대기 시간
    [SerializeField] protected EditController editController; // 편집모드


    protected NavMeshAgent agent;
    protected Animator anim;
    protected NavMeshAgentControl charAgent; // Agent 파츠
    protected LobbyCharacterAnim charAnim; // 애니 파츠
    protected Transform trans;
    protected Camera mainCam;
    protected Vector3 originalPos; // 드래그 시 시작 포지션 저장
    protected bool isDragging = false;

    protected int originalLayer; // 평상 시 레이어
    protected int editableLayer; // 편집모드 시 레이어
    protected int currentWaypoinIndex = 0; // 웨이포인트 이동 인덱스
    protected bool isEditMode; // 상태전환을 이걸로 퉁치나?
    protected float yValue; // 생성 시 y축 얻고 드래그 시 해당 값 고정
    protected bool yCaptured = false; // yValue가 값을 얻었는지 판단
    protected int mainPlaneMask;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // agent
        charAgent = new NavMeshAgentControl(agent, moveSpeed, angularSpeed, acceleration, moveRadius, waitTime, trans);
        // charAnim
        charAnim = new LobbyCharacterAnim(anim);
        mainCam = Camera.main;
        if (editController == null) editController = FindFirstObjectByType<EditController>();

        originalLayer = LayerMask.NameToLayer("InLobbyObject");
        editableLayer = LayerMask.NameToLayer("Editable");
        mainPlaneMask = LayerMask.NameToLayer("MainPlaneLayer");
        isEditMode = false; // 상태패턴 전환 시 수정

    }

    protected virtual void OnEnable()
    {
        if (!yCaptured)
        {
            yValue = transform.position.y;
            Debug.Log($"yValue : {yValue}");
            yCaptured = true;
            return;
        }
        currentWaypoinIndex = 0;
    }

    protected virtual void Update()
    {
        bool current = editController.IsEditMode;
        Debug.Log($"current 상태 : {current}");
        if (current != isEditMode)
        {
            isEditMode = current;
            gameObject.layer = isEditMode ? editableLayer : originalLayer;
        }

        if (isEditMode)
        {
            charAnim.StopAnim();
            if (!agent.isStopped) charAgent.AgentIsStop(true);
            //charAgent.EnableAgent(false);
        }
        else
        {
            if (!agent.enabled) charAgent.EnableAgent(true);
            if (agent.isStopped) agent.isStopped = false;
            charAnim.MoveAnim(charAgent.ValueOfMagnitude());
            charAgent.LetsGoCoco(ref currentWaypoinIndex, 0, InLobbyManager.Instance.waypoints);
        }

        //oNMAC.MoveValueChanged();
        //oNMAC.WaitAndMove();
    }

    protected virtual void OnDisable()
    {

    }


    public abstract void OnCocoAnimalEmotion();

    public abstract void OnCocoMasterEmotion();

    public void OnLobbyBeginDrag(Vector3 position)
    {
        if (editController.IsEditMode) return;

        // ���߿� bool ����(���� ĳ���� ���) ����.
        originalPos = transform.position;
        isDragging = true;
        charAnim.StopAnim();
        charAgent.AgentIsStop(true);
        charAgent.EnableAgent(false);
    }

    public void OnLobbyDrag(Vector3 position)
    {
        if (!isDragging) return;
        Ray ray = mainCam.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mainPlaneMask))
        {
            //if (!hit.collider.CompareTag("MainPlane")) return;

            Vector3 pos = hit.point;
            pos.y = yValue; // ��� y ����
            transform.position = pos;
        }
    }

    public void OnLobbyEndDrag(Vector3 position)
    {
        isDragging = false;
        NavMeshHit navHit;
        bool onNavMesh = NavMesh.SamplePosition(transform.position, out navHit, 0.5f, NavMesh.AllAreas);
        if (!onNavMesh) // �߸��� ��ġ�� ����ġ ����
        {
            transform.position = originalPos;
            Debug.Log($"{gameObject.name} : NavMesh ��");
        }
        else
        {
            transform.position = navHit.position;
            Debug.Log($"{gameObject.name} : NavMesh ���� ����");
        }
        charAgent.EnableAgent(true);
        agent.Warp(transform.position);
        currentWaypoinIndex = GetClosestWaypointIndex();
        MoveToNextWaypoint();
    }

    public void OnLobbyInteract()
    {
        if (editController.IsEditMode) return;

        // PointerClick ������ ��
        // �ִϸ��̼� ���, ���� ���� ���� ��
        Debug.Log($"Ŭ��Ŭ��");
        charAnim.PlaySpinAmin();
    }

    public void OnLobbyPress()
    {
        if (editController.IsEditMode) return;

        Debug.Log($"������� ����");
        charAgent.AgentIsStop(true);
        charAnim.StopAnim();
    }

    private int GetClosestWaypointIndex()
    {
        float minDistance = 1000f;
        int closestIndex = 0;

        for (int i = 0; i < InLobbyManager.Instance.waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, InLobbyManager.Instance.waypoints[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }
        return closestIndex;
    }

    private void MoveToNextWaypoint()
    {
        if (InLobbyManager.Instance.waypoints == null || InLobbyManager.Instance.waypoints.Length == 0) return;
        charAgent.AgentIsStop(false);
        charAgent.MoveToPoint(InLobbyManager.Instance.waypoints[currentWaypoinIndex]);

    }
}
