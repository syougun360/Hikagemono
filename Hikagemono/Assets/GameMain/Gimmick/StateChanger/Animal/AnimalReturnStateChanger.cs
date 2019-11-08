/// ----------------------------------------------------
/// アニマル元の状態に戻す処理
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class AnimalReturnStateChanger : IAnimalState {

    [SerializeField]
    float moveTime = 1;

    Vector3 startPosition = Vector3.zero;
    float currentTime = 0;
    
    // Use this for initialization
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stateManager.IsReturning) return;

        Init();
        Control();
        Finish();
    }

    void Init()
    {
        if (state != State.Init) return;

        currentTime = moveTime;

        state = State.Control;
    }

    void Control()
    {
        if (state != State.Control) return;

        iTween.MoveTo(gameObject, startPosition, moveTime);

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            state = State.Finish;
        }
    }

    void Finish()
    {
        if (state != State.Finish) return;

        state = State.Init;
    }
}
