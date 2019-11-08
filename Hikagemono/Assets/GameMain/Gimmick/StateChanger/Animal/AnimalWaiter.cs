/// ----------------------------------------------------
/// アニマル道にいる状態処理
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class AnimalWaiter : IAnimalState {


    // Update is called once per frame
    void Update()
    {
        if (!stateManager.IsWaiting) return;

        Init();
        Control();
        Finish();
    }

    void Init()
    {
        if (state != State.Init) return;

        state = State.Control;
    }

    void Control()
    {
        if (state != State.Control) return;
        
        state = State.Finish;
    }

    void Finish()
    {
        if (state != State.Finish) return;

        state = State.Init;
    }
}
