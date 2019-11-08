/// ----------------------------------------------------
/// 具現化したときのエフェクトの管理
/// 
/// 生成個数や色を指定できる。
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstanceCreateEffectManager : MonoBehaviour {

    [SerializeField]
    int createFrame = 10;

    [SerializeField]
    GameObject effect = null;

    [SerializeField]
    List<Color> createColors = new List<Color>();

    int frame = 0;
    int index = 0;
	// Use this for initialization
	void Start () {
        frame = createFrame;
        transform.LookAt(
            new Vector3(transform.position.x,
            Camera.main.transform.position.y,
            Camera.main.transform.position.z));
	}
	
	// Update is called once per frame
	void Update () {
        if (index >= createColors.Count) return;

        frame++;
        if (frame >= createFrame)
        {
            frame = 0;
            var clone = (GameObject)Instantiate(effect, transform.position, transform.rotation);
            clone.transform.parent = transform;
            clone.GetComponent<SpriteRenderer>().color = createColors[index];
            index++;
        }
	}
}
