using UnityEngine;
using System.Collections;

public class MaterMover : MonoBehaviour {

    /// <summary>
    /// 隠れている時のUIの高さ
    /// </summary>
    [SerializeField]
    float hidingHeight = -200.0f;

    /// <summary>
    /// 表示している時のUIの高さ
    /// </summary>
    [SerializeField]
    float showingHeight = 68.0f;

    /// <summary>
    /// UIの移動にかける時間
    /// </summary>
    [SerializeField]
    float animationTime = 1.0f;

    /// <summary>
    /// 出てくる時と隠れる時のイージングの種類
    /// </summary>
    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;

    /// <summary>
    /// メーターとして検知するメータ中心からの距離
    /// </summary>
    [SerializeField]
    float MaterRadius = 275.0f;


	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(transform.localPosition.x, hidingHeight, transform.localPosition.z);
	}

	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0) )
        {
            if (CanPopUp() && GameStateManager.IsControlPlayer) PopUp();
            if (CanPopDown() && GameStateManager.IsControlTimeMater) PopDown();
        }

    }

    /// <summary>
    /// メーター中心からのタップ地点の距離を返す
    /// </summary>
    /// <returns>タップ地点とメータ中心の距離</returns>
    float GetDistance()
    {
        var MaterPosition = Vector3.zero;

        if (GameStateManager.IsControlPlayer)
        {
            MaterPosition = new Vector3(Screen.width / 2, hidingHeight, 0.0f);
        }
        else
        {
            MaterPosition = new Vector3(Screen.width / 2, showingHeight, 0.0f);
        }

        var distanceX = (TapManager.ScreenPosition.x - MaterPosition.x);
        var distanceY = (TapManager.ScreenPosition.y - MaterPosition.y);

        return Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
    }

    /// <summary>
    /// 強調しても良い状態かどうか
    /// </summary>
    /// <returns>強調する...true 強調しない...false</returns>
    bool CanPopUp()
    {
        if (GetDistance() > MaterRadius) return false;
        
        return true;

    }


    /// <summary>
    /// UIを隠せる状態かどうか
    /// </summary>
    /// <returns>隠せる...true 隠すことが出来ない...false</returns>
    public bool CanPopDown()
    {
        if (GetDistance() < MaterRadius) return false;

        return true;
    }

    /// <summary>
    /// 出現する際のコード
    /// </summary>
    void PopUp()
    {
        StartCoroutine("WaitChangeControlMater");
        iTween.MoveTo(gameObject, iTween.Hash("y", showingHeight,"islocal", true,"easetype",easeType, "time", animationTime));
    }

    IEnumerator WaitChangeControlMater()
    {
        yield return new WaitForSeconds(animationTime);

        GameStateManager.SetControlMater();
    }

    /// <summary>
    /// 隠す際のコード
    /// </summary>
    public void PopDown()
    {
        GameStateManager.SetControlPlayer();
        iTween.MoveTo(gameObject, iTween.Hash("y", hidingHeight, "islocal", true, "easetype", easeType, "time", animationTime));
    }

}
