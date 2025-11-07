using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressManager : MonoBehaviour
{
    public static PlayerProgressManager Instance;
    private Dictionary<string, StageProgressData> stageProgressDict = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else Destroy(gameObject);
    }

    public StageProgressData GetStageProgress(string stageId)
    {
        if (!stageProgressDict.ContainsKey(stageId))
            stageProgressDict[stageId] = new StageProgressData { stageId = stageId };
        return stageProgressDict[stageId];
    }

    public void UpdateStageTreasure(string stageId, bool[] newlyCollected)
    {
        var progress = GetStageProgress(stageId);
        for (int i = 0; i < 3; i++)
        {
            if (newlyCollected[i])
                progress.treasureCollected[i] = true;
        }
        SaveProgress();
    }

    void SaveProgress()
    {
        //Todo : Firebase 연동시 변경해야 할 부분
        PlayerPrefs.SetString("StageProgress", JsonUtility.ToJson(new Wrapper(stageProgressDict)));
    }

    void LoadProgress()
    {
        //Todo : Firebase 연동시 변경해야 할 부분
        if (PlayerPrefs.HasKey("StageProgress"))
        {
            var json = PlayerPrefs.GetString("StageProgress");
            stageProgressDict = JsonUtility.FromJson<Wrapper>(json).ToDictionary();
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<StageProgressData> list = new();
        public Wrapper(Dictionary<string, StageProgressData> dict) => list.AddRange(dict.Values);
        public Dictionary<string, StageProgressData> ToDictionary()
        {
            var d = new Dictionary<string, StageProgressData>();
            foreach (var e in list) d[e.stageId] = e;
            return d;
        }
    }
}