using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class FireColumn : MonoBehaviour
{
    public bool IsPlayer1 { get; set; }

    public int Damage { get; set; }

    private bool onDamageCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lifetime(5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Damage = 10;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (IsPlayer1)
        {
            case true:
                if (!onDamageCooldown && collision.tag == "Player2")
                {
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    StartCoroutine(DamageCooldown(0.2f));
                }
                break;

            case false:
                if (!onDamageCooldown && collision.tag == "Player1")
                {
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    StartCoroutine(DamageCooldown(0.2f));
                }
                break;
        }

        if (collision.tag == "Shield")
        {
            collision.GetComponent<ShieldManager>().TakeDamage(Damage);
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

    private IEnumerator Lifetime(float cooldown)
    {
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
