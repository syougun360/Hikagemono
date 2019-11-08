using UnityEngine;
using System.Collections;

public class BaseTutorial : MonoBehaviour
{

    /// <summary>
    /// プレイヤーを登録する
    /// </summary>
    [SerializeField]
    protected Transform player = null;

    /// <summary>
    /// プレイヤーがどこまで行っていたらチュートリアルを表示するか
    /// </summary>
    [SerializeField]
    protected float showTutorialStartX = 0.0f;

    /// <summary>
    /// プレイヤーがどこまでいっていたらチュートリアルを消すかどうか
    /// </summary>
    [SerializeField]
    protected float showTutorialLimitX = 5.0f;

    /// <summary>
    /// 動きのループ時間
    /// </summary>
    [SerializeField]
    protected float loopTime = 0.5f;


    /// <summary>
    /// 動きのイージングの種類
    /// </summary>
    [SerializeField]
    protected iTween.EaseType moveEaseType = iTween.EaseType.linear;


    /// <summary>
    /// アイコンが消滅する時間
    /// </summary>
    [SerializeField]
    protected float dyingTime = 0.2f;

    /// <summary>
    /// フェードアウトのイージングの種類
    /// </summary>
    [SerializeField]
    protected iTween.EaseType fadeoutEaseType = iTween.EaseType.linear;


    /// <summary>
    /// 消すかどうか
    /// </summary>
    protected bool isDestroy = false;

    /// <summary>
    /// 親のWidthとHeight
    /// </summary>
    protected Vector2 parentSize = Vector2.zero;

    /// <summary>
    /// iTween等の初期化済かどうか
    /// </summary>
    protected bool alreadyInitialized = false;

    /// <summary>
    /// キャンバスのスケールを得る
    /// </summary>
    protected void SetParentSize()
    {
        parentSize.x = transform.parent.GetComponent<RectTransform>().rect.width;
        parentSize.y = transform.parent.GetComponent<RectTransform>().rect.height;
    }

    /// <summary>
    /// 初期位置設定
    /// </summary>
    protected virtual void SetStartPosition()
    {
    }

    /// <summary>
    /// チュートリアルを消すかどうかのチェック
    /// </summary>
    protected void CheckShowTutorial()
    {
        if (!isDestroy && player.position.x >= showTutorialLimitX)
        {
            isDestroy = true;
        }
    }

    /// <summary>
    /// iTweenを使用したフェードアウト
    /// </summary>
    /// <param name="alphaValue">変更後のalpha値</param>
    protected void FadeTo(float alphaValue)
    {
        iTween.FadeTo(gameObject, iTween.Hash("a", alphaValue, "time", dyingTime, "easetype", fadeoutEaseType));
    }

    /// <summary>
    /// 初期化
    /// </summary>
    protected virtual void Initalize()
    {
        SetStartPosition();
        GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        FadeTo(1.0f);
    }

    /// <summary>
    /// 削除時の処理
    /// </summary>
    protected void DestroyActions()
    {
        Destroy(gameObject, dyingTime);

        var increaceValue = 1.3f;
        iTween.ScaleTo(gameObject, iTween.Hash("x", transform.localScale.x * increaceValue,
                                            "y", transform.localScale.y * increaceValue,
                                            "z", transform.localScale.z * increaceValue,
                                            "time", dyingTime, "easetype", fadeoutEaseType));

        FadeTo(0.0f);
    }


}
