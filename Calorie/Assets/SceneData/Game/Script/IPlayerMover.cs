//************************************************
// IPlayerMover.cs
// Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//************************************************
//IPlayerMover
//プレイヤーの移動関数定義
//************************************************
public interface IPlayerMover
{
  //移動関数 vecは正規化前提
  void Move(Vector3 vec,float spd);
  void Jump(float force);
}
