using UnityEngine;
using System.Collections;

public class TownStageManager : IStageManager
{

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void Init()
    {
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    protected override void Finish()
    {
        SceneManager.Instance.ChangeSceneLoad(SceneNameManager.Scene.Ending,false);
    }
}
