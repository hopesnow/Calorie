using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour, PlayerMoveController
{
  [SerializeField]
  new Rigidbody rigidbody;
  [SerializeField]
  Player player;

  public void Jump(float force)
  {
    if(rigidbody.velocity.y == 0)
    rigidbody.AddForce(0,force+player.CalJumpForce(),0, ForceMode.Impulse);
  }

  public void Move(Vector3 vec)
  {
    Vector3 vel = rigidbody.velocity;
    vel.x = vec.x * player.CalSpd();
    vel.z = vec.z * player.CalSpd();
    rigidbody.velocity = vel;
  }
}
