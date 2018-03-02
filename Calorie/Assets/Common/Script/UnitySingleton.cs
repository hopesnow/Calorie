//************************************************
//UnitySingleton.cs
//Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//UnitySingleton
//シングルトンクラス
//Initを派生先で呼んでね
//************************************************
public class UnitySingleton<T> : MonoBehaviour
{
  static T instance;

  static public T Instance { get { return instance; } }

  protected void Init(T ins)
  {
    instance = ins;
  }
}
