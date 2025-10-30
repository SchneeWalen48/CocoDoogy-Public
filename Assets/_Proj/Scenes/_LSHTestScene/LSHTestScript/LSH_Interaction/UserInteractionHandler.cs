using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 일단 먼저 해볼 것이 클릭 시 특정 애니메이션 동작
/// 결국 드래그는 클릭 1초 후 실행 되는 것 이니 bool 변수는 1개만 있어도?
/// </summary>
/// 

public class UserInteractionHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ILobbyInteractable interactable;
    private ILobbyDraggable draggable;
    private ILobbyPressable longPressable;

    private bool isPressing = false;
    private bool isDragging = false;
    private float pressTime = 0f;
    private Vector3 startPos;


    private void Awake()
    {
        interactable = GetComponent<ILobbyInteractable>();
        draggable = GetComponent<ILobbyDraggable>();
        longPressable = GetComponent<ILobbyPressable>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPressing) return;
        draggable?.OnLobbyBeginDrag(eventData.position);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPressing) return;
        //Vector3 worldPos = eventData.pointerCurrentRaycast.worldPosition;
        draggable?.OnLobbyDrag(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPressing) return;
        //Vector3 setPos = eventData.pointerCurrentRaycast.worldPosition;
        //transform.position = setPos;
        draggable?.OnLobbyEndDrag(eventData.position);
        isDragging = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPressing || isDragging) return;
        interactable.OnLobbyInteract();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        pressTime = Time.time;
        StartCoroutine(CheckLongPress());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }

    private IEnumerator CheckLongPress()
    {
        while (isPressing)
        {
            if (Time.time - pressTime >= 0.15f)
            {
                longPressable?.OnLobbyPress();
                isPressing = false;
            }
            yield return null;
        }
    }
}
