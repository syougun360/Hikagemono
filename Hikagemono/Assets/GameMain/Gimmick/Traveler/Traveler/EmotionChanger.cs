using UnityEngine;
using System.Collections;

public class EmotionChanger : MonoBehaviour {

    [SerializeField]
    TravelerStateManager state = null;

    /// <summary>
    /// 悲しみに包まれているときの画像
    /// </summary>
    [SerializeField]
    Sprite sadSprite = null;

    /// <summary>
    /// 楽しい気分の画像
    /// </summary>
    [SerializeField]
    Sprite funnySprite = null;

    /// <summary>
    /// 楽しい気分かい！？
    /// </summary>
    bool isFunny = false;

    SpriteRenderer spriteRenderer = null;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        ChangeSprite();

	}
    
    /// <summary>
    /// spriteRendererのspriteを変更する
    /// </summary>
    void ChangeSprite()
    {
        if (state.IsWatching && !isFunny)
        {
            spriteRenderer.sprite = funnySprite;
            isFunny = true;
        }

        if (state.IsIdling && isFunny)
        {
            spriteRenderer.sprite = sadSprite;

            isFunny = false;
        }
    }

}
