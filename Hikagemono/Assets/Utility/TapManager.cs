/// ----------------------------------------------------
/// Tap管理
/// 
/// オブジェクトとのタップあたり判定
/// レイキャスト使用！！
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class TapManager : MonoBehaviour {

    /// <summary>
    /// タップした座標
    /// </summary>
    static public Vector3 ScreenPosition { get { return Input.mousePosition; } }

    /// <summary>
    /// ワールド座標タップした座標
    /// </summary>
    static public Vector3 WorldPosition { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }

    /// <summary>
    /// レイで飛ばした時のヒット座標
    /// </summary>
    static public Vector3 RayHitPosition { get; private set; }

    /// <summary>
    /// タップを押した瞬間の判定
    /// </summary>
    /// <param name="hitGameObject"></param>
    /// <returns></returns>
    static public bool TapDown(GameObject hitGameObject)
    {
        if (!Input.GetMouseButtonDown(0)) return false;
        if (!RayCastHit(hitGameObject)) return false;

        return true;
    }

    /// <summary>
    /// タップが離れたときの判定
    /// </summary>
    /// <param name="hitGameObject"></param>
    /// <returns></returns>
    static public bool TapUp(GameObject hitGameObject)
    {
        if (!Input.GetMouseButtonUp(0)) return false;
        if (!RayCastHit(hitGameObject)) return false;

        return true;
    }

    /// <summary>
    /// タップを押した瞬間の判定
    /// </summary>
    /// <returns></returns>
    static public bool TapDown()
    {
        if (!Input.GetMouseButtonDown(0)) return false;
        if (!RayCastHit()) return false;

        return true;
    }

    /// <summary>
    /// タップが離れたときの判定
    /// </summary>
    /// <returns></returns>
    static public bool TapUp()
    {
        if (!Input.GetMouseButtonUp(0)) return false;
        if (!RayCastHit()) return false;

        return true;
    }

    /// <summary>
    /// レイキャスト処理
    /// </summary>
    /// <param name="hitGameObject"></param>
    /// <returns></returns>
    static bool RayCastHit()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            RayHitPosition = hit.point;
            return true;
        }

        return false;
    }

    /// <summary>
    /// レイキャスト処理
    /// </summary>
    /// <param name="hitGameObject"></param>
    /// <returns></returns>
    static bool RayCastHit(GameObject hitGameObject)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            if (hitGameObject == hit.collider.gameObject)
            {
                RayHitPosition = hit.point;
                return true;
            }
        }

        return false;
    }
}
