using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeAnimationController : MonoBehaviour,ISceneChangeAnimation
{
  [SerializeField]
  Image image;
  [SerializeField]
  float animationTime = 0.5f;


  bool isPlayAnime;

  public void PlayInAnime()
  {
    StartCoroutine(FadeA(0, 1, animationTime));
  }

  public void PlayOutAnime()
  {
    StartCoroutine(FadeA(1, 0, animationTime));
  }

  IEnumerator FadeA(float begin,float end,float time )
  {
    if (isPlayAnime)
      yield break;

    isPlayAnime = true;
    float timer = 0;

    Color col = image.color;

    col.a = begin;
    while(timer < time)
    {
      timer += Time.deltaTime;

      float val = timer / time;
      col.a = begin * (1 - val) + end * val;
      image.color = col;
      yield return null;
    }
    isPlayAnime = false;
  }

  public bool IsPlayAnime
  {
    get { return isPlayAnime; }
  }
}
