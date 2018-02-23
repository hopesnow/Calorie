using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
  [SerializeField]
  PlayerController controller;

  Vector3 vec;
  // Update is called once per frame
  void Update ()
  {
    vec = new Vector3(0,0,0);

    if(Input.GetKeyDown(KeyCode.Space))
    {
      controller.Jump();
    }

    if(Input.GetKey(KeyCode.LeftShift))
    {
      controller.StartAbsorbtion();
    }
    else
    {
      controller.StopAbsorbtion();
    }

    if(Input.GetKey(KeyCode.RightShift))
    {
      controller.Attack();
    }


    controller.Move(vec.normalized);

  }


}
