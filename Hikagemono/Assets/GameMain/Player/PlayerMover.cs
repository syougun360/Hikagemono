using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    float moveSpeed = 5.0f;

    [SerializeField]
    float tarnTime = 1.0f;

    [SerializeField]
    PlayerStateManager stateManager = null;
    
    /// <summary>
    /// タップされたかどうか
    /// </summary>
    bool activeTapMove = false;
    bool activeKeyMove = false;

    /// <summary>
    /// タップが開始された場所
    /// 初期値は-1,-1
    /// </summary>
    Vector2 beginTapPosition = -Vector2.one;
    Vector2 touchPos = Vector2.zero;

    /// <summary>
    /// マウスの移動量をキャラクターの移動量に変換する際の減少する割合
    /// </summary>
    [SerializeField, Range(0.0f, 1.0f)]
    float devideVelocity = 0.5f;


    enum Direction
    {
        Left = -1,
        Right = 1,
    };

    float velocity = 0;
    Direction dir = Direction.Right;
    Touch touch;

    /// <summary>
    /// 今左方向なのかどうか
    /// </summary>
    public bool IsNowDirectionLeft { get { return (dir == Direction.Left); } }

    void Update()
    {
        ActiveMove();
        MoveByInput();
    }

    void MoveByInput()
    {
        if (!GameStateManager.IsControlPlayer) return;
        if (stateManager.IsChangeTime ||
            stateManager.IsShadowInstanceCreate ||
            stateManager.IsDamage) return;

        MoveByKeyboard();
        MoveByTap();
        Animation();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * velocity);
    }

    void Turn(float value)
    {
        if (value < 0) dir = Direction.Left;
        if (value > 0) dir = Direction.Right;
        iTween.ScaleTo(gameObject, iTween.Hash("x", (float)dir, "time", tarnTime));
    }

    /// <summary>
    /// キーボードによる左右の移動
    /// </summary>
    void MoveByKeyboard()
    {
        activeKeyMove = Input.GetAxis("Horizontal") == 0.0f ? false : true;
    }

    void Animation()
    {
        if (stateManager.IsJump) return;

        if (velocity == 0)
        {
            stateManager.ChangeIdling();
        }
        else if (stateManager.IsIdling)
        {
            stateManager.ChangeMove();
        }
    }

    /// <summary>
    /// タッチの座標を設定する
    /// </summary>
    Vector2 TouchPosition()
    {
        var pos = touch.position;
        pos.y = 0;

        return pos;
    }

    /// <summary>
    /// タッチが移動しているときの処理
    /// </summary>
    void TouchMove()
    {
        if (touch.phase != TouchPhase.Moved) return;

        touchPos = TouchPosition();
        if (Vector2.Distance(touchPos, beginTapPosition) <= 1)
        {
            beginTapPosition = TouchPosition();
        }
    }

    /// <summary>
    /// タッチが押した瞬間のときの処理
    /// </summary>
    void TouchBegan()
    {
        if (activeTapMove) return;
        if (touch.phase != TouchPhase.Began) return;

        if (touch.position.x < Screen.width / 2)
        {
            activeTapMove = true;
            beginTapPosition = TouchPosition();
            touchPos = TouchPosition();
        }
    }

    /// <summary>
    /// タッチが離れたときの処理
    /// </summary>
    void TouchEnded()
    {
        if (touch.phase != TouchPhase.Ended) return;

        activeTapMove = false;
        velocity = 0;
    }


    Touch GetTouch()
    {
        var touchID = !activeTapMove && Input.touchCount >= 2 ? 1 : 0;
        return Input.GetTouch(touchID);
    }

    void MoveByTap()
    {
        if (Input.touchCount > 0)
        {
            touch = GetTouch();
            TouchBegan();
        }
    }

    void ActiveMove()
    {
        ActiveKey();
        ActiveTap();

        velocity = !GameStateManager.IsControlPlayer || stateManager.IsShadowInstanceCreate ? 0 : velocity;

        Turn(velocity);

    }


    void ActiveKey()
    {
        if (!activeKeyMove) return;

        MoveByKeyboard();

        velocity = Input.GetAxis("Horizontal") * moveSpeed;

    }

    void ActiveTap()
    {
        if (!activeTapMove) return;

        velocity = devideVelocity * (touchPos.x - beginTapPosition.x);

        if (velocity > moveSpeed) velocity = moveSpeed;
        if (velocity < -moveSpeed) velocity = -moveSpeed;

        TouchEnded();
        TouchMove();


    }

    /// <summary>
    /// 画面の左半分でタップされたかどうか
    /// </summary>
    /// <returns>された...true されていない...false</returns>
    bool IsTapLeftSide()
    {
        if (TapManager.TapDown() && TapManager.ScreenPosition.x < Screen.width / 2)
        {
            return true;
        }

        return false;
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "Floor")
        {
            velocity = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Attacker")
        {
            activeTapMove = false;
            activeKeyMove = false;
        }
    }


}
