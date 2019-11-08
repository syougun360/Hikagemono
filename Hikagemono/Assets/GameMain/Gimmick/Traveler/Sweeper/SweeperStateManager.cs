using UnityEngine;
using System.Collections;

public class SweeperStateManager : MonoBehaviour {

    [SerializeField]
    SweeperAnimator animator = null;
    
    /// <summary>
    /// 影の脚立
    /// </summary>
    [SerializeField]
    MeshRenderer shadowStepladder = null;

    SweeperAnimator.AnimationType state = SweeperAnimator.AnimationType.Idling;

    /// <summary>
    /// 移動するX軸の移動量
    /// </summary>
    [SerializeField]
    float moveValueX = 1.0f;

    /// <summary>
    /// ジャンプする高さ
    /// </summary>
    [SerializeField]
    float moveValueY = 0.5f;

    /// <summary>
    /// 移動にかける時間
    /// </summary>
    [SerializeField]
    float moveTime = 1.0f;

    /// <summary>
    /// 移動時の左右イージングの種類
    /// </summary>
    [SerializeField]
    iTween.EaseType moveEaseType = iTween.EaseType.linear;

    /// <summary>
    /// 移動時の上昇イージングの種類
    /// </summary>
    [SerializeField]
    iTween.EaseType upEaseType = iTween.EaseType.linear;

    /// <summary>
    /// 移動時の下降イージングの種類
    /// </summary>
    [SerializeField]
    iTween.EaseType downEaseType = iTween.EaseType.linear;





    public bool IsIdling { get { return state == SweeperAnimator.AnimationType.Idling; } }
    public bool IsJump { get { return state == SweeperAnimator.AnimationType.Jump; } }
    public bool IsLeanRight { get { return state == SweeperAnimator.AnimationType.LeanRight; } }

    public bool IsMoved {get; private set;}


    [SerializeField]
    float activeBeginTime = 0.0f;

    [SerializeField]
    float activeEndTime = 0.0f;


    /// <summary>
    /// 初期の高さを保存
    /// </summary>
    float defaultHeight = 0.0f;

    /// <summary>
    /// 普通に拭いている状態にする
    /// </summary>
    public void ChangeIdling ()
    {
        if (IsIdling) return;

        SetState(SweeperAnimator.AnimationType.Idling);

        //animator.Play(nowState);
    }

    /// <summary>
    /// 脚立から脚立に移動している状態にする
    /// </summary>
    public void ChangeJump()
    {
        if (IsJump) return;

        SetState(SweeperAnimator.AnimationType.Jump);
        //animator.Play(nowState);

        JumpingMove();

    }

    /// <summary>
    /// 脚立から身を乗り出して掃除をしている時間にする
    /// </summary>
    public void ChangeLeanRight()
    {
        if (IsLeanRight) return;

        SetState(SweeperAnimator.AnimationType.LeanRight);
        //animator.Play(nowState);
    }

    /// <summary>
    /// 座標を調整する
    /// </summary>
    void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, defaultHeight, transform.position.z);
    }

    /// <summary>
    /// stateを変化させる
    /// </summary>
    /// <param name="_newState">変化させたいstate</param>
    void SetState (SweeperAnimator.AnimationType _newState)
    {
        ResetPosition();

        state = _newState;
    }

	// Use this for initialization
	void Start () {
        defaultHeight = transform.position.y;
        IsMoved = false;
	}


	
	// Update is called once per frame
	void Update () {
        //if (animator.IsAnimationCheckPlay()) return;
    
        CheckState();


    }
    

    /// <summary>
    /// 状態の変更を確認する
    /// </summary>
    void CheckState()
    {
        if (!TimeManager.IsInTime(activeBeginTime, activeEndTime))
        {
            if (!IsMoved)
            {
                ChangeIdling();
            }
            else if(shadowStepladder.enabled)
            {
                ChangeJump();
            }
        }
        else
        {
            if (IsMoved)
            {
                ChangeIdling();
            }
            else
            {
                if (shadowStepladder.enabled)
                {

                    ChangeJump();
                }
                else
                {
                    ChangeLeanRight();

                }
            }

        }
    }

    /// <summary>
    /// 飛んで移動する
    /// </summary>
    void JumpingMove()
    {
        ///Y軸移動
        iTween.MoveAdd(gameObject, iTween.Hash("y", moveValueY, "time", moveTime / 2, "easetype", upEaseType));
        iTween.MoveAdd(gameObject, iTween.Hash("y", -moveValueY, "delay", moveTime / 2, "time", moveTime / 2, "easetype", downEaseType));

        ///X軸移動
        if(!IsMoved)
        {
            iTween.MoveBy(gameObject, iTween.Hash("x",moveValueX, "time", moveTime, "easetype", moveEaseType));
        }
        else
        {
            iTween.MoveBy(gameObject, iTween.Hash("x",-moveValueX, "time", moveTime, "easetype", moveEaseType));
        }

        IsMoved = !IsMoved;
    }
}
