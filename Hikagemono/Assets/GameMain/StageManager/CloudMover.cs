/// ----------------------------------------------------
/// 雲を動かす
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudMover : MonoBehaviour {

    [SerializeField]
    float speed = 0;

    [SerializeField]
    Transform vanishPosition = null;

    [SerializeField]
    Transform appearancePosition = null;

    GameObject[] childrens;

	// Use this for initialization
	void Start () {
        childrens = GameObject.FindGameObjectsWithTag("Cloud") ;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var children in childrens)
        {
            children.transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (vanishPosition.position.x >= children.transform.position.x)
            {
                children.transform.position = appearancePosition.position;
            }
        }
	}
}
