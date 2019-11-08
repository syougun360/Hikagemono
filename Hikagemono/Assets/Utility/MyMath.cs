using UnityEngine;
using System.Collections;


/// <summary>
/// 今後使えそうな数学関係の関数を記述するクラスです
/// </summary>
public class MyMath : MonoBehaviour {

    /// <summary>
    /// valueの物をbaseMinからbaseMaxの範囲からresultMinからresultMaxの範囲にマップし返す
    /// </summary>
    /// <param name="value">変更を加える数値</param>
    /// <param name="baseMin">変更前の範囲の最小値</param>
    /// <param name="baseMax">変更前の範囲の最大値</param>
    /// <param name="resultMin">変更後の範囲の最小値</param>
    /// <param name="resultMax">変更後の範囲の最大値</param>
    /// <returns>マップされたvalueの値</returns>
    public static float CalcMap(float value, float baseMin, float baseMax, float resultMin, float resultMax)
    {
        return (value - baseMin) * (resultMax - resultMin) / (baseMax - baseMin) + resultMin;
    }

}
