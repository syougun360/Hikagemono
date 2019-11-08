using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

    /// <summary>
    /// カメラが追うオブジェクト
    /// </summary>
    [SerializeField]
    PlayerMover targetObject = null;

    /// <summary>
    /// 移動量の減少する割合
    /// </summary>
    [SerializeField, Range(0.0f, 1.0f)]
    float dividePower = 0.5f;

    float velocity = 0;

    enum State
    {
        Stop,
        Move,
        Deceleration,
    };

    State state = State.Stop;

	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () 
    {
        ChangeMoveState();
        //ChangeDecelerationState();


    }

    public void Stop()
    {
        ChangeStopState();
    }

    void FixedUpdate()
    {
        Move();
        //Deceleration();

        transform.Translate(new Vector3(velocity, 0, 0));
    }
    
    /// <summary>
    /// 移動状態に変化
    /// </summary>
    void ChangeMoveState()
    {
        //if (targetObject.IsNowDirectionLeft) return;
        if (targetObject.transform.position.x <= transform.position.x) return;

        state = State.Move;
    }


    /// <summary>
    /// 減速状態に変化
    /// </summary>
    void ChangeDecelerationState()
    {
        if (!targetObject.IsNowDirectionLeft) return;
        
        state = State.Deceleration;
    }

    /// <summary>
    /// 減速状態に変化
    /// </summary>
    void ChangeStopState()
    {
        state = State.Stop;
        velocity = 0;
    }


    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        if (state != State.Move) return;

        var distance = targetObject.transform.position.x - transform.position.x;

        velocity = dividePower * distance;
    }

    /// <summary>
    /// 減速
    /// </summary>
    void Deceleration()
    {
        if (state != State.Deceleration) return;

        velocity *= 0.95f;
    }
}
