/// ----------------------------------------------------
/// 具現化したときのエフェクトの大きさを大きくする
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class InstanceCreateEffectScaler : MonoBehaviour {

    [SerializeField]
    float maxScale = 1.0f;

    [SerializeField]
    float scaleTime = 2.0f;

    SpriteRenderer sprite = null;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(maxScale, maxScale, maxScale),
            "time", scaleTime, "easetype", iTween.EaseType.easeOutCirc));
        iTween.ValueTo(gameObject, iTween.Hash("from",1 , "to",0, "time", scaleTime / 4, "onupdate", "AlphaUpdate"));
	}

    void AlphaUpdate(float value)
    {
        sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b,value);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
