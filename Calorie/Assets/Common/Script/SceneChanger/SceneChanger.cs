using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : UnitySingleton<SceneChanger>
{
  ISceneChange sceneChange;
  ISceneChangeAnimation sceneChangeAnimation;

  private void Awake()
  {
    Init(this);

    sceneChange = GetComponent<ISceneChange>();
    sceneChangeAnimation = GetComponent<ISceneChangeAnimation>();
  }

  private void Start()
  {
    ChangeScene("A");
  }

  public void ChangeScene(string sceneName)
  {
    StartCoroutine(_SceneChange(()=>sceneChange.ChangeScene(sceneName)));
  }

  public void BackScene()
  {
    StartCoroutine(_SceneChange(sceneChange.BackScene));
  }

  IEnumerator _SceneChange(System.Action changeAction)
  {
    sceneChangeAnimation.PlayInAnime();

    while(sceneChangeAnimation.IsPlayAnime)
    {
      yield return null;
    }

    changeAction();

    while(sceneChange.IsChanging)
    {
      yield return null;
    }

    sceneChangeAnimation.PlayOutAnime();

    while(sceneChangeAnimation.IsPlayAnime)
    {
      yield return null;
    }   
  }


}
