/// ----------------------------------------------------
/// butterflyのアニメーション
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButterflyAnimator : MonoBehaviour {

    [SerializeField]
    List<Texture> sprites = new List<Texture>();

    [SerializeField]
    int changeFrame = 10;

    int frame = 0;
    int index = 0;

    MeshRenderer render = null;
    MeshRenderer Render{get{return render ?? GetComponent<MeshRenderer>() ;}}

	// Use this for initialization
	void Start () {
        SetTexture();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Render.enabled) return;

        frame ++;
        if (frame >= changeFrame)
        {
            frame = 0;
            SetTexture();
        }
	}

    /// <summary>
    /// テクスチャの設定
    /// </summary>
    /// <param name="index"></param>
    void SetTexture()
    {
        index = index >= sprites.Count-1 ? 0 : ++index;

        Render.material.mainTexture = sprites[index];

    }
}
