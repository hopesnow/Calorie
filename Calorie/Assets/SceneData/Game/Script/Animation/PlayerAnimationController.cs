//************************************************
//PlayerAnimationController.cs
//Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//PlayerAnimationController
//プレイヤーのアニメーション再生制御クラス
//************************************************
public class PlayerAnimationController : MonoBehaviour,IPlayerAnimation
{
  [SerializeField]
  Animator animator;

  int currentState;

  bool isFade = false;//遷移中確認用

  void Start()
  {
    currentState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
  }

  #region 再生関数群
  public void PlayIdle()
  {
    Play("Idle",0);
  }

  public void PlayJumping()
  {
    Play("Jumping", 1);
  }

  public void PlayRunning()
  {
    Play("Running", 1);
  }

  public void PlayWalking()
  {
    Play("Walking", 1);
  }
  #endregion

  //************************************************************
  //Play stateName = State名 duration = 補完時間[s]
  //指定したステートのアニメーションを再生する
  //同じステートを指定した場合は無効にする
  //************************************************************
  void Play(string stateName,float duration = 0.5f)
  {
    string convert = "Base Layer." + stateName;
    int hash = Animator.StringToHash(convert);//ステート名を直接取得する方法がないのでハッシュに変換する(重いかも・・・？)

    //今のステートのBehaviourを取得（重いかも)
    var stateBase = animator.GetBehaviours(currentState,0);
    var state = stateBase[0] as AnimationStateTransition;

    //重い場合　どれもキャッシュできる情報のはずなのでキャッシュする

    //自動遷移中か
    if(state.IsNext)
    {
      currentState = Animator.StringToHash("Base Layer." + state.NextState);
    }
    else if(!isFade)//フェード中でない場合は最新ステートの確認
    {
      currentState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
    }

    if (currentState == hash)
      return;

    //アニメーション再生
    animator.CrossFadeInFixedTime(stateName, duration);
    currentState = hash;

    //フェード中の場合もあるので無効かしてから再度再生
    StopCoroutine("WaitFade");
    isFade = true;
    StartCoroutine(WaitFade(duration));
  }

  //フェード中のフラグ更新するだけ
  IEnumerator WaitFade(float duration)
  {
    yield return new WaitForSeconds(duration);
    isFade = false;
  }
}
