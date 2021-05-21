using UnityEngine;

public class BasicShotController : InputController
{
    public int damage = 3;
    private Animator animator;
    private GameObject basicShotPrefab;
    private BasicShot shotInstance;
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
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
