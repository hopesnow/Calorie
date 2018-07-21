using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerMoveState
{
    Movable,
    Attack,
}

//動作確認用
public class DebugController : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private SprayWaterParticle sprayShot;
    [SerializeField] private Collider[] tests;
    [SerializeField] private float moveSpeed = 2.0f;    // １ベクトルに対する移動速度
    [SerializeField] private float sprayShotTime = 3.0f; // 放水時間
    [SerializeField] private bool isPlayer = true;
    [SerializeField] private int playerNo = 0;

    [SerializeField] private MoveButtonController inputController;
    [SerializeField] private Button shotButton;
    [SerializeField] private Button resetButton;

    [SerializeField] private Transform shotParent;
    [SerializeField] private ParticleSystem shot1;

    private PlayerMoveState moveState = PlayerMoveState.Movable;
    private Vector3 input;
    private Vector3 initPos;

    private float delayMove = 0f;
    private float delayShot = 0f;
    private float shotCharge = 0f;

    public bool IsAttackable { get { return this.moveState == PlayerMoveState.Movable; } }

    private void Start()
    {
        // 初期位置保存
        this.initPos = GetComponent<Transform>().localPosition;

        // 放水の当たり判定初期化
        sprayShot.SetupCollision(this.playerNo);
        sprayShot.SetupTrigger(tests);

        if (inputController != null)
        {
            this.inputController.Init(vec =>
            {
                this.input = vec;
            });
        }

        // ショット処理
        if (this.shotButton != null)
        {
            this.shotButton.onClick.AddListener(() =>
            {
                if (IsAttackable)
                {
                    StartCoroutine(ShotSpray());
                }
            });
        }

        // リセット処理
        if (this.resetButton != null)
        {
            this.resetButton.onClick.AddListener(() =>
            {
                GetComponent<Transform>().localPosition = this.initPos;
                this.gameObject.SetActive(true);
                this.isPlayer = true;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isPlayer)
            return;

        Vector3 vec = Vector3.zero;

        // UI操作処理
        vec = this.input;

        if (Input.GetAxisRaw(string.Format("Player{0} Vertical", playerNo)) > 0)
        {
            vec.z += 1;
        }

        if (Input.GetAxisRaw(string.Format("Player{0} Vertical", playerNo)) < 0)
        {
            vec.z -= 1;
        }

        if (Input.GetAxisRaw(string.Format("Player{0} Horizontal", playerNo)) < 0)
        {
            vec.x -= 1;
        }

        if (Input.GetAxisRaw(string.Format("Player{0} Horizontal", playerNo)) > 0)
        {
            vec.x += 1;
        }

        // チャージ処理
        if (Input.GetButton(string.Format("Player{0} Shot", playerNo)))
        {
            // ショット可能かどうか
            if (this.delayShot <= 0)
            {
                this.shotCharge += Time.deltaTime;
            }
        }

        // チャージ終了処理
        if (Input.GetButtonUp(string.Format("Player{0} Shot", playerNo)) && IsAttackable)
        {
            CompleteShot();
        }

        // ショット処理
        /*
        if (Input.GetButtonDown(string.Format("Player{0} Shot", playerNo)) && IsAttackable)
        {
            // StartCoroutine(ShotSpray());
            if(this.delayShot <= 0)
                Shot1();
        }
        */

        //給水開始処理
        if (Input.GetButtonDown(string.Format("Player{0} Absorb", playerNo)))
        {
            controller.StartCharge();
        }

        // 給水終了処理
        if (Input.GetButtonUp(string.Format("Player{0} Absorb", playerNo)))
        {
            controller.EndCharge();
        }

        // 移動処理
        if (this.moveState == PlayerMoveState.Movable)
        {
            if (this.delayMove <= 0)
            {
                controller.Move(vec.normalized * moveSpeed);
            }
            else
            {
                this.delayMove -= Time.deltaTime;
            }
        }
        if (delayShot > 0)
        {
            this.delayShot -= Time.deltaTime;
        }
    }

    // ショットの準備完了
    private void CompleteShot()
    {
        // TODO チャージ用の処理の実装
        this.controller.Shot(this.shotCharge, this.shotParent, () =>
        {
            delayMove = 0.5f;
            delayShot = 0.5f;    
        });

        this.shotCharge = 0f;
    }

    // 通常ショット１仮
    private void Shot1()
    {
        this.controller.Shot(1f, this.shotParent, () =>
        {
            delayMove = 0.5f;
            delayShot = 0.5f;
        });
    }

    private IEnumerator ShotSpray()
    {
        float time = 0;
        this.moveState = PlayerMoveState.Attack;
        sprayShot.SetActiveEmitter(true);

        // 放出時間分待つ
        while (time < this.sprayShotTime && this.moveState == PlayerMoveState.Attack)
        {
            yield return 0;
            time += Time.deltaTime;
        }

        // yield return new WaitForSeconds(this.sprayShotTime);

        this.moveState = PlayerMoveState.Movable;
        sprayShot.SetActiveEmitter(false);
    }

    public void SetGameOver()
    {
        isPlayer = false;
        this.gameObject.SetActive(false);
    }

    public void SetPlayerNo(int receivedPlayerNo)
    {
        playerNo = receivedPlayerNo;
    }
}