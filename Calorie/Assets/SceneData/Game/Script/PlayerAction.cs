using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour, PlayerAtionController
{
  [SerializeField]
  AbsorbtionObject absorbtionObject;
  [SerializeField]
  Player player;

  void Start()

  {
    //吸い込み時処理追加
    absorbtionObject.HitAction = player.ChargePower;
  }

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
    player.UsePower();
  }
}
