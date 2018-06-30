using UnityEngine;
using UniRx;

public class PlayerController : MonoBehaviour
{
    IPlayerMover mover;
    [SerializeField] private NormalShot shotPrefab;
    [SerializeField] private PlayerEnergy energyPrefab;
    private Rigidbody rigidbody;

    private const int MaxCharge = 10;
    private ReactiveProperty<int> charge = new ReactiveProperty<int>();

    private Vector3 forceVec = Vector3.zero;
    private float forcePower = 0f;
    private float forceDecay = 0.9f;
    private float moveSpeed = 3f;

    // 初期化処理
    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        mover = GetComponent<IPlayerMover>();

        this.charge.Value = 5;

        // UI系の初期化処理
        var energy = Instantiate(this.energyPrefab, FieldManager.Instance.uiParent);
        energy.SetTarget(this.transform);
        energy.ChangeRemainPercent((float)this.charge.Value / MaxCharge);

        // 残弾更新処理の登録
        this.charge.Subscribe(remain =>
        {
            energy.ChangeRemainPercent((float)remain / MaxCharge);
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
    }

    public void Move(Vector3 vector)
    {
        mover.Move(vector, this.moveSpeed);
    }

    // 可能であればショットをする
    public void Shot(Transform parent)
    {
        if(this.charge.Value > 0)
        {
            var shot = Instantiate(shotPrefab, parent);
            shot.transform.localPosition = this.transform.localPosition + new Vector3(0f, 1f, 0f);
            shot.Init(this.transform.forward, this.gameObject.name);
            this.charge.Value -= 1;
        }
    }

    // 攻撃を受けたときのノックバック処理
    public void Knockback(Vector3 vec, float power)
    {
        this.forceVec = vec.normalized;
        this.forcePower = power;
    }

    // 補充処理
    public void Replenishment(int chargePower)
    {
        this.charge.Value += chargePower;
        if (this.charge.Value > MaxCharge)
        {
            this.charge.Value = MaxCharge;
        }
    }
}