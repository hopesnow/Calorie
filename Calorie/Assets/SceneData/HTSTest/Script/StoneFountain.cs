using UnityEngine;
using UniRx;

public class StoneFountain : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private ReactiveProperty<bool> playing = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> Playing { get { return this.playing; } }

    private const float PlayMinTime = 5f;
    private const float PlayMaxTime = 6f;
    private const float WaitMinTime = 2f;
    private const float WaitMaxTime = 3f;

    private float waitTime = 0;

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
                // 再生する処理
                this.waitTime = Random.Range(PlayMinTime, PlayMaxTime);
                this.particle.Play();
            }
            else
            {
                // 停止する処理
                this.waitTime = Random.Range(WaitMinTime, WaitMaxTime);
                this.particle.Stop();
            }
        }
    }
}
