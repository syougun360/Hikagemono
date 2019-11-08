/// ----------------------------------------------------
/// シーン管理部
/// 
/// ここでシーンの管理をします
/// シーンの管理方法は、.unityのシーンを遷移はしてない
/// Resrouces/PreafbsにあるPrefabを生成して、シーンを使っている。
/// 使い終わったらUnLoadしてメモリから削除しています。
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------


using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    /// <summary>
    /// シーンのプレハブフォルダー名
    /// </summary>
    const string scenePrefabFolderName = "ScenePrefabs/";

    /// <summary>
    /// ローディングフォルダー名
    /// </summary>
    const string loadingFolderName = "Loading";

    static SceneManager instance = null;
    public static SceneManager Instance
    {
        get
        { 
            if(instance == null)
            {
                var obj = new GameObject("SceneManager");
                instance = obj.AddComponent<SceneManager>();
                instance.GetComponent<SceneManager>().enabled = true;
                instance.gameObject.AddComponent<FadeManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// フェードタイム
    /// </summary>
    FadeTimeData fadeTime = new FadeTimeData(1, 1);

    /// <summary>
    /// フェードするマネージャ
    /// </summary>
    FadeManager fadeManager = null;

    /// <summary>
    /// 現在のシーンGameObject
    /// </summary>
    GameObject scene = null;

    /// <summary>
    /// ローディングインスタンス
    /// </summary>
    GameObject loadingInstance = null;

    /// <summary>
    /// ローディング管理
    /// </summary>
    LoadingManager loadingManager = null;

    /// <summary>
    /// 現在のシーン
    /// </summary>
    SceneNameManager.Scene nowScene = SceneNameManager.Scene.Title;

    /// <summary>
    /// 次のシーン
    /// </summary>
    SceneNameManager.Scene nextScene = SceneNameManager.Scene.Title;

    /// <summary>
    /// 状態
    /// </summary>
    enum State
    {
        None,
        Change,
        Loading,
    };
    /// <summary>
    /// シーン管理の状態
    /// </summary>
    State state = State.None;

    /// <summary>
    /// ローディングするかどうか
    /// </summary>
    bool isLoading = false;

    /// <summary>
    /// ここでシーンの初期化・アプリケーションの設定をしている
    /// 
    /// </summary>
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        fadeManager = GetComponent<FadeManager>();
        fadeManager.StartFadeIn(fadeTime.inTime);

        scene = GameObject.Find(nowScene.ToString());
    }

    /// <summary>
    /// シーンを切り替える準備
    /// </summary>
    public void ChangeSceneLoad(SceneNameManager.Scene scene,bool isLoading)
    {
        if (state != State.None) return;

        fadeManager.StartFadeOut(fadeTime.outTime);
        nextScene = scene;
        state = State.Change;
        this.isLoading = isLoading;
    }


    void Update()
    {
        ChangeScene();
        LoadingFade();
    }

    /// <summary>
    /// ローディングのフェードイン
    /// </summary>
    void LoadingFade()
    {
        if (state != State.Loading) return;
        if (GameObject.Find(nowScene.ToString()) == null) return;

        loadingManager.StartFade();
        isLoading = false;
        state = State.None;
    }


    /// <summary>
    /// シーンを切り替える
    /// </summary>
    void ChangeScene()
    {
        if (state != State.Change) return;
        if (fadeManager.IsFading) return;

        UnLoadDestroy();
        fadeManager.StartFadeIn(fadeTime.inTime);

        state = CheckLoadingScene();
        nowScene = nextScene;
    }

    /// <summary>
    /// ローディングかどうかをチェックする。
    /// もし、ローディングなら、ローディング画面を生成し、そのあとで、次のシーンを生成する
    /// ローディングではない場合は、そのまま次のシーンを生成する。
    /// </summary>
    /// <returns>ローディングによっての状態</returns>
    State CheckLoadingScene()
    {
        if (isLoading)
        {
            loadingInstance = (GameObject)Instantiate(Resources.Load(scenePrefabFolderName + loadingFolderName));
            loadingManager = loadingInstance.GetComponent<LoadingManager>();
            loadingManager.SetLoadingData(fadeManager, fadeTime);
            StartCoroutine("WaitCreateScene");
            return State.Loading;
        }
        else
        {
            CreateScene();
            return State.None;
        }
    }

    /// <summary>
    /// データを削除する
    /// </summary>
    void UnLoadDestroy()
    {
        Destroy(scene);

        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// シーンを生成する
    /// </summary>
    void CreateScene()
    {
        scene = (GameObject)Instantiate(Resources.Load(scenePrefabFolderName + nextScene.ToString()),
                                            new Vector3(0, 0, 0), Quaternion.identity);
        scene.name = nextScene.ToString();
    }

    IEnumerator WaitCreateScene()
    {
        yield return new WaitForSeconds(1.0f);
        CreateScene();
    }

}
