﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
  [SerializeField]
  PlayerMover mover;
  [SerializeField]
  PlayerAction action;

  Vector3 vec;
  // Update is called once per frame
  void Update ()
  {
    vec = new Vector3(0,0,0);

    if(Input.GetKey(KeyCode.W))
    {
      vec.z++;
    }

    if (Input.GetKey(KeyCode.A))
    {
      vec.x--;
    }

    if (Input.GetKey(KeyCode.S))
    {
      vec.z--;
    }

    if (Input.GetKey(KeyCode.D))
    {
      vec.x++;
    }

    if(Input.GetKeyDown(KeyCode.Space))
    {
      mover.Jump(1);
    }

    if(Input.GetKey(KeyCode.LeftShift))
    {
      action.StartAbsorbtion();
    }
    else
    {
      action.StopAbsorbtion();
    }

    if(Input.GetKey(KeyCode.RightShift))
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
