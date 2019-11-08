using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    /// <summary>
    /// ゲーム状態一覧
    /// </summary>
    enum GameState{
        ControlPlayer,
        ControlTimeMater,
        ControlCameraStart,
    }

    /// <summary>
    /// 現在のゲーム状態を管理
    /// </summary>
    static GameState nowGameState = GameState.ControlCameraStart;

    /// <summary>
    /// プレイヤー操作状態かどうか
    /// </summary>
    public static bool IsControlPlayer { get {return( nowGameState == GameState.ControlPlayer); } }

    /// <summary>
    /// タイムメーター操作状態かどうか
    /// </summary>
    public static bool IsControlTimeMater { get { return (nowGameState == GameState.ControlTimeMater); } }

    /// <summary>
    /// カメラがスタート操作状態かどうか
    /// </summary>
    public static bool IsControlCameraStart { get { return (nowGameState == GameState.ControlCameraStart); } }

    /// <summary>
    /// プレイヤー操作状態にする
    /// </summary>
    public static void SetControlPlayer()
    {
        nowGameState = GameState.ControlPlayer;
    }

    /// <summary>
    /// タイムメーター操作状態にする
    /// </summary>
    public static void SetControlMater()
    {
        nowGameState = GameState.ControlTimeMater;
    }


    /// <summary>
    /// カメラスタート操作状態にする
    /// </summary>
    public static void SetControlCameraStart()
    {
        nowGameState = GameState.ControlCameraStart;
    }



    /// <summary>
    /// 引数に入れられたGameStateと現在のStateが同じかどうかを判断する
    /// </summary>
    /// <param name="checkState">同じかどうかをチェックするGameState</param>
    /// <returns>同じ...true 違う...false</returns>
    static bool IsSameState(GameState checkState)
    {
        if(nowGameState != checkState) return false;

        return true;
    }

}
