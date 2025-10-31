using UnityEngine;

// ISignalSender 대신 임시
public class DoorSignalAdapter : MonoBehaviour
{
    public void OnShock()
    {
        Debug.Log("[DoorSignalAdapter] Shock received -> TODO: ISignalSender로 문 열기 전달");
    }
}
