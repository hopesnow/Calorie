using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour, PlayerMoveController
{
  [SerializeField]
  new Rigidbody rigidbody;
  [SerializeField]
  PlayerParam playerParam;

  public void Jump(float force)
  {
    if(rigidbody.velocity.y == 0)
    rigidbody.AddForce(0,force+playerParam.JumpForce,0, ForceMode.Impulse);
  }

  public void Move(Vector3 vec)
  {
    Vector3 vel = rigidbody.velocity;
    vel.x = vec.x * playerParam.Spd;
    vel.z = vec.z * playerParam.Spd;
    rigidbody.velocity = vel;
  }
}
