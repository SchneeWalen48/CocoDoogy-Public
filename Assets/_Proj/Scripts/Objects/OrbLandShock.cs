using UnityEngine;

public class OrbLandShock : MonoBehaviour
{
    [Header("Refs")]
    public Shockwave shockwave; // 해당 오브젝트의 Shockwave
    public ShockPing shockPing; // 해당 오브젝트의 ShockPing

    [Header("Ground Probe")]
    public float probeUp = 0.1f; // 레이 시작 높이
    public float probeDown = 1.5f; // 레이 길이(칸 높이 *1~1.5)

    [Header("Cooldown")]
    public float coolTime = 0.6f;

    bool wasGrounded = true;
    float nextShockTime = 0f;
    LayerMask groundLayer;

    void Awake()
    {
        if (!shockwave) shockwave = GetComponent<Shockwave>();
        if (!shockPing) shockPing = GetComponent<ShockPing>();

        // PushableObjects의 groundMask를 그대로 사용
        var po = GetComponent<PushableObjects>();
        groundLayer = po ? po.groundMask : ~0;
    }

    void LateUpdate()
    {
        bool grounded = Physics.Raycast(transform.position + Vector3.up * probeUp, Vector3.down, probeDown, groundLayer);

        // 공중 -> 지상(착지)
        if (!wasGrounded && grounded)
        {Debug.Log("[Orb] Landed -> Shockwave.Fire + ShockPing.PingTowers");

            if (Time.time >= nextShockTime && shockwave != null)
            {
                // 충격파
                shockwave.Fire();

                // 감지탑 통지
                if (shockPing) shockPing.PingTowers(transform.position);
                Debug.Log("[Orb] Landed -> Shockwave.Fire + ShockPing.PingTowers");

                nextShockTime = Time.time + coolTime;
            }
        }
        wasGrounded = grounded;
    }
}
