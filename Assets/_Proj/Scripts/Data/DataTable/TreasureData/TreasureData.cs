using UnityEngine;

public class TreasureData
{
    public string treasure_id;
    public TreasureType treasureType;
    public int reward_id;
    public int count;
}
public enum TreasureType 
{
    coin, cap, deco, costune, artifact
}
