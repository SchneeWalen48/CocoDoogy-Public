using NUnit.Framework;
using System;
using Unity.AI.Navigation;
using UnityEngine;
using System.Collections.Generic;
// Surface���� : ������ ���⿡ �� ���� �ְ����� ���߿� �����ϰڽ���.
[Serializable]
public class  NavMeshSaveData
{
    public List<NavMeshObjectData> nObj = new List<NavMeshObjectData>();
}
[Serializable]
public class NavMeshObjectData
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}
//

public class InLobbyManager : MonoBehaviour
{
    [SerializeField] TestScriptableCharacter[] charDatabase;
    [SerializeField] GameObject plane;
    
    private NavMeshSurface planeSurface;

    public Transform[] waypoints;
    public static InLobbyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        planeSurface = plane.GetComponent<NavMeshSurface>();

    }

    void Start() // ������ ��ŸƮ������ �ΰ����̶� ��ü�ϸ� ��� Enable�� �ϳ�? �κ�Ŵ����� �ΰ��ӱ��� ������ �ʿ�� �����װ�, ���� �ε��ϴ� �Ŵ�.. ����? �ٵ� ���ڵα�� �����ʹ� ��ŸƮ�� �����°� �´°� �ƴұ���
    {
        foreach (var data in charDatabase)
        {
            GameObject obj = Instantiate(data.prefab, waypoints[0].position, Quaternion.identity);
            obj.tag = data.type.ToString();
            obj.layer = LayerMask.NameToLayer("InLobbyObject");
            var meta = obj.GetComponent<GameObjectData>();
            meta.Initialize(data);
        }
    }

    public void NewMap()
    {
        planeSurface.BuildNavMesh();
    }
}
