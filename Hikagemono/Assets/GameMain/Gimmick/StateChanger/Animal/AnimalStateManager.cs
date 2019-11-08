/// ----------------------------------------------------
/// アニマル状態管理
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class AnimalStateManager : MonoBehaviour {


    public enum State
    {
        None,
        Waiting,
        Moving,
        Eating,
        Returning,
    };
    
    [System.Serializable]
    public struct StateData
    {
        public StateData(float beginTime,float endTime,State state):this()
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
            this.state = state;
        }

        public float beginTime;
        public float endTime;
        public State state { get; set; }
    };

    [SerializeField]
    StateData morningWaitingState = new StateData(0, 0, State.Waiting);

    [SerializeField]
    StateData movingState = new StateData(0, 0, State.Moving);

    [SerializeField]
    StateData eveningWaitingState = new StateData(0, 0, State.Waiting);

    [SerializeField]
    AnimalAnimator animator = null;

    State state = State.None;

    public bool IsMoving { get { return state == State.Moving; } }
    public bool IsWaiting { get { return state == State.Waiting; } }
    public bool IsEating { get { return state == State.Eating; } }
    public bool IsReturning { get { return state == State.Returning; } }

	// Use this for initialization
	void Start () {
        state = morningWaitingState.state;
	}

    /// <summary>
    /// 食べる状態にする。
    /// </summary>
    public void ChangeEating()
    {
        state = State.Eating;
    }

    /// <summary>
    /// 戻る状態にする
    /// もし、道にいる状態の時間なら戻す状態にする。
    /// </summary>
    /// <returns></returns>
    public bool IsChangeReturnState()
    {
        if (ChangeState(morningWaitingState) || ChangeState(eveningWaitingState))
        {
            state = State.Returning;
            animator.Play(AnimalAnimator.AnimationType.Normal);
            return true;
        }
        return false;
    }

	// Update is called once per frame
	void Update () 
    {
        if (ChangeState(movingState))
        {
            animator.Play(AnimalAnimator.AnimationType.Eating);
        }
	}

    /// <summary>
    /// 状態を変更する
    /// </summary>
    /// <param name="stateData"></param>
    /// <returns></returns>
    bool ChangeState(StateData stateData)
    {
        if (stateData.state == state) return false;

        if (TimeManager.IsInTime(stateData.beginTime, stateData.endTime))
        {
            state = stateData.state;
            return true;
        }
        return false;
    }
}
