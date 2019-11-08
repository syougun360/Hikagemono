/// ----------------------------------------------------
/// 具現化
/// 
/// 時間内に影をタップすると具現化される
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class InTimeInstanceCreator : MonoBehaviour {

    [SerializeField]
    float beginTime = 0;
   
    [SerializeField]
    float endTime = 0;

    [SerializeField]
    PlayerStateManager playerState = null;

    [SerializeField]
    GameObject effect = null;

    [SerializeField]
    Vector3 appearPos = Vector3.zero;

    [SerializeField]
    float appearTime = 1.0f;

    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;

    public float BeginTime { get { return beginTime; } }
    public float EndTime { get { return endTime; } }
    public bool IsDraw { get; private set; }

	// Use this for initialization
	void Start () {
	}

    public bool IsInstance()
    {
        if (IsDraw) return false;
        if (!TimeManager.IsInTime(beginTime, endTime)) return false;
        if (!TapManager.TapDown(gameObject)) return false;

        return true;
    }
	
	void Update () 
    {
        if (!IsInstance()) return;

        IsDraw = true;
        var clone = (GameObject)Instantiate(effect, gameObject.transform.position, Quaternion.identity);
        playerState.ChangeShadow();

        Destroy(GetComponent<Collider>());

        var child = transform.GetChild(0);
        child.GetComponent<Collider>().isTrigger = false;
        child.GetComponent<MeshRenderer>().enabled = true;

        iTween.MoveTo(child.gameObject, 
            iTween.Hash("position", transform.position + appearPos,
            "time", appearTime, "easeType", easeType));

        Destroy(this);
	}
}
