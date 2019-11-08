using UnityEngine;
using System.Collections;

public class AnimalAnimator : ISpriteAnimator {

    public enum AnimationType
    {
        Eating = 0,
        Normal ,
    };

    // Use this for initialization
    void Start()
    {
        StartCoroutine("WaitStartPlay");
    }

    IEnumerator WaitStartPlay()
    {
        yield return new WaitForEndOfFrame();

        Play(AnimationType.Normal);
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
