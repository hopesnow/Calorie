using UnityEngine;

//動作確認用
public class DebugController : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private SprayWaterParticle test;
    [SerializeField] private Collider[] tests;
    [SerializeField] private float moveSpeed = 2.0f;    // １ベクトルに対する移動速度
    [SerializeField] private bool isPlayer = true;
    [SerializeField] private int playerNo = 0;

    private void Start()
    {
        test.SetupCollision(this.playerNo);
        test.SetupTrigger(tests);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isPlayer)
            return;

        Vector3 vec = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            vec.z += 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            vec.z -= 1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vec.x -= 1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vec.x += 1;
        }

        // 放水処理
        test.SetActiveEmitter(Input.GetKey(KeyCode.Space));

        // 移動処理
        controller.Move(vec.normalized * moveSpeed);
    }
}