using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerParam param;

    int power = 0;

    public void Init()
    {
        power = 0;
    }

    public void ChargePower()
    {
        power++;

        if (power > param.MaxPower)
            power = param.MaxPower;
    }

    public bool UsePower()
    {
        if (power < param.OneShotNeedPower)
        {
            return false;
        }

        power -= param.OneShotNeedPower;

        return true;
    }

    public float CalSpd()
    {
        float k = 1 - power / param.OneShotNeedPower;
        //TODO 調整値はどこかで設定したい
        k = k < 0.3f ? 0.3f : k;

        return param.Spd * k;
    }

    public float CalJumpForce()
    {
        float k = 1 - power / param.OneShotNeedPower;
        //TODO 調整値はどこかで設定したい
        k = k < 0.5f ? 0.3f : k;

        return param.JumpForce * k;
    }

}