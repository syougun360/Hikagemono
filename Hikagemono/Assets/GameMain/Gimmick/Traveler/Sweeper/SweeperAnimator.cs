using UnityEngine;
using System.Collections;

public class SweeperAnimator : ISpriteAnimator{

    public enum AnimationType
    {
        Idling = 0,
        Jump,
        /// <summary>
        /// 体を左に乗り出す
        /// </summary>
        LeanRight
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
