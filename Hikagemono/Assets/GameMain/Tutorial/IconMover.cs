using UnityEngine;
using System.Collections;

public class IconMover : MonoBehaviour {


    /// <summary>
    /// 表示するものの元となるオブジェクト
    /// </summary>
    [SerializeField]
    Transform origin = null;

    /// <summary>
    /// 元の物からのX軸の距離
    /// </summary>
    [SerializeField]
    float distanceFromOriginX = 0.0f;

    /// <summary>
    /// 元の物からのY軸の距離
    /// </summary>
    [SerializeField]
    float distanceFromOriginY = 0.0f;

    /// <summary>
    /// 上下に移動する幅
    /// </summary>
    [SerializeField]
    float shakeDistance = 0.0f;

    /// <summary>
    /// 回転する角度
    /// </summary>
    [SerializeField]
    float rotateAngle = 5.0f;

    /// <summary>
    /// 上下移動にかける時間
    /// </summary>
    [SerializeField]
    float moveTime = 1.0f;

    /// <summary>
    /// 上下移動のイージングタイプ
    /// </summary>
    [SerializeField]
    iTween.EaseType moveEaseType = iTween.EaseType.linear;

	// Use this for initialization
	void Start () {
        SetPosition();
        SetRotate();

        Move();
        Turn();

	}

    void SetPosition()
    {
        transform.position = origin.position;
        transform.Translate(new Vector3(distanceFromOriginX, distanceFromOriginY, 0.0f));
    }

    void SetRotate()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        Turn();
	}

    void Turn()
    {
        transform.Rotate(Vector3.up, rotateAngle);
    }

    void Move()
    {
        iTween.MoveAdd(gameObject,iTween.Hash("y",shakeDistance,"time",moveTime,"easetype",moveEaseType,"looptype",iTween.LoopType.pingPong));
    }

}
