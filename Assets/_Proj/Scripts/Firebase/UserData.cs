using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    

public interface IUserDataCategory<T> where T : class
{
    bool TryUpdateFirebase(T dataValue);
    
}




/// <summary>
/// <b>유저 데이터 관리용 클래스.</b>
/// <br>[DB 루트 노드] -> [users] -> [(uid)] : [이 클래스의 JSON]</br>
/// </summary>
[Serializable]
public class UserData
//유저 데이터 관리용 클래스.    
{
    //로컬의 UserData. Firebase DB에서 받아오게 될 것임.
    public static UserData Local { get; private set; }


    
    
    #region 내부 클래스 정의. Firebase Realtime Database에 등록되는 각종 정보의 부모 격으로, 폴더라고 생각해주시면 됩니다.

    /// <summary>
    /// <b>유저 데이터 개요.</b>
    /// <br>1. 총 좋아요 개수</br>
    /// <br>2. 유저의 계정 생성일 타임스탬프</br>
    /// <br>3. 마지막 로그인 타임스탬프</br>
    /// <br>4. 마지막 활동 시간 타임스탬프(클라->DB 신호)</br>
    /// </summary>
    //프로필 정보(유저 데이터 개요)
    [Serializable]
    public class Master
    {

        //유저가 여지껏 받은 모든 좋아요 개수.
        public int totalLikes;

        //유저의 계정 생성일 타임스탬프
        public DateTime registeredDate;

        //마지막 로그인 시간 타임스탬프
        public DateTime lastLogin;

        //마지막 활동 시간 타임스탬프 (하트비트 보내듯이 주기적으로 DB에 업데이트 필요.)
        public DateTime lastActive;

        public Master()
        {
            totalLikes = 0;
            registeredDate = DateTime.Now;
            lastLogin = DateTime.Now;
            lastActive = DateTime.Now;
        }

    }

    /// <summary>
    /// <b>유저 재화 정보</b>
    /// <br>1. 병뚜껑 (무료 재화)</br>
    /// <br>2. 코인 (유료 재화)</br>
    /// <br>3. 에너지 (행동력)</br>
    /// <br>각각의 필드 값 = 해당 재화의 총량을 의미함.</br>
    /// </summary>
    [Serializable]
    public class Wallet
    {

        //병뚜껑 (무료 재화)
        public int cap;

        //코인 (유료 재화)
        public int coin;

        //에너지 (행동력)
        public int energy;
        
        public Wallet()
        {
            cap = 0;
            coin = 0;
            energy = 0;
        }
    }

    /// <summary>
    /// <b>유저 인벤토리 정보</b>
    /// <br>1. keyValues (TKey: 아이템의 id, TValue: 해당 아이템의 소지 개수)</br>
    /// </summary>
    [Serializable]
    public class Inventory
    {
        public Dictionary<int, int> keyValues;

        public Inventory()
        {
            keyValues = new();
        }
    }


    /// <summary>
    /// <b>로비에 배치한 장식물의 배치 정보</b>
    /// <br>1. keyValues (TKey: 장식물의 id, TValue: 그 장식물의 배치 정보 리스트)</br>
    /// </summary>
    [Serializable]
    public class Lobby
    {

        /// <summary>
        /// <b>장식물의 배치 정보</b>
        /// <br>1. xPosition (장식물의 x 위치)</br>
        /// <br>2. yPosition (장식물의 y 위치)</br>
        /// <br>3. yAxisRotation (장식물의 y축 회전각)</br>
        /// </summary>
        [Serializable]
        public class PlaceInfo
        {
            public int xPosition;
            public int yPosition;
            public int yAxisRotation;

            public PlaceInfo()
            {
                xPosition = 0;
                yPosition = 0;
                yAxisRotation = 0;
            }
        }

        public Dictionary<int, List<PlaceInfo>> keyValues;

        public Lobby()
        {
            keyValues = new();
        }
    }


    /// <summary>
    /// <b>이벤트 기록</b>
    /// <br>1. keyValues (TKey: 장식물의 id, TValue: 그 장식물의 배치 정보 리스트)</br>
    /// </summary>
    [Serializable]
    public class EventArchive
    {
        public Dictionary<string, int> keyValues;

        public EventArchive()
        {
            keyValues = new();
        }
    }

    /// <summary>
    /// <b>친구 목록</b>
    /// <br>1. keyValues (TKey: 친구의 uid, TValue: 해당 친구의 친구목록 상태와 요청 시간)</br>
    /// </summary>
    [Serializable]
    public class Friends
    {
        public Dictionary<string, FriendInfo> keyValues;

        /// <summary>
        /// <b>친구 상세정보</b>
        /// <br>1. 친구 상태 (0: 친구, 1: 보낸 요청, 2: 받은 요청)</br>
        /// <br>2. 요청을 보낸 시간</br>
        /// </summary>
        [Serializable]
        public class FriendInfo
        {
            [Flags]
            public enum FriendState
            {
                NewRequest = 0, Friend, RequestSent = 1 << 1, RequestReceived = 1 << 2,
            }

            public FriendState state;
            public DateTime requestTime;

            public FriendInfo()
            {
                state = (FriendState)0;
                requestTime = DateTime.Now;
            }

        }
    }


    #endregion



    public Master master;
    public Wallet wallet;
    public Inventory inventory;
    public Lobby lobby;
    public EventArchive eventArchive;
    public Friends friends;


    public UserData()
    {
        master = new Master();
        wallet = new Wallet();
        inventory = new Inventory();
        lobby = new Lobby();
        eventArchive = new EventArchive();
        friends = new Friends();
    }

    //로비 배치 정보
    //(가칭)ItemPlaceInfo itemId { int id = ###, Vector2 xyPos = { ###, ### }, int rotation = ###(0~270, 90도씩 스냅) }

    //시즌별(이벤트별?) 좋아요 갯수


    //유저 도감 테이블 개요
    //도감 해금 정보(Dictionary<CodexType,HashSet<string>>) => 수하씨가 만듦 => 도감타입별로 어떤어떤 녀석들을 해금했는가?(이건 갯수가 아님)

    //유저 스테이지 데이터 개요
    //StageProgressData[] progressDatas

    //StageProgressData => public string stageId; //이 스테이지의 id;
    //                     public bool[] treasureCollected = new bool[3]; // 각 보물별 개별 획득 여부
    //                     public int bestTreasureCount = 0;              // 지금까지 달성한 최대 별 개수

}
