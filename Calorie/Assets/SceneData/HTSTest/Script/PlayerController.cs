using UnityEngine;

namespace HTSTest
{
    /// <summary>
    /// テスト用のプレイヤーコントローラー
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform shotParent;
        [SerializeField] private NormalShot shotPrefab;

        [SerializeField] private float speed = 2.0f;

        private void Awake()
        {
            
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            var vec = Vector3.zero;
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

            this.transform.localPosition += vec.normalized * speed;

            // 放水処理
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
            }
        }
    }
}