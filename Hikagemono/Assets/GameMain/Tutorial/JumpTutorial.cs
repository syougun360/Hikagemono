using UnityEngine;
using System.Collections;

public class JumpTutorial : BaseTutorial {

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

        transform.localPosition = new Vector3(parentSize.x / 4 , -parentSize.y / 4, 0.0f);
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
                                    "time", loopTime, "easetype", moveEaseType,"looptype",iTween.LoopType.loop));

    }


}

