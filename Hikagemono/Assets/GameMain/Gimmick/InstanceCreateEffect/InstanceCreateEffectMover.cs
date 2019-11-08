/// ----------------------------------------------------
/// 具現化したときのエフェクトの移動処理
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class InstanceCreateEffectMover : MonoBehaviour {

    [SerializeField]
    float moveTime = 1.0f;

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("position", Camera.main.transform.position,
            "time", moveTime, "easetype", iTween.EaseType.easeOutCirc));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
