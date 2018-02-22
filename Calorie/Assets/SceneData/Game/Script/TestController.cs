using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
  [SerializeField]
  PlayerMover mover;
  [SerializeField]
  PlayerAction action;
  [SerializeField]
  int playerNo;

  Vector3 vec;
  // Update is called once per frame
  void Update ()
  {
    vec = new Vector3(0,0,0);

    if(Input.GetAxisRaw(string.Format("Player{0} Vertical", playerNo)) > 0||Input.GetKey(KeyCode.W))
    {
      vec.z++;
    }

    if (Input.GetAxisRaw(string.Format("Player{0} Horizontal", playerNo)) < 0||Input.GetKey(KeyCode.A))
    {
      vec.x--;
    }

    if (Input.GetAxisRaw(string.Format("Player{0} Vertical", playerNo)) < 0||Input.GetKey(KeyCode.S))
    {
      vec.z--;
    }

    if (Input.GetAxisRaw(string.Format("Player{0} Horizontal", playerNo)) > 0||Input.GetKey(KeyCode.D))
    {
      vec.x++;
    }

    if(Input.GetButtonDown(string.Format("Player{0} Jump", playerNo))||Input.GetKeyDown(KeyCode.Space))
    {
      mover.Jump(1);
    }

    if(Input.GetButton(string.Format("Player{0} Absorb", playerNo))||Input.GetKey(KeyCode.LeftShift))
    {
      action.StartAbsorbtion();
    }
    else
    {
      action.StopAbsorbtion();
    }

    if(Input.GetButton(string.Format("Player{0} Shot", playerNo))||Input.GetKey(KeyCode.RightShift))
    {
      action.Attack();
    }


    //mover.Move(vec);

  }

  private void FixedUpdate()
  {
    mover.Move(vec.normalized);
  }
}
