using UnityEngine;
using System.Collections;

public class ButtonSceneChanger : MonoBehaviour {

    [SerializeField]
    SceneNameManager.Scene scene = SceneNameManager.Scene.Title;

    [SerializeField]
    float grayValue = 0.7f;

    [SerializeField]
    float maxFadeTime = 1.0f;

    [SerializeField]
    iTween.EaseType easeType = iTween.EaseType.linear;

    [SerializeField]
    bool isLoading = false;

    SpriteRenderer spriteRenderer = null;
    float fadeTime = 0;
    bool isChanged = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeTime = maxFadeTime;
    }

    void Update()
    {
        CallChangeScene();
        Fading();
    }

    void Fading()
    {
        if (!isChanged) return;

        fadeTime -= Time.deltaTime;
        if (fadeTime <= -maxFadeTime)
        {
            iTween.Stop(gameObject);
        }
    }


    /// <summary>
    /// シーンチェンジをSceneMangerから呼び出す。
    /// </summary>
    void CallChangeScene()
    {
        if (isChanged) return;

        if (TapManager.TapDown(gameObject))
        {
            SceneManager.Instance.ChangeSceneLoad(scene, isLoading);
            iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", grayValue, "time", maxFadeTime,
                "looptype", iTween.LoopType.pingPong, "easetype", easeType, "onupdate", "UpdateHandler"));
            isChanged = true;
        }
    }

    void UpdateHandler(float value)
    {
        spriteRenderer.color = new Color(value,value,value);
    }
}
