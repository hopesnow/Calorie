using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    IPlayerMover mover;
    [SerializeField] private NormalShot shotPrefab;
    private Rigidbody rigidbody;

    private Vector3 forceVec = Vector3.zero;
    private float forcePower = 0f;
    private float forceDecay = 0.9f;

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        mover = GetComponent<IPlayerMover>();
    }

    public void Update()
    {
        // ダメージ処理
        if (this.forcePower > 0.1f)
        {
            this.rigidbody.AddForce(this.forceVec * this.forcePower);
            this.forcePower *= this.forceDecay; // 少しずつ減衰する
        }
    }

    public void Move(Vector3 vector)
    {
        mover.Move(vector, 2);
    }

    public void Shot(Transform parent)
    {
        var shot = Instantiate(shotPrefab, parent);
        shot.transform.localPosition = this.transform.localPosition + new Vector3(0f, 1f, 0f);
        shot.Init(this.transform.forward, this.gameObject.name);
    }

    // 攻撃を受けたときのノックバック処理
    public void Knockback(Vector3 vec, float power)
    {
        this.forceVec = vec.normalized;
        this.forcePower = power;
    }
}