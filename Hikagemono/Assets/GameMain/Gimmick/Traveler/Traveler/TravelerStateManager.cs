using UnityEngine;
using System.Collections;

public class TravelerStateManager : MonoBehaviour{

    [SerializeField]
    TravelerAnimator animator = null;


    /// <summary>
    /// 掃除人の参照
    /// </summary>
    [SerializeField]
    SweeperStateManager sweeper = null;

    /// <summary>
    /// 道を塞ぐ用の当たり判定
    /// </summary>
    [SerializeField]
    BoxCollider bagCollider = null;

    /// <summary>
    /// 移動するZ軸の移動量
    /// </summary>
    [SerializeField]
    float moveValueZ = 1.0f;

    /// <summary>
    /// 移動にかける時間
    /// </summary>
    [SerializeField]
    float moveTime = 1.0f;

    /// <summary>
    /// 移動時のイージングの種類
    /// </summary>
    [SerializeField]
    iTween.EaseType moveEaseType = iTween.EaseType.linear;

    TravelerAnimator.AnimationType state = TravelerAnimator.AnimationType.Idling;

    public bool IsIdling { get { return state == TravelerAnimator.AnimationType.Idling; } }
    public bool IsWatching { get { return state == TravelerAnimator.AnimationType.WatchingMap; } }

    public bool IsMoved { get; private set; }

    /// <summary>
    /// 移動後状態に切り替える
    /// </summary>
    public void ChangeWatching()
    {
        if (IsWatching) return;
        state = TravelerAnimator.AnimationType.WatchingMap;
        //animator.Play(state);
        bagCollider.enabled = false;
        Move(true);
    }

    /// <summary>
    /// アイドリング状態に切り替える
    /// </summary>
    public void ChangeIdling()
    {
        if (IsIdling) return;

        state = TravelerAnimator.AnimationType.Idling;
        //animator.Play(state);
        bagCollider.enabled = true;
        Move(false);
    }

    void Start()
    {
        IsMoved = false;
    }

    void Update()
    {
        //if (animator.IsAnimationCheckPlay()) return;
        CheckState();

    }
   
    /// <summary>
    /// stateの変化を見る
    /// </summary>
    void CheckState()
    {
        if (sweeper.IsMoved && sweeper.IsIdling)
        {
            ChangeWatching();
            return;
        }

        ChangeIdling();

    }

    /// <summary>
    /// 移動させる
    /// </summary>
    /// <param name="toDepth">画面奥に向かう...true 画面手前に移動する...false</param>
    void Move(bool toDepth)
    {
        var Velocity = 1.0f;
        if (!toDepth) Velocity *= -1.0f;

        if(IsMoved != toDepth)
        {
            iTween.MoveTo(gameObject, iTween.Hash("z", transform.position.z + (Velocity * moveValueZ), "time", moveTime, "easetype", moveEaseType));
            IsMoved = !IsMoved;
        }
    }


}
