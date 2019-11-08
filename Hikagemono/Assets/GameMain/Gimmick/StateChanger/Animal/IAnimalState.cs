/// ----------------------------------------------------
/// アニマル状態インターフェイス
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class IAnimalState : MonoBehaviour {

    [SerializeField]
    protected AnimalStateManager stateManager = null;

    protected enum State
    {
        Init,
        Control,
        Finish,
    };

    protected State state = State.Init;

}
