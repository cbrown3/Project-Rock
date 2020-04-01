using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

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
        animator = GetComponent<Animator>();
        iManager = GetComponent<InputManager>();
        basicShotPrefab = Resources.Load("Abilities/MCBasicShot") as GameObject;

        if (isPlayer1)
        {
            iManager.onP1BasicShot.AddListener(Shoot);
        }
        else
        {
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
