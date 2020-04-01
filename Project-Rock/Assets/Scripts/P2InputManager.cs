using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2InputManager : InputManager
{

    public float P2MovementDelay { get; set; }
    public bool inHitStun = false;

    private Coroutine currentHitStunCoroutine = null;
    public ParticleSystem hitStunParticles;
    private ParticleSystem.MainModule main;

    private bool onCooldown = false;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        P2MovementDelay = 0.1f;
        main = hitStunParticles.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inHitStun)
        {
            CheckP2Shield();

            if (!onCooldown)
            {
                CheckP2Movement();
                if (GameManager.Instance.currentGameState == GameManager.GameState.Playing ||
                    GameManager.Instance.currentGameState == GameManager.GameState.RoundEnd)
                {
                    CheckP2Attacks();
                }
            }
        }
    }
    
    private void CheckP2Movement()
    {
        if (Input.GetAxis("P2Horizontal") != 0 ||
            Input.GetAxis("P2Vertical") != 0)
        {
            if (!isMoving)
            {
                if (Input.GetAxisRaw("P2Horizontal") > 0)
                {
                    onP2Movement.Invoke(1);
                    StartCoroutine(ActionCooldown(P2MovementDelay));
                }
                else if (Input.GetAxisRaw("P2Horizontal") < 0)
                {
                    onP2Movement.Invoke(3);
                    StartCoroutine(ActionCooldown(P2MovementDelay));
                }
                if (Input.GetAxisRaw("P2Vertical") > 0)
                {
                    onP2Movement.Invoke(0);
                    StartCoroutine(ActionCooldown(P2MovementDelay));
                }
                else if (Input.GetAxisRaw("P2Vertical") < 0)
                {
                    onP2Movement.Invoke(2);
                    StartCoroutine(ActionCooldown(P2MovementDelay));
                }

                isMoving = true;
            }
        }
        if (Input.GetAxisRaw("P2Horizontal") == 0 &&
            Input.GetAxisRaw("P2Vertical") == 0)
        {
            isMoving = false;
        }
    }
    
    private void CheckP2Attacks()
    {
        if (!onCooldown)
        {
            if (Input.GetAxisRaw("P2BasicShot") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2BasicShot.Invoke();
            }

            if (Input.GetAxisRaw("P2Ability1") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ability1.Invoke();
            }
            else if (Input.GetAxisRaw("P2Ability2") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ability2.Invoke();
            }
            else if (Input.GetAxisRaw("P2Ability3") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ability3.Invoke();
            }
            else if (Input.GetAxisRaw("P2Super") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ultimate.Invoke();
            }
        }
    }
    
    private void CheckP2Shield()
    {
        if (Input.GetAxisRaw("P2Shield") > 0)
        {
            onP2Shield.Invoke(true);
        }
        else if (Input.GetAxisRaw("P2Shield") == 0)
        {
            onP2Shield.Invoke(false);
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

    public void ActivateHitStun(float cooldown)
    {
        if (currentHitStunCoroutine != null)
        {
            GameManager.Instance.comboCounter[0]++;
            StopCoroutine(currentHitStunCoroutine);
        }
        currentHitStunCoroutine = StartCoroutine(HitStun(cooldown));
    }


    private IEnumerator HitStun(float cooldown)
    {
        GetComponent<Animator>().Play("HitStunAnim");
        main.simulationSpeed = 1/cooldown;
        hitStunParticles.Play(true);

        inHitStun = true;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        GameManager.Instance.comboCounter[0] = 0;

        inHitStun = false;
        GetComponent<Animator>().Rebind();
        hitStunParticles.Stop(true);
    }
}
