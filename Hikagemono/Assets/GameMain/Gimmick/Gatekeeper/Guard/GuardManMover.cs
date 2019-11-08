using UnityEngine;
using System.Collections;

public class GuardManMover : MonoBehaviour {

    /// <summary>
    /// 状態管理クラス
    /// </summary>
    [SerializeField]
    GuardManStateManager stateManager = null;

    /// <summary>
    /// 追う対象
    /// </summary>
    [SerializeField]
    Transform followTarget = null;

    /// <summary>
    /// 追う対象との最小距離
    /// X軸
    /// </summary>
    [SerializeField]
    float distanceFromTarget = 1.0f;

    /// <summary>
    /// 移動速度の最大値
    /// </summary>
    [SerializeField]
    float moveSpeedLimit = 0.05f;

    /// <summary>
    /// 移動量の減少する割合
    /// </summary>
    [SerializeField, Range(0.0f, 1.0f)]
    float dividePower = 0.03f;

    /// <summary>
    /// X軸のスケール
    /// </summary>
    float defaultScaleX = 1.5f;


	// Use this for initialization
	void Start () {
        defaultScaleX = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {

	}


    void FixedUpdate()
    {
        if (!stateManager.IsMove) return;

        Move();
    }

    /// <summary>
    /// 移動させる
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.right * GetMoveSpeed());
    }

    /// <summary>
    /// ターゲットとの距離を得る
    /// </summary>
    /// <returns>ターゲットとの距離</returns>
    float GetDistance()
    {
        var Distance = followTarget.position.x - transform.position.x;

        TurnScale(Distance);

        if (Distance > 0.0f && Distance < distanceFromTarget) Distance = 0.0f;

        if (Distance < 0.0f && Distance > -distanceFromTarget) Distance = 0.0f;

        return Distance;
    }

    /// <summary>
    /// 移動速度を得る
    /// </summary>
    /// <returns></returns>
    float GetMoveSpeed()
    {
        var MoveSpeed = GetDistance() * dividePower;

        if (MoveSpeed > 0.0f && MoveSpeed > moveSpeedLimit) MoveSpeed = moveSpeedLimit;
        if (MoveSpeed < 0.0f && MoveSpeed < -moveSpeedLimit) MoveSpeed = -moveSpeedLimit;

        return MoveSpeed;
    }
    
    /// <summary>
    /// scaleを変更することで左右反転する
    /// 距離によって回転する具合を変化します
    /// </summary>
    /// <param name="distance">距離</param>
    void TurnScale(float distance)
    {
            
        if (distance > 0.0f && distance > distanceFromTarget) distance = distanceFromTarget;
        if (distance < 0.0f && distance < -distanceFromTarget) distance = -distanceFromTarget;

        var newScaleX = -distance / distanceFromTarget * defaultScaleX;
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
    }

}
