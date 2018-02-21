//************************************************
//PlayerParam.cs
//Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//PlayerParam
//プレイヤー性能
//************************************************
[CreateAssetMenu(fileName = "PlayerParam",menuName = "CreatePlayerParam",order =100)]
public class PlayerParam : ScriptableObject
{
  [SerializeField]
  float spd;
  [SerializeField]
  float jumpForce;
  [SerializeField]
  int oneShotNeedPower;//ショット1発で使うパワー
  [SerializeField]
  int maxPower;//最大パワー

  public float Spd { get { return spd; } set { spd = value; } }
  public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
  public int OneShotNeedPower { get { return oneShotNeedPower; } set { oneShotNeedPower = value; } }
  public int MaxPower { get { return maxPower; } set { maxPower = value; } }
}
