using UnityEngine;

public interface IPushable
{
    bool RequestPush(Vector3 axis);
    bool IsMoving { get; }
}
