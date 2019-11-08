/// ------------------------------------------------------
/// BGMプレイヤー
///
/// BGMの再生機です。
///
/// code by Yamada Masamitsu
/// -----------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct FadeTimeData
{
    public FadeTimeData(float inTime, float outTime)
        : this()
    {
        this.inTime = inTime;
        this.outTime = outTime;
    }

    public static FadeTimeData Zero { get { return new FadeTimeData(0, 0); } }

    public float inTime;
    public float outTime;
}


public class BGMPlayer : MonoBehaviour {

    public struct Data
    {
        public Data(string resName):this()
        {
            this.resName = resName;
            clip = Resources.Load("BGM/" + resName) as AudioClip;
        }

        public string resName;
        public AudioClip clip;
    }

    const float minVolume = 0;
    const float maxVolume = 0.2f;
    const float startFadeInVolume = 0.005f;

    AudioSource source = null;

    Dictionary<string, Data> audioMap = new Dictionary<string, Data>();
    FadeTimeData FadeTime;

    public bool IsPlaying { get { return source.isPlaying; } }

    private static BGMPlayer instance = null;
    public static BGMPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject instanceObject = new GameObject("BGMPlayer");
                instanceObject.AddComponent<AudioSource>();
                instance = instanceObject.AddComponent<BGMPlayer>();
            }
            return instance;
        }
    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
    }

    /// <summary>
    /// 再生
    /// </summary>
    /// <param name="resName">Resources/BGM/の中にあるオーディオ名</param>
    /// <param name="fadeInTime">フェードイン時間</param>
    public void Play(string resName, FadeTimeData fadeTime)
    {
        if (!audioMap.ContainsKey(resName))
        {
            audioMap.Add(resName, new Data(resName));
        }

        FadeTime = fadeTime;
        source.clip = audioMap[resName].clip;
        source.Play();
        source.volume = startFadeInVolume;

        StartFadeIn(FadeTime.inTime);
    }

    /// <summary>
    /// 停止
    /// </summary>
    /// <param name="fadeOutTime">フェードアウト時間</param>
    public void Stop()
    {
        StartFadeOut(FadeTime.outTime);
    }

    /// <summary>
    /// フェードアウトスタート
    /// </summary>
    /// <param name="time">時間</param>
    void StartFadeOut(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", maxVolume, "to", minVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    /// <summary>
    /// フェードインスタート
    /// </summary>
    /// <param name="time">時間</param>
    void StartFadeIn(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", startFadeInVolume, "to", maxVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        source.volume = value;

        if (source.volume <= 0)
        {
            source.Stop();
        }
    }

}
