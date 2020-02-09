using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public bool isPlayer1;
    protected InputManager iManager;
    protected Animator animator;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        iManager = GameObject.Find("GameManager").GetComponent<InputManager>();
        animator = GetComponent<Animator>();
    }
}
