using UnityEngine;
using System.Collections;

public class PlayerKnockBacker : MonoBehaviour {

    [SerializeField]
    PlayerStateManager stateManager = null;

    [SerializeField]
    CameraMover cameraMover = null;

    Rigidbody playerRig = null;

    void Start()
    {
        playerRig = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ノックバックさせる
    /// </summary>
    /// <param name="velocity">吹き飛ばす方向と威力</param>
    public void KnockBack(Vector3 velocity)
    {
        playerRig.AddForce(velocity, ForceMode.VelocityChange);
        stateManager.ChangeDamage();
        cameraMover.Stop();
    }
}
