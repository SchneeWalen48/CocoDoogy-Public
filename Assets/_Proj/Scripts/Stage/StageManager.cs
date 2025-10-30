using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //이 클래스가 해야 할 일: 스테이지 구성 요청(블록팩토리), 스테이지 내 각종 상호작용 상태 기억, 시작점에 주인공 생성, 주인공이 도착점에 도달 시 스테이지 클리어 처리.




    public Transform stageRoot;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Joystick joystickPrefab;
    [SerializeField] Transform joystickRoot;
    GameObject playerObject;
    //불러올 맵의 이름
    public string mapNameToLoad;

    Vector3Int startPoint;
    Vector3Int endPoint;

    public Dictionary<Vector3Int, List<Block>> placedBlocks = new();

    public List<Vector3Int> pbKeys = new();
    public List<Block> pbValues = new();

    //맵의 이름으로 찾아온 현재 맵 데이터 객체 (초기상태로의 복귀를 위해 필요)
    private MapData currentMapData;

    [SerializeField] BlockFactory factory;

    async void Start()
    {
        //1. 파이어베이스가 맵 정보를 가져오길 기다림.
        await Task.Delay(1000);
        currentMapData = await FirebaseManager_FORTEST.Instance.LoadMapFromFirebase(mapNameToLoad);
        StartCoroutine(StageStart());
    }

    //TODO: 상호작용 상태 기억시키기

    //TODO: 도착점 도달 시 스테이지 클리어 처리시키기.
    //스테이지 클리어 시에 

    IEnumerator StageStart()
    {
        stageRoot.name = mapNameToLoad;
        //2. 가져온 맵 정보로 이 씬의 블록팩토리가 맵을 생성하도록 함.
        //2-1. 블록팩토리가 맵을 생성
        LoadStage(currentMapData);

        //TODO: 2-2. 블록팩토리가 맵의 오브젝트들 중 서로 연결된 객체를 연결해 줌.

        //TODO: 3. 가져온 맵 정보로 모든 블록이 생성되고 연결까지 끝나면 가리고 있던 부분을 치워줌.

        //TODO: 4. 시작점에 코코두기를 생성해줌.
        playerObject = Instantiate(playerPrefab, startPoint, Quaternion.identity);
        yield return null;
        var joystick = Instantiate(joystickPrefab, joystickRoot);
        joystick.GetComponent<RectTransform>().anchoredPosition = new(300, 200);
        playerObject.GetComponent<PlayerMovement>().joystick = joystick;
        //TODO: 5. 카메라 연출 시작

        //6. 연출 종료 시부터 게임 시작.
    }

    void LoadStage(MapData loaded)
    {
        
        foreach (var block in loaded.blocks)
        {
            print($"[StageManager] {block.blockName}: {block.blockType} [{block.position.x}],[{block.position.y}],[{block.position.z}]");
            //여기서 팩토리가 들고 있는 프리팹으로 인스턴시에이트.
            
            GameObject go = factory.CreateBlock(block);
            go.transform.SetParent(stageRoot, true);
            go.name = block.blockName;

            //생성 후 블록의 타입이나 블록의 이름에 따라 적절한 컴포넌트를 붙여 줌.
                if (block.blockName == "WoodBlockData")
                    go.AddComponent<WoodBox>();
               else 
                    go.AddComponent<GroundBlock>();
            EnlistBlock(go.GetComponent<Block>());
            
            if (block.blockType == BlockType.StartPoint)
                startPoint = block.position; 
            if (block.blockType == BlockType.EndPoint) 
                endPoint = block.position;
            
        }

        pbKeys = placedBlocks.Keys.ToList();

        foreach (var kv in placedBlocks)
            pbValues.AddRange(kv.Value);
    }

    void EnlistBlock(Block target)
    {
        if (!placedBlocks.ContainsKey(target.gridPosition))
            placedBlocks.Add(target.gridPosition, new() { target });
        else
            placedBlocks[target.gridPosition].Add(target);
    }
}
