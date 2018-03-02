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

  public void Jump(float force)
  {
    if(rigidbody.velocity.y == 0)
    rigidbody.AddForce(0,force,0, ForceMode.Impulse);
  }

  public void Move(Vector3 vec,float spd)
  {
    Vector3 vel = rigidbody.velocity;
    vel.x = vec.x * spd;
    vel.z = vec.z * spd;
    rigidbody.velocity = vel;
  }
}
