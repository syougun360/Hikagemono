using UnityEngine;
using System.Collections;

public class CameraStageShowMover : MonoBehaviour {

    [SerializeField]
    Collider deadEndLine = null;

    [SerializeField]
    Transform player = null;

    [SerializeField]
    float moveTime = 3.0f;

    [SerializeField]
    float startMoveTime = 1.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine("StartMove");
	}

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(startMoveTime);

        iTween.MoveTo(gameObject, iTween.Hash("x", player.transform.position.x + 2, "time", moveTime, "easetype", iTween.EaseType.easeInOutQuad));
    }

	// Update is called once per frame
	void Update () {
        moveTime -= Time.deltaTime;
        if (moveTime <= 0)
        {
            GameStateManager.SetControlPlayer();
            Destroy(this);
            deadEndLine.enabled = true;   
        }
	}
}
