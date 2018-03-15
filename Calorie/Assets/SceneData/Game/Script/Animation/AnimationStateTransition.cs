//************************************************
//AnimationStateTransition.cs
//Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//AnimationStateTransition
//
//************************************************
public class AnimationStateTransition : StateMachineBehaviour
{
  [SerializeField]
  string nextState;//アニメーション終了時に自動遷移したいステート名　なければ空欄
  [SerializeField]
  float nextDurationTime;

  bool isNext = false;
  public bool IsNext { get { return isNext; } }
  public string NextState { get { return nextState; } }

  public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {                                                     
    if(stateInfo.normalizedTime >=1 && !string.IsNullOrEmpty(nextState) && !isNext)
    {
      animator.CrossFadeInFixedTime(nextState, nextDurationTime);
      isNext = true;
    }
  }

  public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    isNext = false;
  }
}
