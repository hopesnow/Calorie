using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  PlayerMoveController moveController;
  PlayerAtionController actionController;

	// Use this for initialization
	void Start ()
  {
    moveController = GetComponent<PlayerMoveController>();
    actionController = GetComponent<PlayerAtionController>();
	}

  //vec 正規化された方向ベクトル
  public void Move(Vector3 vec)
  {
    moveController.Move(vec);

    //アニメーション再生？

    //向き変更？
  }

  public void Jump()
  {
    moveController.Jump(0);

    //アニメーション再生？
  }

  public void StartAbsorbtion()
  {
    actionController.StartAbsorbtion();
    //アニメーション再生？
  }

  public void StopAbsorbtion()
  {
    actionController.StopAbsorbtion();
  }

  public void Attack()
  {
    actionController.Attack();

    //アニメーション再生
  }

}
