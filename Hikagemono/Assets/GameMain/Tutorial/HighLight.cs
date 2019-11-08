using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighLight : MonoBehaviour {

    /// <summary>
    /// プレイヤーの座標情報
    /// </summary>
    [SerializeField]
    Transform playerTransform = null;
    public Transform PlayerTransform { get { return playerTransform; } }

    /// <summary>
    /// ハイライトを開始するX座標
    /// </summary>
    [SerializeField]
    float highLightStartX = 0.0f;
    public float HighLightStartX { get { return highLightStartX; } }

    /// <summary>
    /// 減少する色の数値の最小値
    /// </summary>
    [SerializeField]
    float minColorValue = 0.8f;
    public float MinColorValue { get { return minColorValue; } }

    /// <summary>
    /// 何秒かけて変更するか
    /// </summary>
    [SerializeField]
    float changeTime = 1.0f;
    public float ChangeTime { get { return changeTime; } }

    /// <summary>
    /// イージングの種類
    /// </summary>
    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;
    public iTween.EaseType EaseType { get { return easeType; } }


}
