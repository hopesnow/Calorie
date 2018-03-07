using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//動作確認用
public class DebugController : MonoBehaviour
{
  [SerializeField]
  PlayerController controller;
  
	// Update is called once per frame
	void Update ()
  {
    Vector3 vec = Vector3.zero;

    if(Input.GetKey(KeyCode.W))
    {
      vec.z += 1;
    }

    if (Input.GetKey(KeyCode.S))
    {
      vec.z -= 1;
    }

    if (Input.GetKey(KeyCode.A))
    {
      vec.x += -1;
    }

    if (Input.GetKey(KeyCode.D))
    {
      vec.x += 1;
    }

    controller.Move(vec.normalized);

  }
}
