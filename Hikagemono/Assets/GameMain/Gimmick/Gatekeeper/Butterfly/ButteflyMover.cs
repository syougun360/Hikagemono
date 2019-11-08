/// ------------------------------
/// ----------------------
/// butterflyのAI（動き）
/// 
/// 出現してからゆっくり番兵に近づき
/// 少し滞在してからスタート地点に移動します
/// 
/// 
/// code by 髙木　芳朗
/// ----------------------------------------------------
using UnityEngine;
using System.Collections;

public class ButteflyMover : MonoBehaviour
{
    [SerializeField]
    GuardManStateManager guardManState = null;

    [SerializeField]
    Transform target = null;

    [SerializeField]
    Transform checkPoint = null;

    [SerializeField]
    private float playTime = 60.0f;
    [SerializeField]
    private float interval = 5.0f;

    MeshRenderer meshRender = null;
    
    Hashtable  path01 = new Hashtable();
    Hashtable  path02 = new Hashtable();

    enum State
    {
        None,
        CheckPointMove,
        TargetStartMove,
        TargetMove,
    };

    State state = State.None;

	// Use this for initialization
	void Start ()
    {
        meshRender = GetComponent<MeshRenderer>();
        path01.Add("x", checkPoint.transform.position.x - 2);
        path01.Add("y", checkPoint.transform.position.y + 1);
        path01.Add("z", checkPoint.transform.position.z);
        path01.Add("time", interval);

        path02.Add("x", target.transform.position.x);
        path02.Add("y", target.transform.position.y);
        path02.Add("z", target.transform.position.z);
        path02.Add("time", playTime);
        
    }

    IEnumerator ChangeTargetStartMoveState()
    {
        yield return new WaitForSeconds(interval);

        GetComponent<BoxCollider>().enabled = false;
        state = State.TargetStartMove;
    }


    IEnumerator WaitCheckPointStartMoveState()
    {
        yield return new WaitForSeconds(interval/2);

        iTween.MoveTo(gameObject, path01);
    }

	// Update is called once per frame
	void Update () 
    {
        StartCheckPointMove();
        StartTargetPointMove();
	}

    void StartCheckPointMove()
    {
        if (state != State.None) return;
        if (!meshRender.enabled) return;

        StartCoroutine("WaitCheckPointStartMoveState");
        StartCoroutine("ChangeTargetStartMoveState");
    }

    void StartTargetPointMove()
    {
        if (state != State.TargetStartMove) return;

        iTween.MoveTo(gameObject, path02);
        iTween.ScaleTo(gameObject, iTween.Hash("x",2,"time",1.0f));

        guardManState.ChangeMove();
        state = State.TargetMove;
    }
}
