using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1InputManager : InputManager
{
    public float P1MovementDelay { get; set; }

    private GridMovementController gmController;

    private bool onCooldown = false;
    private bool isMoving = false;


    private void Awake()
    {
        SceneManager.sceneLoaded += LoadGridController;
    }

    private void LoadGridController(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "GameScene")
        {
            Scene gameScene = SceneManager.GetSceneByName("GameScene");

            GameObject[] gameObjects = gameScene.GetRootGameObjects();

            for(int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].name == "Player1")
                {
                    gmController = gameObjects[i].GetComponent<GridMovementController>();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        P1MovementDelay = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        else
        {
            if (SceneManager.GetSceneByName("GameScene").isLoaded)
            {
                if (!gmController.inHitStun)
                {
                    if (!onCooldown)
                    {
                        CheckP1Movement();
                        CheckP1Shield();
                        if (GameManager.Instance.currentGameState == GameManager.GameState.Playing ||
                            GameManager.Instance.currentGameState == GameManager.GameState.RoundEnd)
                        {
                            CheckP1Attacks();
                        }
                    }
                }
            }
        }
    }

    private void CheckP1Movement()
    {
        if (Input.GetAxis("P1Horizontal") != 0 ||
            Input.GetAxis("P1Vertical") != 0)
        {
            if (!isMoving)
            {
                if (Input.GetAxisRaw("P1Horizontal") > 0)
                {
                    onP1Movement.Invoke(1);
                    StartCoroutine(ActionCooldown(P1MovementDelay));
                }
                else if (Input.GetAxisRaw("P1Horizontal") < 0)
                {
                    onP1Movement.Invoke(3);
                    StartCoroutine(ActionCooldown(P1MovementDelay));
                }
                if (Input.GetAxisRaw("P1Vertical") > 0)
                {
                    onP1Movement.Invoke(0);
                    StartCoroutine(ActionCooldown(P1MovementDelay));
                }
                else if (Input.GetAxisRaw("P1Vertical") < 0)
                {
                    onP1Movement.Invoke(2);
                    StartCoroutine(ActionCooldown(P1MovementDelay));
                }

                isMoving = true;
            }
        }
        if (Input.GetAxisRaw("P1Horizontal") == 0 &&
            Input.GetAxisRaw("P1Vertical") == 0)
        {
            isMoving = false;
        }
    }
    
    private void CheckP1Attacks()
    {
        if (!onCooldown)
        {
            if (Input.GetAxisRaw("P1BasicShot") > 0)
            {
                StartCoroutine(ActionCooldown(0.5f));
                onP1BasicShot.Invoke();
            }

            if (Input.GetAxisRaw("P1Ability1") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Ability1.Invoke();
            }
            else if (Input.GetAxisRaw("P1Ability2") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Ability2.Invoke();
            }
            else if (Input.GetAxisRaw("P1Ability3") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Ability3.Invoke();
            }
            else if (Input.GetAxisRaw("P1Super") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Super.Invoke();
            }
            else if(Input.GetAxisRaw("P1Grab") > 0)
            {
                StartCoroutine(ActionCooldown(0.5f));
                onP1Grab.Invoke();
            }
        }
    }

    private void CheckP1Shield()
    {
        if (!onCooldown)
        {
            if (Input.GetAxisRaw("P1Shield") > 0)
            {
                if (Input.GetAxisRaw("P1BasicShot") > 0)
                {
                    StartCoroutine(ActionCooldown(0.5f));
                    onP1Shield.Invoke(false);
                    onP1Grab.Invoke();
                }
                else
                {
                    onP1Shield.Invoke(true);
                }
            }
            else if (Input.GetAxisRaw("P1Shield") == 0)
            {
                onP1Shield.Invoke(false);
            }
        }
    }

    private IEnumerator ActionCooldown(float cooldown)
    {
        onCooldown = true;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        onCooldown = false;
    }
}
