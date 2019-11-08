/// ----------------------------------------------------
/// 背景の蝶の移動
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackGroundButterflyMover : MonoBehaviour {

    [SerializeField]
    string leftCheckPointName = string.Empty;

    [SerializeField]
    string rightCheckPointName = string.Empty;

    [SerializeField]
    float minTime = 20.0f;

    [SerializeField]
    float maxTime = 60.0f;

    [SerializeField]
    List<iTween.EaseType> easeTypes = new List<iTween.EaseType>();

    GameObject rightCheckPoint = null;
    GameObject leftCheckPoint = null;

    float scale = 0;
    enum Direction
    {
        Left,
        Right
    };
    Direction dir = Direction.Left;

    void Start()
    {
        scale = transform.lossyScale.x;

        rightCheckPoint = GameObject.Find(rightCheckPointName);
        leftCheckPoint = GameObject.Find(leftCheckPointName);

        var randomDir = Random.Range(0, 2);
        if (randomDir == 0)
            RightMoveTo();
        if (randomDir == 1)
            LeftMoveTo();
    }

    
    void Update()
    {
        LeftCheckPoint();
        RightCheckPoint();
    }

    void RightMoveTo()
    {
        var randomEaseType = Random.Range(0,easeTypes.Count);
        iTween.MoveTo(gameObject, iTween.Hash("x", rightCheckPoint.transform.position.x,
            "time", Random.Range(minTime, maxTime), "easetype", easeTypes[randomEaseType]));
        iTween.ScaleTo(gameObject, new Vector3(-scale, scale, 1), 1.0f);

        dir = Direction.Right;
    }

    void LeftMoveTo()
    {
        var randomEaseType = Random.Range(0, easeTypes.Count);

        iTween.MoveTo(gameObject, iTween.Hash("x", leftCheckPoint.transform.position.x,
            "time", Random.Range(minTime, maxTime), "easetype", easeTypes[randomEaseType]));
        iTween.ScaleTo(gameObject, new Vector3(scale, scale, 1), 1.0f);

        dir = Direction.Left;
    }

    void LeftCheckPoint()
    {
        if (dir != Direction.Left) return;

        if (leftCheckPoint.transform.position.x >= transform.position.x)
        {
            RightMoveTo();
        }

    }


    void RightCheckPoint()
    {
        if (dir != Direction.Right) return;
        
        if (rightCheckPoint.transform.position.x <= transform.position.x)
        {
            LeftMoveTo();
        }
    }
}
