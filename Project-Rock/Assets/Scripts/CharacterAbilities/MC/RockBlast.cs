using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlast : MonoBehaviour
{
    public bool IsPlayer1 { get; set; }

    public int Damage { get; set; }

    public float hitStun = 3f;

    private Rigidbody2D rigid;

    private float lifetime = 5f;

    public float shieldStun = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        switch (IsPlayer1)
        {
            case true:
                tag = "P1RockBlast";
                rigid.AddForce(new Vector2(10f, 0), ForceMode2D.Impulse);
                break;

            case false:
                tag = "P2RockBlast";
                rigid.AddForce(new Vector2(-10f, 0), ForceMode2D.Impulse);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 ||
            transform.position.x < -10)
        {
            Destroy(gameObject);
        }

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (IsPlayer1)
        {
            case true:
                if (collision.tag == "Player2")
                {
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<SpriteRenderer>());
                    collision.GetComponent<GridMovementController>().ActivateHitStun(hitStun);
                }

                if (collision.tag == "P2RockPillar")
                {
                    Destroy(gameObject);
                }

                if (collision.tag == "P2Shield")
                {
                    collision.GetComponent<ShieldManager>().ActivateShieldStun(shieldStun);
                    collision.GetComponent<ShieldManager>().TakeDamage(Damage);
                    Destroy(gameObject);
                }
                break;

            case false:
                if (collision.tag == "Player1")
                {
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<SpriteRenderer>());
                    collision.GetComponent<GridMovementController>().ActivateHitStun(hitStun);
                }

                if (collision.tag == "P1RockPillar")
                {
                    Destroy(gameObject);
                }

                if (collision.tag == "P1Shield")
                {
                    collision.GetComponent<ShieldManager>().ActivateShieldStun(shieldStun);
                    collision.GetComponent<ShieldManager>().TakeDamage(Damage);
                    Destroy(gameObject);
                }
                break;
        }
    }
}
