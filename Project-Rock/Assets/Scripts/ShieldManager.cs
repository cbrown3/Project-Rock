using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldManager : MonoBehaviour
{
    public bool isPlayer1;
    private InputManager iManager;

    public Slider shieldHealthSlider;
    public float shieldMaxHealth;
    private float currentShieldHealth;

    public CircleCollider2D playerHurtBox;
    private CircleCollider2D collider;
    private SpriteRenderer sprite;

    private int parryTimer;
    private bool localIsShielding;
    // Start is called before the first frame update
    void Start()
    {
        iManager = GameObject.Find("GameManager").GetComponent<InputManager>();

        if (isPlayer1)
        {
            iManager.onP1Shield.AddListener(Shield);
        }
        else
        {
            iManager.onP2Shield.AddListener(Shield);
        }

        shieldHealthSlider.maxValue = shieldMaxHealth;
        shieldHealthSlider.value = currentShieldHealth = shieldMaxHealth;

        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        sprite.enabled = false;
        collider.enabled = false;

        parryTimer = 0;
        localIsShielding = false;
    }

    // Update is called once per frame
    void Update()
    {
        shieldHealthSlider.value = currentShieldHealth;

        if(currentShieldHealth < shieldMaxHealth)
        {
            currentShieldHealth += 0.1f;
        }

        if(localIsShielding && parryTimer < 12)
        {
            parryTimer++;
        }
    }

    public void Shield(bool isShielding)
    {
        if(isShielding && currentShieldHealth > 0)
        {
            sprite.enabled = true;
            collider.enabled = true;
            playerHurtBox.enabled = false;

            localIsShielding = true;
        }
        else
        {
            sprite.enabled = false;
            collider.enabled = false;
            playerHurtBox.enabled = true;
            localIsShielding = false;
            parryTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    
    public void TakeDamage(int damage)
    {
        if(parryTimer < 10)
        {
            print("parry!");
        }
        else
        {
            currentShieldHealth -= damage / 2;
            GetComponentInParent<HealthManager>().TakeDamage(damage / 2);
        }

        if(currentShieldHealth <= 0)
        {
            print("shield broken!");
            StartCoroutine(GetComponentInParent<GridMovementController>().Immobilize(3f));
        }
    }
}
