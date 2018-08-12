using UnityEngine;
using UniRx;

public class StoneFountain : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private ReactiveProperty<bool> playing = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> Playing { get { return this.playing; } }

    private const float PlayMinTime = 2f;
    private const float PlayMaxTime = 6f;
    private const float WaitMinTime = 3f;
    private const float WaitMaxTime = 8f;

    private float waitTime = 0;
    private int reserveIndex = -1;

    // 初期化処理
    public void Start()
    {
        Playing.Value = false;
        this.waitTime = Random.Range(WaitMinTime, WaitMaxTime);
    }

    // 更新処理
    public void Update()
    {
        // 時間の調整
        this.waitTime -= Time.deltaTime;
        if (this.waitTime <= 0)
        {
            Playing.Value = !Playing.Value;
            if (Playing.Value)
            {
                // 場所取得処理
                var manager = FieldManager.Instance.fountain;
                this.reserveIndex = manager.GetFreeIndex();
                manager.SetReserve(this.reserveIndex, true);
                this.transform.localPosition = manager.GetPosition(this.reserveIndex);

                // 再生する処理
                this.waitTime = Random.Range(PlayMinTime, PlayMaxTime);
                Debug.LogFormat("再生箇所:{0}, 再生時間:{1}", this.reserveIndex, this.waitTime);
                this.particle.Play();
            }
            else
            {
                // 停止する処理
                FieldManager.Instance.fountain.SetReserve(this.reserveIndex, false);
                this.waitTime = Random.Range(WaitMinTime, WaitMaxTime);
                this.particle.Stop();
            }
        }
    }
}
