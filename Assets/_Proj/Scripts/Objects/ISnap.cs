using UnityEngine;

public interface ISnap
{
    // 그리드 크기 1 by 1
    float TileSize { get; }
    // 가장 가까운 그리드 중심 위치로 스냅
    Vector3 Snap(Vector3 worldPos, bool snapY = false);
    void SnapNow(bool snapY = false);
}
