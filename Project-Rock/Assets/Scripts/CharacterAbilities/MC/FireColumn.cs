using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class FireColumn : MonoBehaviour
{
    public bool IsPlayer1 { get; set; }

    public int Damage { get; set; }

    public float hitStun = 0.05f;

    public float shieldStun = 0.025f;

    public float meterGain = 0.2f;

    private bool onDamageCooldown = false;

    private float lifetime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if(IsPlayer1)
        {
            tag = "P1FireColumn";
        }
        else
        {
            tag = "P2FireColumn";
        }
    }

    // Update is called once per frame
    void Update()
    {
        Damage = 10;

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (IsPlayer1)
        {
            case true:
                if (!onDamageCooldown && collision.tag == "Player2")
                {
                    GameManager.Instance.superMeter[0].value += meterGain;
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    collision.GetComponent<GridMovementController>().ActivateHitStun(hitStun);
                    StartCoroutine(DamageCooldown(0.2f));
                }

                if(collision.tag == "P2RockPillar")
                {
                    Destroy(gameObject);
                }
                break;

            case false:
                if (!onDamageCooldown && collision.tag == "Player1")
                {
                    GameManager.Instance.superMeter[1].value += meterGain;
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    collision.GetComponent<GridMovementController>().ActivateHitStun(hitStun);
                    StartCoroutine(DamageCooldown(0.75f));
                }

                if (collision.tag == "P1RockPillar")
                {
                    Destroy(gameObject);
                }
                break;
        }

        if (collision.tag.Contains("Shield"))
        {
            if(!onDamageCooldown)
            {
                collision.GetComponent<ShieldManager>().ActivateShieldStun(shieldStun);
                collision.GetComponent<ShieldManager>().TakeDamage(Damage);
                StartCoroutine(DamageCooldown(0.75f));
            }
        }
    }

    private IEnumerator DamageCooldown(float cooldown)
    {
        onDamageCooldown = true;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        onDamageCooldown = false;
    }
}
