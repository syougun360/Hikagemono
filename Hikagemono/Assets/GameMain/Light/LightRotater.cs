using UnityEngine;
using System.Collections;

public class LightRotater : MonoBehaviour {

    /// <summary>
    /// メータの針の角度を求めて光の角度を変える
    /// </summary>
    [SerializeField]
    MaterHandMover mater = null;

    /// <summary>
    /// 初期の角度を記憶する
    /// </summary>
    Vector3 defaultEulerAngles = Vector3.zero;


    /// <summary>
    /// Y軸の角度の最小値
    /// </summary>
    [SerializeField]
    float MinShadowAngle = -90;

    /// <summary>
    /// Y軸の角度の最大値
    /// </summary>
    [SerializeField]
    float MaxShadowAngle = 90;

	// Use this for initialization
	void Start () {
        defaultEulerAngles = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        SetEulerAngles();
	}

    /// <summary>
    /// 角度を変更
    /// </summary>
    void SetEulerAngles()
    {
        transform.eulerAngles = new Vector3(defaultEulerAngles.x, CalcAngle(), defaultEulerAngles.z);
    }

    float CalcAngle()
    {
        var result = MyMath.CalcMap(mater.RotateZ, MaterHandMover.MaxAngle + 360, MaterHandMover.MinAngle + 360, MaxShadowAngle, MinShadowAngle);
        return result;
    }

}
