/// ----------------------------------------------------
/// ステージインターフェイス
/// 
/// 初期化と終了処理等
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class IStageManager : MonoBehaviour {

    [SerializeField]
    Transform nextPoint = null;

    [SerializeField]
    Transform player = null;

    [SerializeField]
    float nextDistance = 2.0f;

    [SerializeField]
    string bgmName = string.Empty;

    [SerializeField]
    FadeTimeData fadeTime = new FadeTimeData(1,1);

    enum State
    {
        Init,
        Update,
        Finish,
        Next,
    };

    State state = State.Init;

    void Awake()
    {
    }

    void Start()
    {
        GameStateManager.SetControlCameraStart();
        BGMPlayer.Instance.Play(bgmName, fadeTime);
	}

    /// <summary>
    /// 初期化
    /// </summary>
    void InitHandler()
    {
        if (state != State.Init) return;

        Init();
        state = State.Update;
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void FinishHandler()
    {
        if (state != State.Finish) return;

        Finish();
        state = State.Next;
        BGMPlayer.Instance.Stop();
        SEPlayer.Instance.AllDestroy();
    }

    /// <summary>
    /// 初期化
    /// コールバック形式
    /// </summary>
    protected virtual void Init()
    {

    }

    /// <summary>
    /// 終了処理
    /// コールバック形式
    /// </summary>
    protected virtual void Finish()
    {

    }

    void OnNextStagePoint()
    {
        if (state != State.Update) return;

        if (Vector3.Distance(nextPoint.position,player.position) <= nextDistance)
        {
            state = State.Finish;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InitHandler();
        OnNextStagePoint();
        FinishHandler();
    }
}
