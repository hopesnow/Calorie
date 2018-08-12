using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FountainManager : MonoBehaviour
{
    [SerializeField] private List<Transform> randomList;

    private bool[] reserveList;

    private int fountainCount = 1;    // 噴水同時存在数

    // 空きスペースを取得
    public int GetFreeIndex()
    {
        Debug.Assert(this.randomList.Count >= this.fountainCount);

        int index = Random.Range(0, this.randomList.Count - 1);
        bool searching = true;
        while (searching)
        {
            if (this.reserveList[index])
            {
                // 予約済みなら次のindexへ
                index++;
                if (index >= this.reserveList.Length)
                {
                    index -= this.reserveList.Length;
                }
            }
            else
            {
                searching = false;
            }
        }

        return index;
    }

    // 予約状態の設定
    public void SetReserve(int index, bool reserve)
    {
        this.reserveList[index] = reserve;
    }

    // 座標の取得
    public Vector3 GetPosition(int index)
    {
        return this.randomList[index].localPosition;
    }

    // 初期化処理
	private void Start ()
    {
        this.reserveList = new bool[this.randomList.Count];
	}
	
    // 更新処理
	private void Update ()
    {
	}
}
