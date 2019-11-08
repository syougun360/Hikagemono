/// ----------------------------------------------------
/// 背景の色を変える
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackGroundColorChanger : MonoBehaviour {

    [System.Serializable]
    public struct ColorChangeData
    {
        public ColorChangeData(int changeTime, Color color)
            : this()
        {
            this.changeTime = changeTime;
            this.color = color;
        }

        public int changeTime;
        public Color color;

    };

    [SerializeField]
    float changeTime = 0.5f;

    [SerializeField]
    List<ColorChangeData> changeColors = new List<ColorChangeData>();

    SpriteRenderer sprite = null;
    Color color;
    float oldTime = -1;


    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oldTime == TimeManager.ChangedTime) return;

        oldTime = TimeManager.ChangedTime;

        foreach (var changeColor in changeColors)
        {
            if (changeColor.changeTime <= TimeManager.ChangedTime)
            {
                color = changeColor.color;
            }
        }

        iTween.ValueTo(gameObject, iTween.Hash("from", sprite.color, "to", color, "time", changeTime, "onupdate", "ChnageUpdate"));
    }

    void ChnageUpdate(Color value)
    {
        sprite.color = value;
    }

}
