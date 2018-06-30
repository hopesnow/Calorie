//************************************************
//PlayerAnimationController
//Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//PlayerAnimationController
//アニメーションをプレイヤーの挙動で制御する
//************************************************
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;//移動してるかどうかとる

    IPlayerAnimation animation;

    // 初期化処理
    void Start()
    {
        animation = GetComponent<IPlayerAnimation>();
    }

    // 更新処理
    void Update()
    {
        Vector3 vel = rigidbody.velocity;
        if (Mathf.Abs(vel.x) > 0.01f || Mathf.Abs(vel.z) > 0.01f)
        {
            //animation.PlayRunning();
        }
        else
        {
            //animation.PlayIdle();
        }
    }
}