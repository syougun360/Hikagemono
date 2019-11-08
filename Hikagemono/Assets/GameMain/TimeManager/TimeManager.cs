using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

    private static string className = "TimeManager";

    /// <summary>
    /// 時間を操る上限下限
    /// </summary>
    public const float MinTimeRange = 0.0f;
    public const float MaxTimeRange = 100.0f;

    /// <summary>
    /// 現在のゲーム内の時間を扱う
    /// </summary>
    [SerializeField, Range(MinTimeRange, MaxTimeRange)]
    static private float nowTime = 0.0f;

    static public float NowTime { get { return nowTime; } }
    static public float NowChangingTime { get; private set; }
    static public float ChangedTime { get; private set; }

    [SerializeField]
    private iTween.EaseType changeTimeEaseType = iTween.EaseType.linear;

	// Use this for initialization
	void Start () {
        nowTime = 0;
        ChangedTime = nowTime;
	}
	
	// Update is called once per frame
	void Update () {
        CheckTimeOverflow();
	}

    /// <summary>
    /// 時間の変数の範囲外ではないかをチェックし直す
    /// </summary>
    static void CheckTimeOverflow()
    {
        if (nowTime < MinTimeRange) nowTime = MinTimeRange;

        if (nowTime > MaxTimeRange) nowTime = MaxTimeRange;
    }

    /// <summary>
    /// 現在時間が第一引数と第二引数の間かどうかを判断する
    /// </summary>
    /// <param name="beginTime">この時間より遅く</param>
    /// <param name="endTime">この時間より早いか</param>
    /// <returns>指定された時間内である...true 時間外である...false</returns>
    static public bool IsInTime(float beginTime, float endTime)
    {
        if (beginTime > endTime) return false;

        if (beginTime < nowTime && nowTime < endTime)
        {
            return true;
        }

        return false;
    }
    

    /// <summary>
    /// 操作中の現在時間が第一引数と第二引数の間かどうかを判断する
    /// </summary>
    /// <param name="beginTime">この時間より遅く</param>
    /// <param name="endTime">この時間より早いか</param>
    /// <returns>指定された時間内である...true 時間外である...false</returns>
    static public bool IsInChangingTime(float beginTime, float endTime)
    {
        if (beginTime > endTime) return false;

        if (beginTime < NowChangingTime && NowChangingTime < endTime)
        {
            return true;
        }

        return false;
    }

    static public void ChangeTimeTo(float changedTime, float needTime = 1.0f)
    {
        ChangedTime = changedTime;
        GameObject TimeMngrObj = GameObject.Find(className);
        var TimeMngr = TimeMngrObj.GetComponent<TimeManager>();
        iTween.ValueTo(TimeMngrObj, iTween.Hash("from",TimeManager.NowTime , "to", changedTime,"easetype",TimeMngr.changeTimeEaseType, "time", needTime, "onupdate", "UpdateHandler"));
    }


    static public void ChangingTimeUpdate(float nowChangingTime, float needTime = 1.0f)
    {
        GameObject TimeMngrObj = GameObject.Find(className);
        var TimeMngr = TimeMngrObj.GetComponent<TimeManager>();
        NowChangingTime = nowChangingTime;
    }
    
    private void UpdateHandler(float value)
    {
        nowTime = value;
    }
 
}
