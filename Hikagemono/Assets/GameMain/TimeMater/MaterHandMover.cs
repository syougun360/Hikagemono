using UnityEngine;
using System.Collections;

public class MaterHandMover : MonoBehaviour {

    [SerializeField]
    MaterMover materMover = null;

    [SerializeField]
    PlayerStateManager playerState = null;

    /// <summary>
    /// アングルの最小値、最大値
    /// </summary>
    public const float MinAngle = -165.0f;
    public const float MaxAngle = -15;

    /// <summary>
    /// Z軸の回転量を渡す
    /// </summary>
    public float RotateZ { get { return transform.localEulerAngles.z; } }

    enum State
    {
        None,
        Move,
    };

    State state = State.None;

	// Use this for initialization
	void Start () {
        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, MaxAngle));
	}

    /// <summary>
    /// 針を調整する角度
    /// </summary>
    float addAngle = 90.0f;

	// Update is called once per frame
	void Update () {
        ControlTimeMater();

        if (!GameStateManager.IsControlPlayer) return;

        state = State.None;
    
    }

    /// <summary>
    /// タイムメーターを操作している状態
    /// </summary>
    void ControlTimeMater()
    {
        if (!GameStateManager.IsControlTimeMater) return;
        if (materMover.CanPopDown()) return;

        MoveMater();

        StratMoveMater();
        ChangeTime();

    }

    /// <summary>
    /// meterを移動する。
    /// </summary>
    void MoveMater()
    {
        if (state != State.Move) return;

        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, GetHandAngle()));
    }

    /// <summary>
    /// 時間を切り替える
    /// </summary>
    void ChangeTime()
    {
        if (state != State.Move) return;
        CallChangingTime();

        if (!Input.GetMouseButtonUp(0)) return;
        state = State.None;
        materMover.PopDown();
        CallChangeTime();
    }

    /// <summary>
    /// メーターの針を移動させる動作
    /// </summary>
    void StratMoveMater()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (state != State.None) return;

        state = State.Move;
    }

    /// <summary>
    /// マウスの座標から角度を得る
    /// </summary>
    /// <returns></returns>
    float GetHandAngle()
    {
        var result = Mathf.Atan2(Screen.width/2 - TapManager.ScreenPosition.x,TapManager.ScreenPosition.y);
        result *= 180.0f / Mathf.PI;

        result -= addAngle;

        CheckAngleLimit(ref result);

        return result;
    }


    /// <summary>
    /// アングルの最小値、最大値を超えていないかのチェックをして返す
    /// </summary>
    /// <param name="angle">チェックする角度の変数の参照</param>
    void CheckAngleLimit(ref float angle)
    {
        if (angle < MinAngle) angle = MinAngle;
        if (angle > MaxAngle) angle = MaxAngle;
    }

    /// <summary>
    /// 時間を変えるようにTimeManagerを呼びます
    /// </summary>
    void CallChangeTime()
    {
        var degRotaZ = transform.localEulerAngles.z;
        var calcTime = MyMath.CalcMap(degRotaZ, 360 + MaxAngle, 360 + MinAngle, TimeManager.MinTimeRange, TimeManager.MaxTimeRange);
        
        TimeManager.ChangeTimeTo(calcTime);
        playerState.ChangeTime();

    }

    /// <summary>
    /// 時間を変えるようにTimeManagerを呼びます
    /// </summary>
    void CallChangingTime()
    {
        var degRotaZ = transform.localEulerAngles.z;
        var calcTime = MyMath.CalcMap(degRotaZ, 360 + MaxAngle, 360 + MinAngle, TimeManager.MinTimeRange, TimeManager.MaxTimeRange);

        TimeManager.ChangingTimeUpdate(calcTime);

    }


}
