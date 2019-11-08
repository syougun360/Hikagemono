/// ----------------------------------------------------
/// 描画順設定
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortingLayerSetting : MonoBehaviour {

    [System.Serializable]
    public struct SortingSettingData
    {
        public SortingSettingData(GameObject gameObjectLayer,string sortingLayerName):this()
        { 
            this.gameObjectLayer = gameObjectLayer;
            this.sortingLayerName = sortingLayerName;
        }

        public GameObject gameObjectLayer;
        public string sortingLayerName;
    };

    [SerializeField]
    List<SortingSettingData> sortingLayers = new List<SortingSettingData>();

	// Use this for initialization
	void Start () {
        foreach (var layer in sortingLayers)
        {
            layer.gameObjectLayer.GetComponent<MeshRenderer>().sortingLayerName = layer.sortingLayerName;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
