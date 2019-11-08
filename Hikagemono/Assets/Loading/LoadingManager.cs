/// ----------------------------------------------------
/// ローディング管理部
///
/// ここでフェード処理を主にしています
/// そのほかの処理があればここに記述する。
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------


using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour {

    FadeManager fadeManager = null;
    FadeTimeData fadeTime;
    bool isFadeOut = false;
 
    /// <summary>
    /// ローディングの情報を設定する
    /// </summary>
    /// <param name="fadeManager">フェードする管理インスタンス</param>
    /// <param name="fadeTime">フェードタイム情報</param>
    public void SetLoadingData(FadeManager fadeManager,FadeTimeData fadeTime)
    {
        this.fadeManager = fadeManager;
        this.fadeTime = fadeTime;
    }

    /// <summary>
    /// フェードスタート
    /// </summary>
    public void StartFade()
    {
        fadeManager.StartFadeOut(fadeTime.outTime);
        isFadeOut = true;
    }

	// Update is called once per frame
	void Update () {
        if (!isFadeOut) return;
        if (fadeManager.IsFading) return;

        fadeManager.StartFadeIn(fadeTime.inTime);
        Destroy(gameObject);
	}
}
