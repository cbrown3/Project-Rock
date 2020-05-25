using UnityEngine;

public class AbilityController : InputController
{
    protected GridMovementController movementController;
    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
        movementController = GetComponent<GridMovementController>();
    }
}
