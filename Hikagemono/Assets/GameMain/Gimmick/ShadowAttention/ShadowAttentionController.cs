using UnityEngine;
using System.Collections;

public class ShadowAttentionController : MonoBehaviour {

    [SerializeField]
    float oneLapRotateTime = 5.0f;

    [SerializeField]
    float rotateY = 300.0f;

    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;

    SpriteRenderer sprite = null;
    InTimeInstanceCreator creator = null;
    
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        creator = transform.parent.GetComponent<InTimeInstanceCreator>();
        sprite.enabled = false;
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", rotateY, "time", oneLapRotateTime,
            "looptype", iTween.LoopType.loop,"easetype", easeType,"onupdate", "UpdateHandler"));

	}

    void UpdateHandler(float value)
    {
        transform.rotation = Quaternion.Euler(
            new Vector3(transform.rotation.x, value, transform.rotation.z));
    }

	// Update is called once per frame
	void Update () 
    {
        StartDraw();
        StopDraw();
        DestroyAttention();
	}

    /// <summary>
    /// 描画を開始する
    /// </summary>
    void StartDraw()
    {
        if (sprite.enabled) return;
        if (!TimeManager.IsInChangingTime(creator.BeginTime, creator.EndTime)) return;

        sprite.enabled = true;
    }

    /// <summary>
    /// 描画を停止する
    /// </summary>
    void StopDraw()
    {
        if (!sprite.enabled) return;
        if (TimeManager.IsInChangingTime(creator.BeginTime, creator.EndTime)) return;

        sprite.enabled = false;
    }

    /// <summary>
    /// 削除する。
    /// </summary>
    void DestroyAttention()
    {
        if (!creator.IsDraw) return;

        Destroy(gameObject);
    }

}
