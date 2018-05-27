using UnityEngine;

/// <summary>
/// 通常ショットの弾自体の処理
/// </summary>
public class NormalShot : MonoBehaviour
{
    private Vector3 vec;
    private float speed;
    private float size;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="vec">方向</param>
    /// <param name="spd">速度</param>
    /// <param name="scale">弾のサイズ</param>
    public void Init(Vector3 vec, float spd = 1.0f, float scale = 1.0f)
    {
        this.vec = vec.normalized;
        this.speed = spd;
        this.size = scale;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        this.transform.localPosition += this.vec * this.speed;
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">判定オブジェクト</param>
    private void OnCollisionEnter(Collision collision)
    {
        // collision.gameObject.GetComponent
        Debug.LogFormat("弾が当たったオブジェクト: {0}", collision.gameObject.name);
    }
}
