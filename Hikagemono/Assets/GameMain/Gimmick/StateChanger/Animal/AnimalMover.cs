/// ----------------------------------------------------
/// アニマル木に向かって移動処理
/// 
/// 
/// code by Yamada Masamitsu
/// ----------------------------------------------------

using UnityEngine;
using System.Collections;

public class AnimalMover : IAnimalState
{
    [SerializeField]
    Transform treeTransform = null;

    [SerializeField]
    float moveTime = 3;

    float currentTime = 0;

    void Init() 
    {
        if (state != State.Init) return;

        iTween.MoveTo(gameObject, 
            new Vector3(treeTransform.position.x, gameObject.transform.position.y, treeTransform.position.z - 1)
            , moveTime);

        currentTime = moveTime;

        state = State.Control;

    }

    // Update is called once per frame
    void Update()
    {
        if (!stateManager.IsMoving) return;

        Init();
        Finish();
        Control();
    }

    void Control()
    {
        if (state != State.Control) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            state = State.Finish;
        }

    }

    void Finish()
    {
        if (state != State.Finish) return;

        stateManager.ChangeEating();

        state = State.Init;
    }

}
