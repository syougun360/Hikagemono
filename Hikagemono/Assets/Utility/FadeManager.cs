/// ----------------------------------------------------
/// フェード管理
/// 
/// フェードイン・アウトの処理がある。
/// もし、フェードイン・アウトをする場合は、
/// GetComponentで取得して、
/// StartFadeIn,StartFadeOutで実行してください。
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------


using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour 
{
    Texture2D texture = null;
    float fadeAlpha = 0;
    float fadeTime = 0;
    public bool IsFading { get; private set; }

    void Awake()
    {
        texture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
    }

    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

    }

    /// <summary>
    /// フェードイン開始
    /// </summary>
    /// <param name="time"></param>
    public void StartFadeIn(float time)
    {
        StratFade(time, 1, 0);
    }

    /// <summary>
    /// フェードアウト開始
    /// </summary>
    /// <param name="time"></param>
    public void StartFadeOut(float time)
    {
        StratFade(time, 0, 1);
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <param name="time"フェードする時間></param>
    /// <param name="begin">開始する値</param>
    /// <param name="end">終了する値</param>
    void StratFade(float time,float begin,float end)
    {
        IsFading = true;
        fadeTime = time;
        iTween.ValueTo(gameObject, iTween.Hash("from", begin, "to", end, "time", fadeTime, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        fadeAlpha = value;

        fadeTime -= Time.deltaTime;
        if (fadeTime <= 0)
        {
            IsFading = false;
        }
    }

}
