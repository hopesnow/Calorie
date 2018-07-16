using System;
using UnityEngine;
using UniRx;

public class PlayerController : MonoBehaviour
{
    IPlayerMover mover;
    [SerializeField] private NormalShot shotPrefab;
    [SerializeField] private PlayerEnergy energyPrefab;
    private Rigidbody rigidbody;

    private const int PerShot = 7;              // 100%から何発打
    private const float MaxChargeSpeed = 3f;    // マックスになるために必要なチャージ時間
    private ReactiveProperty<float> charge = new ReactiveProperty<float>();
    private float ChargePerShot { get { return 1f / PerShot; } }   // 一発あたりのcharge量

    private Vector3 forceVec = Vector3.zero;
    private float forcePower = 0f;
    private float forceDecay = 0.9f;
    private float moveSpeed = 3f;

    private bool isChargable = false;           // 給水可能フラグ
    private IDisposable isChargeDispose = null; // 給水可能フラグの検知用
    private bool charging = false;              // 給水中フラグ

    // 初期化処理
    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        mover = GetComponent<IPlayerMover>();

        this.charge.Value = 0.5f;

        // UI系の初期化処理
        var energy = Instantiate(this.energyPrefab, FieldManager.Instance.uiParent);
        energy.SetTarget(this.transform);
        energy.ChangeRemainPercent(this.charge.Value);

        // 残弾更新処理の登録
        this.charge.Subscribe(remain =>
        {
            energy.ChangeRemainPercent(remain);
        });
    }

    // 更新処理
    public void Update()
    {
        // ダメージ処理
        if (this.forcePower > 0.1f)
        {
            this.rigidbody.AddForce(this.forceVec * this.forcePower);
            this.forcePower *= this.forceDecay; // 少しずつ減衰する
        }

        // 給水処理
        if (this.charging)
        {
            Charge();
        }
    }

    public void Move(Vector3 vector)
    {
        mover.Move(vector, this.moveSpeed);
    }

    // 可能であればショットをする
    public void Shot(float chargeTime, Transform parent, Action successEvent)
    {
        if (chargeTime >= 3f && this.charge.Value > ChargePerShot * 5)
        {
            // チャージショット強
            var shot = Instantiate(shotPrefab, parent);
            shot.transform.localPosition = this.transform.localPosition + new Vector3(0f, 1f, 0f);
            shot.Init(this.transform.forward, this.gameObject.name, NormalShot.ChargeType.Charge2);
            this.charge.Value -= ChargePerShot * 5;

            successEvent();
        }
        else if (chargeTime >= 1f && this.charge.Value > ChargePerShot * 2)
        {
            // チャージショット
            var shot = Instantiate(shotPrefab, parent);
            shot.transform.localPosition = this.transform.localPosition + new Vector3(0f, 1f, 0f);
            shot.Init(this.transform.forward, this.gameObject.name, NormalShot.ChargeType.Charge1);
            this.charge.Value -= ChargePerShot * 2;

            successEvent();
        }
        else if (this.charge.Value > ChargePerShot)
        {
            // 通常ショット
            var shot = Instantiate(shotPrefab, parent);
            shot.transform.localPosition = this.transform.localPosition + new Vector3(0f, 1f, 0f);
            shot.Init(this.transform.forward, this.gameObject.name, NormalShot.ChargeType.Normal);
            this.charge.Value -= ChargePerShot;

            successEvent();
        }
        else
        {
            // ショット失敗
        }
    }

    // 攻撃を受けたときのノックバック処理
    public void Knockback(Vector3 vec, float power)
    {
        this.forceVec = vec.normalized;
        this.forcePower = power;
    }

    // 補充処理
    [Obsolete]
    public void Replenishment(int chargePower)
    {
        this.charge.Value += chargePower;
        if (this.charge.Value > PerShot)
        {
            this.charge.Value = PerShot;
        }
    }

    // チャージ開始
    public void StartCharge()
    {
        this.charging = true;
    }

    // チャージ終了
    public void EndCharge()
    {
        this.charging = false;
    }

    // チャージをしようとする
    public void Charge()
    {
        // チャージ可能であればチャージする
        if (this.isChargable)
        {
            if (this.charge.Value < 1f)
            {
                this.charge.Value += Time.deltaTime / MaxChargeSpeed;
                if (this.charge.Value > 1f)
                {
                    this.charge.Value = 1f;
                }
            }
        }
    }

    // 範囲内チェック
    private void OnTriggerEnter(Collider coll)
    {
        // 噴水近くに来たときの判定
        if (coll.gameObject.CompareTag("Fountain"))
        {
            var stoneFountain = coll.GetComponent<StoneFountain>();
            if (stoneFountain != null)
            {
                if (this.isChargeDispose != null)
                {
                    this.isChargeDispose.Dispose();
                    this.isChargeDispose = null;
                }

                // チャージ可能フラグの監視処理
                this.isChargeDispose = stoneFountain.Playing.Subscribe(playing =>
                {
                    this.isChargable = playing;
                });
            }
        }
    }

    // 範囲外チェック
    private void OnTriggerExit(Collider coll)
    {
        // 噴水から離れたときの判定
        if (coll.gameObject.CompareTag("Fountain"))
        {
            if (this.isChargeDispose != null)
            {
                this.isChargeDispose.Dispose();
                this.isChargeDispose = null;
            }

            this.isChargable = false;
        }        
    }
}