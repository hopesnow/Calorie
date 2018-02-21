//************************************************
//PlayerActionController.cs
//Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//PlayerActionController
//プレイヤー攻撃定義
//************************************************
public interface PlayerAtionController
{
  void Attack();
  void StartAbsorbtion();
  void StopAbsorbtion();
}
