using UnityEngine;
using System.Collections;

public class InstanceCreateEffectDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.LookAt(Camera.main.transform.position);
        Destroy(gameObject, GetComponent<ParticleSystem>().duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
