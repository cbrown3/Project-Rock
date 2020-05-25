using System.Collections;
using UnityEngine;

public class GridMovementController : InputController
{
    public Tile currentTile;

    private Coroutine currentHitStunCoroutine = null;
    public ParticleSystem hitStunParticles;
    private ParticleSystem.MainModule main;

    public bool inHitStun = false;

    private bool isMobile;

    private bool isShielding;

    private void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer1)
        {
            iManager.onP1Movement.AddListener(MoveOneTile);

            //Tile of player on the grid
            currentTile = GridManager.Instance.GetTile(2, 3);
            transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;

            iManager.onP2Movement.AddListener(MoveOneTile);

            //Tile of player on the grid
            currentTile = GridManager.Instance.GetTile(7, 2);
            transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
        }

        main = hitStunParticles.main;
        isMobile = true;
        isShielding = false;
    }

    public IEnumerator Freeze(float cooldown)
    {
        GetComponent<Animator>().Play("FrozenAnim");

        isMobile = false;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        isMobile = true;
        GetComponent<Animator>().Rebind();
    }
    
    public void ShieldStop(bool shielding)
    {
        isShielding = shielding;
    }

    public IEnumerator Immobilize(float cooldown)
    {
        isMobile = false;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        isMobile = true;
    }
    
    public void ActivateHitStun(float cooldown)
    {
        if (currentHitStunCoroutine != null)
        {
            GameManager.Instance.comboCounter[1]++;
            StopCoroutine(currentHitStunCoroutine);
        }
        currentHitStunCoroutine = StartCoroutine(HitStun(cooldown));
    }

    public IEnumerator HitStun(float cooldown)
    {
        GetComponent<Animator>().Play("HitStunAnim");
        main.simulationSpeed = 1 / cooldown;
        hitStunParticles.Play(true);

        inHitStun = true;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        GameManager.Instance.comboCounter[1] = 0;

        inHitStun = false;
        GetComponent<Animator>().Rebind();
        hitStunParticles.Stop(true);
    }

    public void MoveOneTile(int direction)
    {
        if(isMobile && !isShielding)
        {
            int futureTileIndex = currentTile.GetTileIndex();
            //moving up
            if (direction == 0)
            {
                futureTileIndex -= 8;
                
                if (futureTileIndex < 0)
                {
                    return;
                }

                if (currentTile.HasTileUp() && GridManager.Instance.GetTile(futureTileIndex).GetIsTraversable())
                {
                    currentTile = GridManager.Instance.GetTile(futureTileIndex);
                    transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
                }
            }
            //moving right
            else if (direction == 1)
            {
                futureTileIndex++;

                if (futureTileIndex > 31)
                {
                    return;
                }

                if (currentTile.HasTileRight())
                {
                    if (GridManager.Instance.GetTile(futureTileIndex).GetIsTraversable())
                    {
                        if(isPlayer1)
                        {
                            if(GridManager.Instance.GetTile(futureTileIndex).GetIsPlayer1())
                            {
                                currentTile = GridManager.Instance.GetTile(futureTileIndex);
                                transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
                            }
                        }
                        else
                        {
                            currentTile = GridManager.Instance.GetTile(futureTileIndex);
                            transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
                        }
                    }
                }
            }
            //moving down
            else if (direction == 2)
            {
                futureTileIndex += 8;

                if (futureTileIndex > 31)
                {
                    return;
                }

                if (currentTile.HasTileDown())
                {
                    if (GridManager.Instance.GetTile(futureTileIndex).GetIsTraversable())
                    {
                        currentTile = GridManager.Instance.GetTile(futureTileIndex);
                        transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
                    }
                }
            }
            //moving left
            else if (direction == 3)
            {
                futureTileIndex--;

                if(futureTileIndex < 0)
                {
                    return;
                }

                if (currentTile.HasTileLeft())
                {
                    if(GridManager.Instance.GetTile(futureTileIndex).GetIsTraversable())
                    {
                        if (isPlayer1)
                        {
                            currentTile = GridManager.Instance.GetTile(futureTileIndex);
                            transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
                        }
                        else
                        {
                            if (!GridManager.Instance.GetTile(futureTileIndex).GetIsPlayer1())
                            {
                                currentTile = GridManager.Instance.GetTile(futureTileIndex);
                                transform.position = currentTile.transform.position + new Vector3(0, 0.5f, 0);
                            }
                        }
                    }
                }
            }
        }
    }
}
