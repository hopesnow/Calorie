
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void InputMoveEvent(Vector3 vec);

public class MoveButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    InputMoveEvent inputMoveEvent;

    [SerializeField] private Button moveButton;
    [SerializeField] private RectTransform buttonTransform;

    [SerializeField] private Text debugText;

    private bool isPress = false;
    private Vector3 buttonInitPosition;
    private int touchIndex = 0;
    private Vector2 mouseDownPosition;
    private Vector3 dragDiff = Vector3.zero;

    public void Init(InputMoveEvent _event)
    {
        this.inputMoveEvent += _event;
    }

    private void Start()
    {
        // 初期位置を取得
        this.buttonInitPosition = this.buttonTransform.localPosition;
    }

    public void OnPointerDown(PointerEventData data)
    {
        isPress = true;
        this.touchIndex = Input.touchCount - 1;
        this.mouseDownPosition = data.position;
        this.dragDiff = Vector3.zero;
    }

    public void OnDrag(PointerEventData data)
    {
        var diff = data.position - this.mouseDownPosition;
        this.dragDiff = new Vector3(diff.x, 0f, diff.y);
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPress = false;
        this.buttonTransform.localPosition = this.buttonInitPosition;
        this.inputMoveEvent(Vector3.zero);
    }

    private void Update()
    {
        if (this.isPress)
        {
            // ボタンを押した場所からの差分
            /*
            Vector2 touchPos;
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            touchPos = Input.touches[this.touchIndex].position;
#else
            touchPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#endif
            */

            // var diff = new Vector3(touchPos.x - this.mouseDownPosition.x, 0,  touchPos.y - this.mouseDownPosition.y);

            var diff = this.dragDiff;

            // 50以上は初期値から離れないように
            if (this.dragDiff.magnitude > 50)
            {
                this.buttonTransform.localPosition = this.buttonInitPosition + new Vector3(this.dragDiff.x, this.dragDiff.z, 0).normalized * 50;
            }
            else
            {
                this.buttonTransform.localPosition = this.buttonInitPosition + new Vector3(this.dragDiff.x, this.dragDiff.z, 0);
            }

            // 5を超えていたら移動判定あり。
            if (this.dragDiff.magnitude > 5)
            {
                this.inputMoveEvent(this.dragDiff.normalized);
            }
            else
            {
                this.inputMoveEvent(Vector3.zero);
            }
        }

        string str = string.Empty;
        str += string.Format("mousePos x:{0}, y:{1}\n", Input.mousePosition.x, Input.mousePosition.y);
        str += string.Format("mouseDownPos x:{0}, y:{1}", this.mouseDownPosition.x, this.mouseDownPosition.y);
        this.debugText.text = str;
    }
}
