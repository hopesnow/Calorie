using System;
using System.Collections.Generic;
using System.Linq;
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

    public enum ChargeType
    {
        Normal,
        Charge1,
        Charge2,
    }

    [Serializable]
    public struct ShotTypeData
    {
        public ChargeType type;
        public float scale;
        public Color color;
        public float power;
    }

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Collider shotColl;
    [SerializeField] private float initSize = 1.75f;

    [SerializeField] private float initSpeed = 0.1f;
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private float minSpeed = 0.65f;

    [SerializeField] private float upAccel = 0.3f;
    [SerializeField] private float downAccel = 0.03f;

    [SerializeField] private float knockbackPower = 5f;
    [SerializeField] private List<ShotTypeData> shotDatas;

    private Vector3 vec;
    private float speed;
    private float correctPower = 1.0f;

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
    public void Init(Vector3 vec, string parentName, ChargeType chargeType)// , float spd = 1.0f)
    {
        this.vec = vec.normalized;
        this.speed = initSpeed;
        this.parentName = parentName;
        // this.transform.localScale = this.transform.localScale * scale;
        // this.transform.localScale *= scale;

        // ショットタイプごとの設定
        var typeData = this.shotDatas.First(l => l.type == chargeType);
        this.transform.localScale *= typeData.scale;
        this.particle.startColor = typeData.color;
        this.correctPower = typeData.power;
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

        var player = coll.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            var power = this.knockbackPower * Mathf.Sqrt(this.speed) * this.correctPower;
            Debug.LogFormat("ショットパワー: {0}", this.speed);
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
        this.shotColl.enabled = false;
        this.deathTime = this.particle.main.duration;
    }
}
