using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneChange
{
  void ChangeScene(string sceneName);
  void BackScene();
  bool IsChanging { get; }
}
