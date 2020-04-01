using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    protected GridMovementController movementController;
    protected InputManager iManager;
    protected Animator animator;
    public bool isPlayer1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        iManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        movementController = GetComponent<GridMovementController>();
    }
}
