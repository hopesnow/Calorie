using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeSimple : MonoBehaviour
{
   public void ChangeScene(string sceneName)
  {
    SceneChanger.Instance.ChangeScene(sceneName);
  }

  public void BackScene()
  {
    SceneChanger.Instance.BackScene();
  }
}
