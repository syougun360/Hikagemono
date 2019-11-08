using UnityEngine;
using System.Collections;

public class ChildHighLight : MonoBehaviour {

    /// <summary>
    /// ハイライトするときに必要な情報を取得
    /// </summary>
    HighLight highLightInfo = null;

    /// <summary>
    /// すでに強調したかどうか
    /// </summary>
    bool alreadyHighLight = false;

    /// <summary>
    /// Destroyが呼ばれているかどうか
    /// </summary>
    bool isDying = false;

    /// <summary>
    /// スプライトレンダーの情報
    /// </summary>
    SpriteRenderer thisSprite = null;

    /// <summary>
    /// ゲーム内時間がいつ以降だったら変更されたとみるか
    /// </summary>
    [SerializeField]
    float checkGameTime = 1.0f;

	// Use this for initialization
	void Start () {
        highLightInfo = transform.parent.GetComponent<HighLight>();

        thisSprite = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        if (!alreadyHighLight && highLightInfo.PlayerTransform.position.x > highLightInfo.HighLightStartX)
        {
            HighLightStart();
            alreadyHighLight = true;
        }

        if (!isDying && alreadyHighLight && checkGameTime < TimeManager.NowTime)
        {
            HighLightEnd();
            Destroy(this, highLightInfo.ChangeTime);
            isDying = true;
        }
    }

    /// <summary>
    /// ハイライト開始の処理
    /// </summary>
    void HighLightStart()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", highLightInfo.MinColorValue, "time", highLightInfo.ChangeTime, "easetype", highLightInfo.EaseType, "looptype", iTween.LoopType.pingPong, "onupdate", "UpdateHandler"));

    }


    /// <summary>
    /// ハイライト開始の処理
    /// </summary>
    void HighLightEnd()
    {
        iTween.Stop(gameObject);
        iTween.ValueTo(gameObject, iTween.Hash("from", thisSprite.color.r, "to", 1.0f, "time", highLightInfo.ChangeTime, "easetype", highLightInfo.EaseType,  "onupdate", "UpdateHandler"));
        Destroy(this,highLightInfo.ChangeTime);

        var itween = GetComponent<iTween>();
        Destroy(itween,highLightInfo.ChangeTime);
    }

    void UpdateHandler(float value)
    {
        thisSprite.color = new Color(value, value, value, 1.0f);
    }

}
