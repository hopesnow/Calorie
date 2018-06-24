using System.Collections;
using UnityEngine;

/// <summary>
/// 通常ショットの弾自体の処理
/// </summary>
public class NormalShot : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private Vector3 vec;
    private float speed;
    private float size;
    private const float InitSpeed = 0.6f;
    private const float DecaySpeed = 0.01f;
    private const float MinimumSpeed = 0.2f;

    // 親オブジェクトの名前(あたり判定用)
    private string parentName = string.Empty;

    private float destroyRange = 50f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="vec">方向</param>
    /// <param name="spd">速度</param>
    /// <param name="scale">弾のサイズ</param>
    public void Init(Vector3 vec, string parentName)// , float spd = 1.0f, float scale = 1.0f)
    {
        this.vec = vec.normalized;
        this.speed = InitSpeed;
        this.size = 1.0f;
        this.parentName = parentName;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        // 座標の更新
        this.transform.localPosition += this.vec * this.speed;

        // 速度減衰処理
        if (this.speed < MinimumSpeed)
        {
            this.speed = MinimumSpeed;
        }
        else
        {
            this.speed -= DecaySpeed;
        }

        // 範囲外チェック
        if (this.transform.localPosition.x > this.destroyRange
            || this.transform.localPosition.z > this.destroyRange
            || this.transform.localPosition.x < -this.destroyRange
            || this.transform.localPosition.z < -this.destroyRange)
        {
            Destroy(this.gameObject);
            // StartCoroutine(DestroyShot());
        }
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">判定オブジェクト</param>
    private void OnTriggerEnter(Collider collider)
    {
        // collision.gameObject.GetComponent
        Debug.LogFormat("弾が当たったオブジェクト: {0}", collider.gameObject.name);
    }

    /// <summary>
    /// ショットの無効化
    /// 対象に当たった際に
    /// </summary>
    private void InvalidShot()
    {
    }
}
