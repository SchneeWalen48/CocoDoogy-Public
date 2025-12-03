using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnergyRecoveryTime : MonoBehaviour
{
    public TextMeshProUGUI energyRecoveryTime;

    void Awake()
    {
        EnergyRecoverTime();
    }

    void EnergyRecoverTime()
    {
        if (UserData.Local.goods[GoodsType.energy] > 5) return;

        long time = UserData.Local.master.lastEnergyTime;

        DateTimeOffset dto = new DateTimeOffset(time, TimeSpan.Zero);
        print($"{dto} --> Unix Seconds: {dto.ToUnixTimeSeconds()}");



    }
}
