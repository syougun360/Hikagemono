/// ------------------------------
/// ----------------------
/// butterflyのAI
/// 
/// 
/// code by 髙木　芳朗
/// ----------------------------------------------------
using UnityEngine;
using System.Collections;

public class ButteflyAI : MonoBehaviour
{
    /// <summary>
    /// 状態管理クラス
    /// </summary>
    [SerializeField]
    GuardManStateManager stateManager = null;

    public float speed = 0.1f;
    public float waiteTime = 3.0f;
    public Transform target;
    public int state = 0;
    private bool once = false;
    // Use this for initialization

    IEnumerator move()
    {
        state = 1;

        yield return new WaitForSeconds(waiteTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 0.07f);
        transform.position += transform.forward * speed;
        stateManager.ChangeMove();
        if (target.transform.position.x - gameObject.transform.position.x >= 0)
        {
            state = 0;
            once = true;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (gameObject.GetComponent<MeshRenderer>().enabled == true && once == false)
        {
            StartCoroutine("move"); 
        }
	
	}
}
