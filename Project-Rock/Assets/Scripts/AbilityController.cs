using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityController : MonoBehaviour
{
    protected GridMovementController movementController;
    protected InputManager iManager;
    protected Animator animator;
    public bool isPlayer1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Scene preloadScene = SceneManager.GetSceneByName("PreloadingScene");

        GameObject[] preloadGOs = preloadScene.GetRootGameObjects();

        if (isPlayer1)
        {
            for (int i = 0; i < preloadGOs.Length; i++)
            {
                if (preloadGOs[i].name == "P1InputManager")
                {
                    iManager = preloadGOs[i].GetComponent<P1InputManager>();
                }
            }
        }
        else
        {
            for (int i = 0; i < preloadGOs.Length; i++)
            {
                if (preloadGOs[i].name == "P2InputManager")
                {
                    iManager = preloadGOs[i].GetComponent<P2InputManager>();
                }
            }
        }
        animator = GetComponent<Animator>();
        movementController = GetComponent<GridMovementController>();
    }
}
