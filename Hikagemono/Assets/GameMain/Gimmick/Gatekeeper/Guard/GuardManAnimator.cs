using UnityEngine;
using System.Collections;

public class GuardManAnimator : ISpriteAnimator
{

    public enum AnimationType
    {
        Idling = 0,
        Move,
        Attack,
    };

    // Use this for initialization
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
