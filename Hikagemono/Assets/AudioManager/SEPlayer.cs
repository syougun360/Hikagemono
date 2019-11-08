/// ------------------------------------------------------
/// SEプレイヤー
///
/// SEの再生機です。
///
/// code by Yamada Masamitsu
/// -----------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SEPlayer : MonoBehaviour {

    public struct Data
    {
        public Data(string resName):this()
        {
            this.resName = resName;
            clip = Resources.Load("SE/" + resName) as AudioClip;
        }
        public string resName;
        public AudioClip clip;
    }

    List<AudioSource> sources = new List<AudioSource>();
    Dictionary<string, Data> audioMap = new Dictionary<string, Data>();
    const float maxVolume = 0.8f;

    private static SEPlayer instance = null;
    public static SEPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject instanceObject = new GameObject("SEPlayer");
                instance = instanceObject.AddComponent<SEPlayer>();
            }
            return instance;
        }
    }

    /// <summary>
    /// 再生
    /// </summary>
    /// <param name="resName">Resource名</param>
    public void Play(string resName, float pitch = 1.0f, bool loop = false)
    {
        if (!audioMap.ContainsKey(resName))
        {
            audioMap.Add(resName, new Data(resName));
        }

        sources.Add(gameObject.AddComponent<AudioSource>());
        var index = sources.Count - 1;
        sources[index].clip = audioMap[resName].clip;
        sources[index].pitch = pitch;
        sources[index].loop = loop;
        sources[index].volume = maxVolume;
        sources[index].Play();
    }

    /// <summary>
    /// 停止
    /// </summary>
    /// <param name="resName">Resource名</param>
    public void Stop(string resName)
    {
        foreach (var source in sources)
        {
            if (source.clip.name == resName)
            {
                source.Stop();
                break;
            }
        }
    }

    /// <summary>
    /// すべて削除する
    /// </summary>
    /// <param name="resName">Resource名</param>
    public void AllDestroy()
    {
        foreach (var source in sources)
        {
            Destroy(source);
            sources.Remove(source);
        }
    }

    void Update()
    {
        foreach (var source in sources)
        {
            if (!source.isPlaying)
            {
                Destroy(source);
                sources.Remove(source);
                break;
            }
        }

    }

    /// <summary>
    /// 再生中かどうか
    /// </summary>
    /// <param name="resName">Resource名</param>
    /// <returns></returns>
    public bool IsPlaying(string resName)
    {
        foreach (var source in sources)
        {
            if (source.isPlaying && source.clip.name == resName)
            {
                return true;
            }
        }

        return false;
    }

}
