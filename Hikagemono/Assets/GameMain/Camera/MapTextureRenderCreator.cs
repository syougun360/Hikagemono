using UnityEngine;
using System.Collections;

public class MapTextureRenderCreator : MonoBehaviour {

    public Texture2D screenTexture { get; private set; }
    RenderTexture renderTexture = null;

    Camera myCamea = null;

    void Awake()
    {
        myCamea = GetComponent<Camera>();
        int width = Screen.width;
        int height = Screen.height;
        screenTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        renderTexture = new RenderTexture(width, height, 24);
        myCamea.targetTexture = renderTexture;
    }
    
    void Take()
    {
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();
    }

    void OnPostRender()
    {
        Take();

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
