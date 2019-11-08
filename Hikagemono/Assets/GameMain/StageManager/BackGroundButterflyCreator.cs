/// ----------------------------------------------------
/// 背景の蝶生成
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class BackGroundButterflyCreator : MonoBehaviour {

    [SerializeField]
    int maxNum = 10;

    [SerializeField]
    Vector3 minRange = new Vector3(2, -1, -1);

    [SerializeField]
    Vector3 maxRange = new Vector3(5, 1, 1);

    [SerializeField]
    GameObject butterfly = null;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < maxNum; i++)
        {
            var x = Random.Range((int)minRange.x, (int)maxRange.x);
            var y = Random.Range((int)minRange.y, (int)maxRange.y);
            var z = Random.Range((int)minRange.z, (int)maxRange.z);
            var pos = new Vector3(x, y, z);
            var clone = (GameObject)Instantiate(butterfly, transform.position + pos, butterfly.transform.rotation);
            clone.name = butterfly.name;
            clone.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
