using UnityEngine;
using System.Collections;

public class MoveTutorial : BaseTutorial {

	// Use this for initialization
	void Start () {
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
        Move();
    }

    /// <summary>
    /// 画面上の初期値（ローカル）
    /// </summary>
    void SetStartPosition()
    {
        transform.localPosition = new Vector3(parentSize.x / 6 - parentSize.x / 2, -parentSize.y / 4, 0.0f);
    }

    /// <summary>
    /// 移動の実装
    /// </summary>
    void Move()
    {
        iTween.MoveTo(gameObject, iTween.Hash("islocal", true, "x", parentSize.x / 3 - parentSize.x / 2, "easetype",moveEaseType,"time", loopTime, "looptype", iTween.LoopType.loop));
    }


}
