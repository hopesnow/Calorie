using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneChangeAnimation
{
  void PlayInAnime();
  void PlayOutAnime();

  bool IsPlayAnime { get; }

}
