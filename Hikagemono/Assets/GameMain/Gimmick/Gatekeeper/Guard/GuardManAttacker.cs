using UnityEngine;
using System.Collections;

public class GuardManAttacker : MonoBehaviour {

    [SerializeField]
    GuardManStateManager stateManager = null;

    /// <summary>
    /// playerをインスペクタで指定
    /// </summary>
    [SerializeField]
    PlayerKnockBacker playerKnockBacker = null;

    [SerializeField]
    CameraShaker cameraShaker = null;
    

    /// <summary>
    /// 吹き飛ばす方向
    /// </summary>
    [SerializeField]
    Vector3 attackVelocity = Vector3.zero;

    void Start()
    {

    }

    void OnCollisionEnter(Collision collObj)
    {
        if (!stateManager.IsIdling) return;

        if (collObj.gameObject != playerKnockBacker.gameObject) return;

        playerKnockBacker.KnockBack(attackVelocity);
        cameraShaker.Shake();
        stateManager.ChangeAttack();
    }
}
