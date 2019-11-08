using UnityEngine;
using System.Collections;

public class PlayerJumper : MonoBehaviour {

    /// <summary>
    /// ジャンプ実行時のジャンプする力
    /// </summary>
    [SerializeField]
    float jumpPower = 0.1f;

    [SerializeField]
    PlayerStateManager playerState = null;

    InTimeInstanceCreator[] showdowInstances = null;

    bool isJumping = false;

    Rigidbody rigidBody = null;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        showdowInstances = GameObject.FindObjectsOfType<InTimeInstanceCreator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!GameStateManager.IsControlPlayer) return;

        InputEvent();
    }

    void TouchBegan(Touch touch)
    {
        if (touch.phase != TouchPhase.Began) return;

        if (touch.position.x > Screen.width / 2)
        {
            RigidBodyToJump();
        }
    }

    /// <summary>
    /// ジャンプができるかどうか
    /// </summary>
    /// <returns></returns>
    bool CanJumping()
    {
        if (isJumping) return false;
        if (playerState.IsShadowInstanceCreate) return false;
        if (playerState.IsChangeTime) return false;

        foreach (var obj in showdowInstances)
        {
            if (obj.IsInstance()) return false;
        }
        
        return true;

    }

    /// <summary>
    /// 入力イベント
    /// </summary>
    void InputEvent()
    {
        if (!CanJumping()) return;

        if (Input.touchCount > 0)
        {
            var touchID = Input.touchCount == 1 ? 0 : 1;
            var touch = Input.GetTouch(touchID);
            TouchBegan(touch);
        }

        if (Input.GetKeyDown(KeyCode.Space) /*|| IsTapRightSide()*/ )
        {
            RigidBodyToJump();
        }
    }

    /// <summary>
    /// RigidBodyからジャンプをさせる
    /// </summary>
    void RigidBodyToJump()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        isJumping = true;
        playerState.ChangeJump();
    }

    /// <summary>
    /// 画面右半分でタップされたかどうか
    /// </summary>
    /// <returns>された...true されていない...false</returns>
    bool IsTapRightSide()
    {
        if (TapManager.TapDown() && TapManager.ScreenPosition.x > Screen.width / 2)
        {
            return true;
            
        }

        return false;

    }

    public void JumpingNonEnable()
    {
        if (!isJumping) return;

        isJumping = false;
    }

}
