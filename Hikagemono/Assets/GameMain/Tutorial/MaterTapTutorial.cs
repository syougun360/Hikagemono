using UnityEngine;
using System.Collections;

public class MaterTapTutorial : BaseTutorial
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        SetParentSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyInitialized && !GameStateManager.IsControlCameraStart && player.transform.position.x > showTutorialStartX)
        {
            Initalize();
            alreadyInitialized = true;
        }


        CheckShowTutorial();

        if (isDestroy)
        {
            DestroyActions();
        }
    }

    void CheckShowTutorial()
    {
        if(!isDestroy && GameStateManager.IsControlTimeMater)
        {
            isDestroy = true;
            Destroy(gameObject, dyingTime);
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Initalize()
    {
        base.Initalize();
        SetStartPosition();
        ChangeScale();

    }

    /// <summary>
    /// 画面上の初期値（ローカル）
    /// </summary>
    void SetStartPosition()
    {
        transform.localPosition = new Vector3(0.0f, -parentSize.y * 2 / 5, -1.0f);
    }

    void Move()
    {
        var MoveDistance = 20.0f;
        iTween.MoveTo(gameObject, iTween.Hash("islocal", true, "y", transform.localPosition.y - MoveDistance, "easetype", moveEaseType, "time", loopTime, "looptype", iTween.LoopType.loop));

    }

    /// <summary>
    /// サイズの変化
    /// </summary>
    void ChangeScale()
    {
        var changeValue = 0.8f;
        iTween.ScaleTo(gameObject, iTween.Hash("x", transform.localScale.x * changeValue,
                                    "y", transform.localScale.y * changeValue,
                                    "z", transform.localScale.z * changeValue,
                                    "time", loopTime, "easetype", moveEaseType, "looptype", iTween.LoopType.loop));

    }


}

