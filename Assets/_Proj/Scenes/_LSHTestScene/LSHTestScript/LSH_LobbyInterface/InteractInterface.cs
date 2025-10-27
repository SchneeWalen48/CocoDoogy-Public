using UnityEngine;

public interface IInteractable
{
    void OnInteract();
}

public interface IDraggable
{
    void OnBeginDrag(Vector3 position);
    void OnDrag(Vector3 position);
    void OnEndDrag(Vector3 position);
}

public interface ILongPressable
{
    void OnLongPress();
}

