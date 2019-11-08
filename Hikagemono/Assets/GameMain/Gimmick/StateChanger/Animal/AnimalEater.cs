/// ----------------------------------------------------
/// アニマル食べる処理
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class AnimalEater : IAnimalState
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!stateManager.IsEating) return;

        Init();
        Finish();
        Control();
    }

    void Init()
    {
        if (state != State.Init) return;

        state = State.Control;
    }

    /// <summary>
    /// 食べる時間以外なら状態を終わらす。
    /// 戻す処理にする。
    /// </summary>
    void Control()
    {
        if (state != State.Control) return;

        if (stateManager.IsChangeReturnState())
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
