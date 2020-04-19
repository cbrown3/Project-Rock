using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShieldManager : MonoBehaviour
{
    public bool isPlayer1;
    private InputManager iManager;
    private GridMovementController movementController;

    public Slider shieldHealthSlider;
    public float shieldMaxHealth;
    private float currentShieldHealth;

    public BoxCollider2D playerHurtBox;
    private CircleCollider2D collider;
    private SpriteRenderer sprite;

    public ParticleSystem parryParticles;
    public ParticleSystem shieldStunParticles;

    private Color defaultShieldColor;

    private int parryTimer;
    private bool localIsShielding;
    private bool shieldBroken;
    private bool inShieldStun;

    // Start is called before the first frame update
    void Start()
    {
        Scene preloadScene = SceneManager.GetSceneByName("PreloadingScene");

        GameObject[] preloadGOs = preloadScene.GetRootGameObjects();

        movementController = GetComponentInParent<GridMovementController>();

        if (isPlayer1)
        {
            for (int i = 0; i < preloadGOs.Length; i++)
            {
                if (preloadGOs[i].name == "P1InputManager")
                {
                    iManager = preloadGOs[i].GetComponent<P1InputManager>();
                }
            }
            iManager.onP1Shield.AddListener(Shield);
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
            iManager.onP2Shield.AddListener(Shield);
        }

        shieldHealthSlider.maxValue = shieldMaxHealth;
        shieldHealthSlider.value = currentShieldHealth = shieldMaxHealth;

        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        defaultShieldColor = sprite.color;
        sprite.enabled = false;
        collider.enabled = false;

        parryTimer = 0;
        localIsShielding = false;
    }

    // Update is called once per frame
    void Update()
    {
        shieldHealthSlider.value = currentShieldHealth;

        if(localIsShielding && parryTimer < 12)
        {
            sprite.color = new Color(255,255,255,0.5f);
            parryTimer++;
        }
        else
        {
            sprite.color = defaultShieldColor;
        }

        if(localIsShielding)
        {
            currentShieldHealth -= 0.1f;
        }
    }

    public void Shield(bool isShielding)
    {
        localIsShielding = isShielding;

        if (!inShieldStun)
        {
            if (isShielding && currentShieldHealth > 0 && !shieldBroken)
            {
                sprite.enabled = true;
                collider.enabled = true;
                playerHurtBox.enabled = false;

                movementController.ShieldStop(true);
            }
            else
            {
                sprite.enabled = false;
                collider.enabled = false;
                playerHurtBox.enabled = true;
                parryTimer = 0;

                movementController.ShieldStop(false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(parryTimer < 10)
        {
            parryParticles.Play();
            print("parry!");
        }
        else
        {
            currentShieldHealth -= damage;
        }

        if(currentShieldHealth <= 0)
        {
            print("shield broken!");
            shieldBroken = true;
        }
    }

    public void ActivateShieldStun(float lag)
    {
        if(parryTimer >= 10)
        {
            StartCoroutine(ShieldStun(lag));
        }
    }

    public IEnumerator ShieldStun(float lag)
    {
        shieldStunParticles.Play(true);
        inShieldStun = true;
        
        while (lag > 0f)
        {
            lag -= Time.deltaTime;
            yield return null;
        }

        inShieldStun = false;

        if(localIsShielding)
        {
            Shield(true);
        }
        else
        {
            Shield(false);
        }
    }

    public void ResetShield()
    {
        currentShieldHealth = shieldMaxHealth;
    }
}
