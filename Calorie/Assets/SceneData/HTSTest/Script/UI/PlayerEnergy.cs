using UnityEngine;
using UnityEngine.UI;

// プレイヤーの弾のチャージUI
public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private Image remain;

    private Transform target;
    private Vector3 offset = new Vector3(0f, 2f, 0f);

    private Camera camera3d;
    private Camera camera2d;
    private RectTransform canvasRect;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Start()
    {
        this.camera3d = FieldManager.Instance.camera3d;
        this.camera2d = FieldManager.Instance.camera2d;
        this.canvasRect = FieldManager.Instance.uiCanvasRect;
    }

    /// <summary>
    /// ターゲットの設定
    /// </summary>
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Update()
    {
        // 3DからUI座標を計算
        if (this.target != null)
        {
            Vector2 pos;
            var screenPos = RectTransformUtility.WorldToScreenPoint(this.camera3d, this.target.position + this.offset);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasRect, screenPos, this.camera2d, out pos);
            this.rect.localPosition = pos;
        }
    }

    /// <summary>
    /// 残量の更新
    /// </summary>
    public void ChangeRemainPercent(float percent)
    {
        this.remain.fillAmount = percent;
    }
}
