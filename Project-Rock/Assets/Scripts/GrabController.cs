using UnityEngine;

public class GrabController : InputController
{
    public int damage;

    private float grabLength = 0.5f;
    private Animator animator;
    private GridMovementController p1Movement;
    private GridMovementController p2Movement;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        if (isPlayer1)
        {
            p1Movement = GetComponent<GridMovementController>();
            iManager.onP1Grab.AddListener(Grab);

            p2Movement = GameObject.Find("Player2").GetComponent<GridMovementController>();
        }
        else
        {
            p2Movement = GetComponent<GridMovementController>();
            iManager.onP2Grab.AddListener(Grab);

            p1Movement = GameObject.Find("Player1").GetComponent<GridMovementController>();
        }
    }

    void Grab()
    {
        if (isPlayer1)
        {
            StartCoroutine(p1Movement.Immobilize(grabLength));
            animator.Play("Grab");

            if (p2Movement.currentTile.GetTileIndex() == p1Movement.currentTile.GetTileIndex() + 4)
            {
                p2Movement.GetComponent<HealthManager>().TakeDamage(damage);
                p2Movement.ActivateHitStun(grabLength);
            }
            else
            {
                print("grab unsuccessful");
            }
        }
        else
        {
            StartCoroutine(p2Movement.Immobilize(grabLength));
            animator.Play("Grab");

            if (p1Movement.currentTile.GetTileIndex() == p2Movement.currentTile.GetTileIndex() - 4)
            {
                p1Movement.GetComponent<HealthManager>().TakeDamage(damage);
                p1Movement.ActivateHitStun(grabLength);
            }
            else
            {
                print("grab unsuccessful");
            }
        }
    }
}
