using UnityEngine;
using System.Collections;

public class ShadowMover : MonoBehaviour {


    /// <summary>
    /// 影の発生源であるオブジェクトを指定する
    /// </summary>
    [SerializeField]
    Transform parentObj = null;

    /// <summary>
    /// 影を検知する最大距離
    /// </summary>
    [SerializeField]
    float maxRayDistance = 30.0f;

    /// <summary>
    /// 影を検知する最小距離
    /// </summary>
    [SerializeField]
    float minRayDistance = 1.0f;

    /// <summary>
    /// 影と母体との距離の調整する値
    /// </summary>
    [SerializeField]
    float addDistance = 0.0f;

    /// <summary>
    /// 具現化するオブジェクトのタグ名
    /// </summary>
    [SerializeField]
    string realizationObjectTag = string.Empty;

    [SerializeField]
    JumpTrigger jumpTrigger = null;

    /// <summary>
    /// 最初のサイズ
    /// </summary>
    Vector3 defaultScale = Vector3.one;

	// Use this for initialization
	void Start () {
        defaultScale = new Vector3(transform.localScale.x - addDistance,transform.localScale.y - addDistance,transform.localScale.z - addDistance);
	}
	
	// Update is called once per frame
	void Update () {

        Move();

        ChangeScale();

	}

    void Move()
    {
        transform.position = new Vector3(parentObj.position.x,
                                         parentObj.position.y - GetDistanceFromFloor() + addDistance,
                                         parentObj.position.z);
    }


    /// <summary>
    /// 母体と地面との距離を得る
    /// </summary>
    /// <returns>母体と地面の距離</returns>
    float GetDistanceFromFloor()
    {
        RaycastHit hit;
        var Distance = minRayDistance;
        if (Physics.Raycast(parentObj.position, Vector3.down, out hit, maxRayDistance, (1 << LayerMask.NameToLayer("Floor"))))
        {
            if (hit.collider.tag == realizationObjectTag) return Distance;
            Distance = hit.distance;
        }
        else
        {
            Distance = maxRayDistance;
        }

        return Distance;
    }

    /// <summary>
    /// 距離に応じて
    /// </summary>
    void ChangeScale()
    {
        transform.localScale = defaultScale / GetDistanceFromFloor();

    }

}
