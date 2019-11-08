using UnityEngine;
using System.Collections;


/// <summary>
/// http://kidooom.hatenadiary.jp/entry/20140330/1396194258
/// より。
/// </summary>

public class CameraShaker : MonoBehaviour
{
    /// <summary>
    /// 振動の減少量
    /// </summary>
    [SerializeField]
    private float shakeDecay = 0.001f;

    /// <summary>
    /// 振動開始時の振動の大きさ
    /// </summary>
    [SerializeField]
    private float coefShakeIntensity = 0.01f;

    /// <summary>
    /// カメラの初期位置
    /// </summary>
    private Vector3 originPosition;

    /// <summary>
    /// カメラの初期傾き
    /// </summary>
    private Quaternion originRotation;

    /// <summary>
    /// 現在の振動の大きさ
    /// </summary>
    private float shakeIntensity;

    void Start()
    {
        originRotation = transform.rotation;
    }

    void Update()
    {
        if (shakeIntensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shakeIntensity;
            transform.rotation = new Quaternion(
                                             originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * 2f,
                                             originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * 2f,
                                             originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * 2f,
                                             originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * 2f);
            shakeIntensity -= shakeDecay;
        }
        else
        {
            transform.rotation = originRotation;
        }
        
    }


    /// <summary>
    /// 振動させる
    /// </summary>
    public void Shake()
    {
        originPosition = transform.position;
        shakeIntensity = coefShakeIntensity;
    }

}