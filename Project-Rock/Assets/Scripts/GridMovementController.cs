using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementController : MonoBehaviour
{
    private InputManager iManager;

    public bool isPlayer1;
    public int[] GridPosition { get; set; }

    private Transform startingPlatform;

    private bool isMobile;

    // Start is called before the first frame update
    void Start()
    {
        iManager = GameObject.Find("GameManager").GetComponent<InputManager>();

        if (isPlayer1)
        {
            iManager.onP1Movement.AddListener(MoveOneTile);

            startingPlatform = GameObject.FindGameObjectWithTag("Player1StartingTile").transform;

            //set player to correct position
            transform.position = new Vector3(startingPlatform.position.x,
                startingPlatform.position.y + 0.25f, 0);


            //Position of player on the grid, ZERO_INDEXED
            GridPosition = new int[2];
            //X, or row position
            GridPosition[0] = 1;
            //Y, or column position
            GridPosition[1] = 1;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;

            iManager.onP2Movement.AddListener(MoveOneTile);

            startingPlatform = GameObject.FindGameObjectWithTag("Player2StartingTile").transform;

            //set player to correct position
            transform.position = new Vector3(startingPlatform.position.x,
                startingPlatform.position.y + 0.25f, 0);


            //Position of player on the grid, ZERO_INDEXED
            GridPosition = new int[2];
            //X, or row position
            GridPosition[0] = 6;
            //Y, or column position
            GridPosition[1] = 2;
        }

        isMobile = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void MoveOneTile(int direction)
    {
        if(isMobile)
        {
            //moving up
            if (direction == 0)
            {
                if (GridPosition[1] > 2)
                {
                    return;
                }

                transform.Translate(0, 0.96f, 0);
                GridPosition[1]++;
            }
            //moving right
            else if (direction == 1)
            {
                switch (isPlayer1)
                {
                    case true:
                        if (GridPosition[0] > 2)
                        {
                            return;
                        }

                        transform.Translate(1.25f, 0, 0);
                        GridPosition[0]++;
                        break;

                    case false:
                        if (GridPosition[0] > 6)
                        {
                            return;
                        }

                        transform.Translate(1.25f, 0, 0);
                        GridPosition[0]++;
                        break;
                }
            }
            //moving down
            else if (direction == 2)
            {

                if (GridPosition[1] < 1)
                {
                    return;
                }

                transform.Translate(0, -0.96f, 0);
                GridPosition[1]--;
            }
            //moving left
            else if (direction == 3)
            {

                switch (isPlayer1)
                {
                    case true:
                        if (GridPosition[0] < 1)
                        {
                            return;
                        }

                        transform.Translate(-1.25f, 0, 0);
                        GridPosition[0]--;
                        break;

                    case false:
                        if (GridPosition[0] < 5)
                        {
                            return;
                        }

                        transform.Translate(-1.25f, 0, 0);
                        GridPosition[0]--;
                        break;
                }
            }
        }
    }
}
