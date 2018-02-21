using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour, PlayerAtionController
{
  [SerializeField]
  AbsorbtionObject absorbtionObject;

  public void StartAbsorbtion()
  {
    absorbtionObject.gameObject.SetActive(true);
  }

  public void StopAbsorbtion()
  {
    absorbtionObject.gameObject.SetActive(false);
  }

  public void Attack()
  {
    Debug.Log("shot");
  }
}
