using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicShotController : MonoBehaviour
{
    public int damage = 3;
    public bool isPlayer1;
    private InputManager iManager;
    private Animator animator;
    private GameObject basicShotPrefab;
    private BasicShot shotInstance;
    // Start is called before the first frame update
    void Awake()
    {
        Scene preloadScene = SceneManager.GetSceneByName("PreloadingScene");

        GameObject[] preloadGOs = preloadScene.GetRootGameObjects();

        animator = GetComponent<Animator>();
        basicShotPrefab = Resources.Load("Abilities/MCBasicShot") as GameObject;

        if (isPlayer1)
        {
            for(int i = 0; i < preloadGOs.Length; i++)
            {
                if(preloadGOs[i].name == "P1InputManager")
                {
                    iManager = preloadGOs[i].GetComponent<P1InputManager>();
                }
            }

            iManager.onP1BasicShot.AddListener(Shoot);
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
            iManager.onP2BasicShot.AddListener(Shoot);
        }
    }

    public void Shoot()
    {
        animator.Play("MCBasicShotAnim");
        shotInstance = Instantiate(basicShotPrefab, transform.position, Quaternion.identity).GetComponent<BasicShot>();
        shotInstance.IsPlayer1 = isPlayer1;
        shotInstance.Damage = damage;
    }
}
