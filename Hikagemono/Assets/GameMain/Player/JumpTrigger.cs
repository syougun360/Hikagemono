using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {

    [SerializeField]
    PlayerJumper jump = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        jump.JumpingNonEnable();
    }
}
