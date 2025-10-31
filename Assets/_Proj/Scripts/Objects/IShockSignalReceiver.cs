using UnityEngine;

public interface IShockSignalReceiver
{
    // 충격파 신호를 전달받기
    void ReceiveShockSignal(Vector3 origin, float strength, long token);
}