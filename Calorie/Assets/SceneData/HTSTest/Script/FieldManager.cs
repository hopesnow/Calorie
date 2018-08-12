using UnityEngine;

public class FieldManager : MonoBehaviour
{
    // いろんな場所で参照されるオブジェクト群
    public Camera camera2d;
    public Camera camera3d;
    public Canvas uiCanvas;
    public RectTransform uiCanvasRect;
    public Transform uiParent;
    public FountainManager fountain;

    private int playerNum = 2;
    public int PlayerNum
    {
        get
        {
            return playerNum;
        }
        set
        {
            playerNum = value;
            if (playerNum > 4) playerNum = 4;
            if (playerNum < 2) playerNum = 2;
        }
    }
    public int[] selectPlayerCharacter;

    private static FieldManager instance;
    public static FieldManager Instance { get { return instance; } }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
