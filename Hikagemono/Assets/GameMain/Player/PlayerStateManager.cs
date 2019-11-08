/// ----------------------------------------------------
/// プレイヤー状態管理
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour {

    [SerializeField]
    PlayerAnimator animator = null;

    [SerializeField]
    string seWalkName = string.Empty;

    [SerializeField]
    string seJumpName = string.Empty;

    [SerializeField]
    string seDamageName = string.Empty;

    [SerializeField]
    string seChangeTimeName = string.Empty;
    
    [SerializeField]
    string seShadowInstanceCreateName = string.Empty;
    
    PlayerAnimator.AnimationType state = PlayerAnimator.AnimationType.Idling;

    public bool IsMove { get { return state == PlayerAnimator.AnimationType.Move; } }
    public bool IsIdling { get { return state == PlayerAnimator.AnimationType.Idling; } }
    public bool IsDamage { get { return state == PlayerAnimator.AnimationType.Damage; } }
    public bool IsJump { get { return state == PlayerAnimator.AnimationType.Jump; } }
    public bool IsChangeTime { get { return state == PlayerAnimator.AnimationType.ChangeTime; } }
    public bool IsShadowInstanceCreate { get { return state == PlayerAnimator.AnimationType.ShadowInstanceCreate; } }

    /// <summary>
    /// 移動状態に切り替える
    /// </summary>
    public void ChangeMove()
    {
        if (IsMove) return;

        state = PlayerAnimator.AnimationType.Move;
        animator.Play(state);
        SEPlayer.Instance.Play(seWalkName, 0.6f, true);
    }

    /// <summary>
    /// アイドリング状態に切り替える
    /// </summary>
    public void ChangeIdling()
    {
        if (IsIdling) return;

        state = PlayerAnimator.AnimationType.Idling;
        animator.Play(state);
        SEPlayer.Instance.Stop(seWalkName);

    }

    /// <summary>
    /// 時間を切り替える状態に切り替える
    /// </summary>
    public void ChangeTime()
    {
        if (IsChangeTime) return;

        state = PlayerAnimator.AnimationType.ChangeTime;
        animator.Play(state);
        SEPlayer.Instance.Play(seChangeTimeName);
        SEPlayer.Instance.Stop(seWalkName);
    }

    /// <summary>
    /// ダメージ状態に切り替える
    /// </summary>
    public void ChangeDamage()
    {
        if (IsDamage) return;

        state = PlayerAnimator.AnimationType.Damage;
        animator.Play(state);
        SEPlayer.Instance.Play(seDamageName);
        SEPlayer.Instance.Stop(seWalkName);
    }


    /// <summary>
    /// ジャンプ状態に切り替える
    /// </summary>
    public void ChangeJump()
    {
        if (IsJump) return;

        state = PlayerAnimator.AnimationType.Jump;
        animator.Play(state);
        SEPlayer.Instance.Play(seJumpName);
        SEPlayer.Instance.Stop(seWalkName);

    }

    /// <summary>
    /// 影を具現化する状態に切り替える
    /// </summary>
    public void ChangeShadow()
    {
        if (IsShadowInstanceCreate) return;

        state = PlayerAnimator.AnimationType.ShadowInstanceCreate;
        animator.Play(state);
        SEPlayer.Instance.Play(seShadowInstanceCreateName);
        SEPlayer.Instance.Stop(seWalkName);

    }
	
	
	// Update is called once per frame
	void Update () 
    {
        if (!IsChangeTime && !IsShadowInstanceCreate && !IsDamage && !IsJump) return;
        if (animator.IsAnimationCheckPlay()) return;

        ChangeIdling();
	}

}
