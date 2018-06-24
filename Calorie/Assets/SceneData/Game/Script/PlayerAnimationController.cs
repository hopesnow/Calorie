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
    [SerializeField]
    Rigidbody rigidbody;//移動してるかどうかとる

    IPlayerAnimation animation;

    // Use this for initialization
    void Start()
    {
        animation = GetComponent<IPlayerAnimation>();
    }

    // Update is called once per frame
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