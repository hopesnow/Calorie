//*************************************************************
//PlayerMover.cs
//Author yt-hrd
//*************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*************************************************************
//PlayerMover
//移動クラス
//*************************************************************
public class PlayerMover : MonoBehaviour, IPlayerMover
{
  [SerializeField]
  new Rigidbody rigidbody;

  Vector3 moveSpd;
  Vector3 outsideForce;//移動以外に外側から与えられる速度

  public Vector3 OutsideForce { set { outsideForce = value; } }

  //移動を伝える
  public void Move(Vector3 vec,float spd)
  {
    if (vec.magnitude == 0)
      return;

    transform.rotation = Quaternion.LookRotation(vec);
    moveSpd = vec * spd;
  }

  public void AddOutsideForce(Vector3 force)
  {
    outsideForce += force;
  }

  //外力と移動力を合算して考える
  //外力は継続して与える場合マイフレーム与えること
  void LateUpdate()
  {
    rigidbody.velocity = moveSpd + outsideForce;
    outsideForce = Vector3.zero;
    moveSpd = Vector3.zero;
  }
}
