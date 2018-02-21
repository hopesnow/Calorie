using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour ,ISceneChange
{
  bool isChanging;
  Stack<string> sceneStack = new Stack<string>();
  string prevSceneName;
  AsyncOperation unloadOperation;
  AsyncOperation loadOperation;

  public void ChangeScene(string sceneName)
  {
    //シーンがあれば削除

    if(!string.IsNullOrEmpty(prevSceneName))
    {
      unloadOperation = SceneManager.UnloadSceneAsync(prevSceneName);
    }

    //ロード
    loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
 
    if(AddUniqueStack(sceneName))
    {
       while(sceneStack.Count != 0)
      {
        string name = sceneStack.Pop();

        if(name == sceneName)
        {
          break;
        }
      }
    }

    prevSceneName = sceneName;
  }

  bool AddUniqueStack(string sceneName)
  {
    bool exist = sceneStack.Contains(sceneName);
    if (!exist)
      sceneStack.Push(sceneName);

    return exist;
  }

  public void BackScene()
  {
    if(sceneStack.Count>=2)
    {
      sceneStack.Pop();
      string name = sceneStack.Pop();
      ChangeScene(name);
    }
  }

  public bool IsChanging
  { get
    {
      bool isUnloading = unloadOperation != null && unloadOperation.isDone;
      bool isLoading = loadOperation != null && loadOperation.isDone;

      return isUnloading || isLoading;
    }
  }
}
