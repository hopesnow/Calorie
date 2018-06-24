using System.Collections;
using UnityEngine;

/// <summary>
/// 通常ショットの弾自体の処理
/// </summary>
public class NormalShot : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Collider collider;
    [SerializeField] private float initSize = 1.75f;
    [SerializeField] private float initSpeed = 0.55f;
    [SerializeField] private float decaySpeed = 0.03f;
    [SerializeField] private float minimumSpeed = 0.15f;
    [SerializeField] private float knockbackPower = 5f;

    private const float PowerMagni = 1000f;  // power倍率

    private Vector3 vec;
    private float speed;
    private float size;

    // 親オブジェクトの名前(あたり判定用)
    private string parentName = string.Empty;

    private float destroyRange = 50f;

    private bool shotInvalid = false;
    private float deathTime = float.MaxValue;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="vec">方向</param>
    /// <param name="spd">速度</param>
    /// <param name="scale">弾のサイズ</param>
    public void Init(Vector3 vec, string parentName)// , float spd = 1.0f, float scale = 1.0f)
    {
        this.vec = vec.normalized;
        this.speed = initSpeed;
        this.size = initSize;
        this.parentName = parentName;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

        if (shotInvalid)
        {
            if (this.deathTime <= 0)
            {
                Destroy(this.gameObject);
            }

            // ショットが消えるまでのカウントダウン
            this.deathTime -= Time.deltaTime;
        }
        else
        {
            // 座標の更新
            this.transform.localPosition += this.vec * this.speed;
        }

        // 速度減衰処理
        if (this.speed < minimumSpeed)
        {
            this.speed = minimumSpeed;
        }
        else
        {
            this.speed -= decaySpeed;
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
    /// <param name="coll">判定オブジェクト</param>
    private void OnTriggerEnter(Collider coll)
    {
        // 自身の場合は何もしない
        if (coll.gameObject.name == this.parentName)
            return;
        
        // collision.gameObject.GetComponent
        Debug.LogFormat("弾が当たったオブジェクト: {0}", coll.gameObject.name);

        var player = coll.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Knockback(this.vec, this.knockbackPower * PowerMagni * this.speed);
            InvalidShot();
        }
    }

    /// <summary>
    /// ショットの無効化
    /// 対象に当たった際に
    /// </summary>
    private void InvalidShot()
    {
        this.shotInvalid = true;
        this.particle.Stop();
        this.collider.enabled = false;
        this.deathTime = this.particle.main.duration;
    }
}
