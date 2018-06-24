using System.Collections;
using UnityEngine;

/// <summary>
/// 通常ショットの弾自体の処理
/// </summary>
public class NormalShot : MonoBehaviour
{
    public enum State
    {
        Accel,
        Brake,
        Invalid,
    }

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Collider collider;
    [SerializeField] private float initSize = 1.75f;

    [SerializeField] private float initSpeed = 0.1f;
    [SerializeField] private float maxSpeed = 0.6f;
    [SerializeField] private float minSpeed = 0.15f;

    [SerializeField] private float upAccel = 0.1f;
    [SerializeField] private float downAccel = 0.03f;

    [SerializeField] private float knockbackPower = 5f;

    private Vector3 vec;
    private float speed;
    private float size;

    // 親オブジェクトの名前(あたり判定用)
    private string parentName = string.Empty;

    private float destroyRange = 50f;

    private State shotState = State.Accel;
    // private bool shotInvalid = false;
    private float deathTime = float.MaxValue;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="vec">方向</param>
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
        switch (this.shotState)
        {
            case State.Accel:   // 発射直後のショットの速度が上がるタイミング
                // 座標の更新
                this.transform.localPosition += this.vec * this.speed;
                this.speed += this.upAccel;
                if (this.speed >= this.maxSpeed)
                {
                    // 次のStateへ
                    this.speed = this.maxSpeed;
                    this.shotState = State.Brake;
                }

                // 範囲外チェック
                CheckField();

                break;

            case State.Brake:   // 速度が上がりきって下がるタイミング
                // 座標の更新
                this.transform.localPosition += this.vec * this.speed;
                this.speed -= this.downAccel;
                if (this.speed <= this.minSpeed)
                {
                    // これ以上遅くはならない
                    this.speed = this.minSpeed;
                }

                // 範囲外チェック
                CheckField();

                break;

            case State.Invalid: // 衝突後の無効タイミング
                if (this.deathTime <= 0)
                {
                    Destroy(this.gameObject);
                }

                // ショットが消えるまでのカウントダウン
                this.deathTime -= Time.deltaTime;
                break;
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
            var power = this.knockbackPower * Mathf.Sqrt(this.speed);
            player.Knockback(this.vec, power);
            InvalidShot();
        }
    }

    /// <summary>
    /// ステージの範囲外チェック
    /// </summary>
    private void CheckField()
    {
        // 範囲外チェック
        if (this.transform.localPosition.x > this.destroyRange
            || this.transform.localPosition.z > this.destroyRange
            || this.transform.localPosition.x < -this.destroyRange
            || this.transform.localPosition.z < -this.destroyRange)
        {
            // 画面外に出ると消す
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ショットの無効化
    /// 対象に当たった際に
    /// </summary>
    private void InvalidShot()
    {
        // this.shotInvalid = true;
        this.shotState = State.Invalid;
        this.particle.Stop();
        this.collider.enabled = false;
        this.deathTime = this.particle.main.duration;
    }
}
