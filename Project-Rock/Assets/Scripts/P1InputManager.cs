using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1InputManager : InputManager
{
    public float P1MovementDelay { get; set; }
    public bool inHitStun = false;

    private Coroutine currentHitStunCoroutine = null;

    public ParticleSystem hitStunParticles;
    private ParticleSystem.MainModule main;

    private bool onCooldown = false;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        P1MovementDelay = 0.1f;
        main = hitStunParticles.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inHitStun)
        {
            CheckP1Shield();

            if (!onCooldown)
            {
                CheckP1Movement();
                if(GameManager.Instance.currentGameState == GameManager.GameState.Playing ||
                    GameManager.Instance.currentGameState == GameManager.GameState.RoundEnd)
                {
                    CheckP1Attacks();
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
                onP1Ultimate.Invoke();
            }
        }
    }

    private void CheckP1Shield()
    {
        if (Input.GetAxisRaw("P1Shield") > 0)
        {
            onP1Shield.Invoke(true);
        }
        else if (Input.GetAxisRaw("P1Shield") == 0)
        {
            onP1Shield.Invoke(false);
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
        if(currentHitStunCoroutine != null)
        {
            GameManager.Instance.comboCounter[1]++;
            StopCoroutine(currentHitStunCoroutine);
        }
        currentHitStunCoroutine = StartCoroutine(HitStun(cooldown));
    }

    public IEnumerator HitStun(float cooldown)
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

        GameManager.Instance.comboCounter[1] = 0;

        inHitStun = false;
        GetComponent<Animator>().Rebind();
        hitStunParticles.Stop(true);
    }
}
