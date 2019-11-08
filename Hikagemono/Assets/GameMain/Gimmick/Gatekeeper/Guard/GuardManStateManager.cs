using UnityEngine;
using System.Collections;

public class GuardManStateManager : MonoBehaviour
{

    [SerializeField]
    GuardManAnimator animator = null;
    
    [SerializeField]
    BoxCollider collider = null;
    
    GuardManAnimator.AnimationType state = GuardManAnimator.AnimationType.Idling;

    public bool IsMove { get { return state == GuardManAnimator.AnimationType.Move; } }
    public bool IsIdling { get { return state == GuardManAnimator.AnimationType.Idling; } }
    public bool IsAttack { get { return state == GuardManAnimator.AnimationType.Attack; } }

    /// <summary>
    /// 移動状態に切り替える
    /// </summary>
    public void ChangeMove()
    {
        if (IsMove) return;

        state = GuardManAnimator.AnimationType.Move;
        animator.Play(state);
        collider.enabled = false;

    }

    /// <summary>
    /// アイドリング状態に切り替える
    /// </summary>
    public void ChangeIdling()
    {
        if (IsIdling) return;

        state = GuardManAnimator.AnimationType.Idling;
        animator.Play(state);
    }

    /// <summary>
    /// 攻撃状態に切り替える
    /// </summary>
    public void ChangeAttack()
    {
        if (IsAttack) return;

        state = GuardManAnimator.AnimationType.Attack;
        animator.Play(state);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAttack ) return;
        if (animator.IsAnimationCheckPlay()) return;

        ChangeIdling();
    }
}
