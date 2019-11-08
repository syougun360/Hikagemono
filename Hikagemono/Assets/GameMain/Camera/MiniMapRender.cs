using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniMapRender : MonoBehaviour {

    [SerializeField]
    MapTextureRenderCreator mapTexture = null;

    Image image = null;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = Sprite.Create(mapTexture.screenTexture,
            new Rect(0, 0, Screen.width, Screen.height), Vector2.zero);
    }

    void Update()
    {
    }

}
