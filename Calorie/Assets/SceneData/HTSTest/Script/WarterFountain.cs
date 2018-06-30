using UnityEngine;

public class WarterFountain : MonoBehaviour
{
    [SerializeField] private Collider fountainColl;

    // 初期化処理
    public void Start()
    {
        this.fountainColl.enabled = true;
    }

    // 泉の当たり判定
    private void OnTriggerEnter(Collider coll)
    {
        var player = coll.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Replenishment(1);

            InvalidFountain();

            // 暫定
            Destroy(this.gameObject);
        }
    }

    // 泉の無効化
    private void InvalidFountain()
    {
        // ショットを参考にしているので、必要ないやつはあとで削除
        // this.shotState = State.Invalid;
        // this.particle.Stop();
        // this.shotColl.enabled = false;
        this.fountainColl.enabled = false;
        // this.deathTime = this.particle.main.duration;
    }
}
