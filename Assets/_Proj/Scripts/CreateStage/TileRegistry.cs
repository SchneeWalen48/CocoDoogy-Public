using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(menuName = "TileRegistry")]
public class TileRegistry : ScriptableObject
{
    [Serializable] 
    public class TileBinding
    {
        public string code; // "G", "W", "P", "I", "B" ...
        //public GameObject prefab;
        public AssetReferenceGameObject tileRef;
        public bool blocking; // 벽 등
        public bool water; // 물
        public bool ice; // 빙판
        public bool pit; // 구덩이
        public bool swamp; // 늪
    }

    [Serializable]
    public class EntityBinding
    {
        public string type; // "spawn", "goal", "box", "door", "bridge", ...
        //public GameObject prefab;
        public AssetReferenceGameObject entityRef;
    }

    public List<TileBinding> tiles;
    public List<EntityBinding> entities;

    Dictionary<string, TileBinding> tmap;
    Dictionary<string, EntityBinding> emap;

    public void Init()
    {
        if (tmap == null)
        {
            tmap = new();
            foreach(var t in tiles)
            {
                if (!string.IsNullOrEmpty(t.code))
                {
                    tmap[t.code] = t;
                }
            }
        }
        if (emap == null)
        {
            emap = new();
            foreach (var e in entities)
            {
                if (!string.IsNullOrEmpty(e.type))
                {
                    emap[e.type] = e;
                }
            }
        }
    }

    public TileBinding GetTile(string code)
    {
        Init();
        return tmap != null && tmap.TryGetValue(code, out var x) ? x : null;
    }

    public EntityBinding GetEntity(string type) { 
        Init();
        return emap != null && emap.TryGetValue(type, out var x) ? x : null;
    }

    public bool HasCode(string code)
    {
        Init(); 
        return tmap != null && tmap.ContainsKey(code);
    }

    public bool HasEntity(string type)
    {
        Init();
        return emap != null && emap.ContainsKey(type);
    }

    public AsyncOperationHandle<GameObject> LoadTilePrefabAsync(string code)
    {
        Init();
        var binding = GetTile(code);
        if(binding != null && binding.tileRef != null && binding.tileRef.RuntimeKeyIsValid())
        {
            return binding.tileRef.LoadAssetAsync<GameObject>();
        }
        Debug.LogError($"[TR] 타일 프리팹 코드 로드 불가 {code}");
        return default;
    }

    public AsyncOperationHandle<GameObject> LoadEntityPrefabAsync(string type)
    {
        Init();
        var binding = GetEntity(type);
        if(binding != null && binding.entityRef != null && !binding.entityRef.RuntimeKeyIsValid())
        {
            return binding.entityRef.LoadAssetAsync<GameObject>();
        }
        Debug.LogError($"[TR] 엔터티 프리팹 로드 불가 {type}");
        return default;
    }

    public void ReleaseTilePrefab(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }
    }

    public void ReleaseEntityPrefab(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }
    }
}
