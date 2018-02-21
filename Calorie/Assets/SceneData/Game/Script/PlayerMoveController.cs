//************************************************
// PlayerMoveController.cs
// Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//************************************************
//PlayerMoveController
//プレイヤーの移動関数定義
//************************************************
public interface PlayerMoveController
{
  void Move(Vector3 vec);
  void Jump(float force);
}
