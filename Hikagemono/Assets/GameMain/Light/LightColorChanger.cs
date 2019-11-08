/// ----------------------------------------------------
/// ライトの色を切り替える
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightColorChanger : MonoBehaviour {

    [System.Serializable]
    public struct ColorChangeData
    {
        public ColorChangeData(int changeTime, Color color):this()
        {
            this.changeTime = changeTime;
            this.color = color;
        }

        public int changeTime ;
        public Color color;

    };

    [SerializeField]
    float changeTime = 0.5f;

    [SerializeField]
    List<ColorChangeData> changeColors = new List<ColorChangeData>();

    Light light = null;
    Color color ;
    float oldTime = -1;


	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (oldTime == TimeManager.ChangedTime) return;

        oldTime = TimeManager.ChangedTime;

        foreach (var changeColor in changeColors)
        {
            if (changeColor.changeTime <= TimeManager.ChangedTime)
            {
                color = changeColor.color;
            }
        }

        iTween.ValueTo(gameObject, iTween.Hash("from", light.color, "to", color, "time", changeTime, "onupdate", "ChnageUpdate"));
	}
    
    void ChnageUpdate(Color value)
    {
        light.color = value;
    }

}
