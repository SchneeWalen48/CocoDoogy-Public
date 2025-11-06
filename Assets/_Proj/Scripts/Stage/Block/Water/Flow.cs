using System.Collections;
using UnityEngine;
namespace Water
{
    public class Flow : MonoBehaviour
    {
        private Material waterMat;
        //[SerializeField] float flowTime = 10f;
        [SerializeField] float flowInterval = 0.5f; // 오브젝트 밀어내는 간격

        private IFlowStrategy flowStrategy = new FlowWaterStrategy();

        private Vector3 flowDir = Vector3.forward;

        [Tooltip("밀려날 수 있는 오브젝트 레이어")]
        public LayerMask pushableMask;

        private Coroutine flowCoroutine;

        void Awake()
        {
            // FlowWater 블록은 Water 레이어로 설정. 레이어 설정 실수 방지용.
            if (gameObject.layer != LayerMask.NameToLayer("Water"))
            {
                Debug.LogWarning($"{gameObject.name}'s Layer is not 'Water'");
            }
            Debug.Log($"[{gameObject.name}] Awake() 시점 PushableMask 값: {pushableMask.value}");
            MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                waterMat = renderer.material;
            }
        }

        void Start()
        {
            SetFlowDir();
            Debug.Log($"[{gameObject.name}] Start() 시점 PushableMask 값: {pushableMask.value}");
            if (flowCoroutine != null) StopCoroutine(flowCoroutine);
            flowCoroutine = StartCoroutine(FlowObjsCoroutine());
        }

        //public float GetFlowTime()
        //{
        //    return flowTime;
        //}


        public void SetFlowDir()
        {
            // 부모의 Y축 회전값으로 흐름 방향을 계산
            Quaternion parentRot = transform.rotation;
            flowDir = parentRot * Vector3.forward;
            flowDir.y = 0f;
            flowDir.Normalize();
            // KHJ NOTE : 컴포넌트가 root에 붙으므로 transform.rotation으로 변경
            if (waterMat != null)
            {
                waterMat.SetVector("_FlowDir", new(parentRot.x, parentRot.y, parentRot.z, parentRot.w));
            }
        }

        IEnumerator FlowObjsCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(flowInterval);

                Vector3 centre = transform.position;
                
                // 물타일 중앙에서 PushableMask 레이어를 가진 물체를 감지
                float checkSize = 0.45f;
                Collider[] hits = Physics.OverlapBox(centre + Vector3.up * 0.5f, Vector3.one * checkSize, Quaternion.identity, pushableMask);

                foreach (var hit in hits)
                {
                    if (hit.TryGetComponent<PushableObjects>(out var pushable))
                    {
                        flowStrategy.ExecuteFlow(pushable, flowDir);
                    }
                }
            }
        }
    }
}