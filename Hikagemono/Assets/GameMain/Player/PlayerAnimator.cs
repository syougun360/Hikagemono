/// ----------------------------------------------------
/// アニメーション
/// 
/// モーションの種類をここで切り替える
/// 
/// アニメーションの再生は、Start後に呼ばないと、
/// Script_SpriteStudio_PartsRootが初期化が完了しません。
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimator : ISpriteAnimator {

    public enum AnimationType
    {
        Idling = 0,
        Damage,
        Jump,
        ShadowInstanceCreate,
        ChangeTime,
        Move,
    };

	// Use this for initialization
	void Start ()
    {
        StartCoroutine("WaitStartPlay");
	}

    IEnumerator WaitStartPlay()
    {
        yield return new WaitForEndOfFrame();

        Play(AnimationType.Idling);
    }

    public void Play(AnimationType motion)
    {
        Play((int)motion);
    }


	// Update is called once per frame
    void Update()
    {
    }



}
