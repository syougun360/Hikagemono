/// ----------------------------------------------------
/// アニメーションのインターフェイス
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ISpriteAnimator : MonoBehaviour 
{

    [System.Serializable]
    public struct AnimationData
    {
        public AnimationData(float rateTime,
            Script_SpriteStudio_PartsRoot.PlayStyle playStyle = Script_SpriteStudio_PartsRoot.PlayStyle.NORMAL, 
            bool canLoop = true)
            : this()
        {
            this.playStyle = playStyle;
            this.rateTime = rateTime;
            this.canLoop = canLoop;
        }

        public Script_SpriteStudio_PartsRoot.PlayStyle playStyle;
        public float rateTime;
        public bool canLoop;
    }

    [SerializeField]
    Script_SpriteStudio_PartsRoot spritePartsRoot = null;

    [SerializeField]
    List<AnimationData> animationData = new List<AnimationData>();

    /// <summary>
    /// アニメーションを再生スタート
    /// </summary>
    /// <param name="motion"></param>
    protected void Play(int motion)
    {
        var data = animationData[motion];
        var loop = data.canLoop ? -1 : 1;

        // motion番号、再生回数(Loopの場合 : -1)、開始再生フレーム、再生する速度時間、再生スタイル
        spritePartsRoot.AnimationPlay(motion, loop, 0, data.rateTime, data.playStyle);

    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        spritePartsRoot.AnimationStop();
    }

    /// <summary>
    /// 現在アニメーションが再生されてるかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsAnimationCheckPlay()
    {
        if (spritePartsRoot.AnimationCheckPlay())
        {
            return true;
        }
        return false;
    }
}
