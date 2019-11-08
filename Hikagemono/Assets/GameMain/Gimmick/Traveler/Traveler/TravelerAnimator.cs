using UnityEngine;
using System.Collections;

public class TravelerAnimator : ISpriteAnimator{
 
    public enum AnimationType
    {
        /// <summary>
        /// 道で迷っている
        /// </summary>
        Idling = 0,
        /// <summary>
        /// 地図を見ている
        /// </summary>
        WatchingMap,
    };

    void Start()
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

    void Update()
    {

    }
}
