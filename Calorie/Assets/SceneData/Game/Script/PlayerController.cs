using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  IPlayerMover mover;

  private void Start()
  {
    mover = GetComponent<IPlayerMover>();
  }

  public void Move(Vector3 vector)
  {
    mover.Move(vector, 2);
  }
}
