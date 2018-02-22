#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// InputManagerを自動的に設定してくれるクラス
/// </summary>
public class InputManagerSetter
{


    /// <summary>
    /// インプットマネージャーを再設定します。
    /// </summary>
    [MenuItem("Util/Reset InputManager")]
    public static void ResetInputManager()
    {
        Debug.Log("インプットマネージャーの設定を開始します。");
        InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

        Debug.Log("設定を全てクリアします。");
        inputManagerGenerator.Clear();

        Debug.Log("プレイヤーごとの設定を追加します。");
        for (int i = 0; i < 4; i++)
        {
            AddPlayerInputSettings(inputManagerGenerator, i);
        }

        Debug.Log("グローバル設定を追加します。");
        AddGlobalInputSettings(inputManagerGenerator);

        Debug.Log("インプットマネージャーの設定が完了しました。");
    }

    /// <summary>
    /// グローバルな入力設定を追加する（OK、キャンセルなど）
    /// </summary>
    /// <param name="inputManagerGenerator">Input manager generator.</param>
    private static void AddGlobalInputSettings(InputManagerGenerator inputManagerGenerator)
    {

        // 横方向
        {
            var name = "Horizontal";
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "left", "right"));
        }

        // 縦方向
        {
            var name = "Vertical";
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 2));
            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "s", "w", "down", "up"));
        }

        // 決定
        {
            var name = "OK";
            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "z", "joystick button 0"));
        }

        // キャンセル
        {
            var name = "Cancel";
            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "x", "joystick button 1"));
        }

        // ポーズ
        {
            var name = "Pause";
            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "escape", "joystick button 7"));
        }
    }

    /// <summary>
    /// プレイヤーごとの入力設定を追加する
    /// </summary>
    /// <param name="inputManagerGenerator">Input manager generator.</param>
    /// <param name="playerIndex">Player index.</param>
    private static void AddPlayerInputSettings(InputManagerGenerator inputManagerGenerator, int playerIndex)
    {
        if (playerIndex < 0 || playerIndex > 3) Debug.LogError("プレイヤーインデックスの値が不正です。");
        string upKey = "", downKey = "", leftKey = "", rightKey = "", jumpKey = "", absorbKey = "", shotKey = "";
        GetAxisKey(out upKey, out downKey, out leftKey, out rightKey, out jumpKey, out absorbKey, out shotKey, playerIndex);

        int joystickNum = playerIndex + 1;

        // 横方向
        {
            var name = string.Format("Player{0} Horizontal", playerIndex);
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 1));
            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, leftKey, rightKey, "", ""));
        }

        // 縦方向
        {
            var name = string.Format("Player{0} Vertical", playerIndex);
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 2));
            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, downKey, upKey, "", ""));
        }


        // ジャンプ
        {
            var axis = new InputAxis();
            var name = string.Format("Player{0} Jump", playerIndex);
            var button = string.Format("joystick {0} button 0", joystickNum);
            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, jumpKey));
        }
        // 吸い込む
        {
            var axis = new InputAxis();
            var name = string.Format("Player{0} Absorb", playerIndex);
            var button = string.Format("joystick {0} button 2", joystickNum);
            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, absorbKey));
        }
        // ショット
        {
            var axis = new InputAxis();
            var name = string.Format("Player{0} Shot", playerIndex);
            var button = string.Format("joystick {0} button 1", joystickNum);
            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, shotKey));
        }
    }

    /// <summary>
    /// キーボードでプレイした場合、割り当たっているキーを取得する
    /// </summary>
    /// <param name="upKey">Up key.</param>
    /// <param name="downKey">Down key.</param>
    /// <param name="leftKey">Left key.</param>
    /// <param name="rightKey">Right key.</param>
    /// <param name="attackKey">Attack key.</param>
    /// <param name="playerIndex">Player index.</param>
    private static void GetAxisKey(out string upKey, out string downKey, out string leftKey, out string rightKey, out string jumpKey, out string absorbKey, out string shotKey, int playerIndex)
    {
        upKey = "";
        downKey = "";
        leftKey = "";
        rightKey = "";
        jumpKey = "";
        absorbKey = "";
        shotKey = "";

        switch (playerIndex)
        {
            case 0:
                upKey = "w";
                downKey = "s";
                leftKey = "a";
                rightKey = "d";
                jumpKey = "z";
                absorbKey = "q";
                shotKey = "e";
                break;
            case 1:
                upKey = "i";
                downKey = "k";
                leftKey = "j";
                rightKey = "l";
                jumpKey = "m";
                absorbKey = "u";
                shotKey = "o";
                break;
            case 2:
                upKey = "up";
                downKey = "down";
                leftKey = "left";
                rightKey = "right";
                jumpKey = "[0]";
                absorbKey = "n";
                shotKey = "m";
                break;
            case 3:
                upKey = "[8]";
                downKey = "[5]";
                leftKey = "[4]";
                rightKey = "[6]";
                jumpKey = "[1]";
                absorbKey = "[7]";
                shotKey = "[9]";
                break;
            default:
                Debug.LogError("プレイヤーインデックスの値が不正です。");
                upKey = "";
                downKey = "";
                leftKey = "";
                rightKey = "";
                jumpKey = "";
                absorbKey = "";
                shotKey = "";
                break;
        }
    }
}

#endif