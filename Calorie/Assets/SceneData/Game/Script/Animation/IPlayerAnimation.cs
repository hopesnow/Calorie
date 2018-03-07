//******************************************************
//IPlayerAnimation
//Author yt-hrd
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//******************************************************
//IPlayerAnimation
//プレイヤーアニメーション
//******************************************************
public interface IPlayerAnimation
{
  void PlayIdle();
  void PlayWalking();
  void PlayRunning();
  void PlayJumping();
  void PlayAttack();

}
