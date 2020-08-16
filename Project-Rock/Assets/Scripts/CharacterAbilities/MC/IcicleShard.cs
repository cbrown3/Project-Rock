using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleShard : MonoBehaviour
{
    public bool IsPlayer1 { get; set; }

    public int Direction { get; set; }

    public int Damage { get; set; }

    public float meterGain = 5f;

    private Rigidbody2D rigid;
    private float lifetimeTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (IsPlayer1)
        {
            tag = "P1IcicleShard";
        }
        else
        {
            tag = "P2IcicleShard";
        }

        rigid = GetComponent<Rigidbody2D>();

        switch (Direction)
        {
            case 0:
                rigid.AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
                break;

            case 1:
                transform.rotation *= Quaternion.Euler(0, 0, 90f);
                rigid.AddForce(new Vector2(10f, 0), ForceMode2D.Impulse);
                break;

            case 2:
                rigid.AddForce(new Vector2(0, -10f), ForceMode2D.Impulse);
                break;

            case 3:
                transform.rotation *= Quaternion.Euler(0, 0, 90f);
                rigid.AddForce(new Vector2(-10f, 0), ForceMode2D.Impulse);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x > 10 ||
            transform.position.x < -10 ||
            transform.position.y > 5 ||
            transform.position.y < -5)
        {
            Destroy(gameObject);
        }

        lifetimeTimer -= Time.deltaTime;

        if(lifetimeTimer < 0)
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
                    GameManager.Instance.superMeter[0].value += meterGain;
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<SpriteRenderer>());
                    StartCoroutine(collision.GetComponent<GridMovementController>().Freeze(1f));
                }

                if (collision.tag == "P2Shield")
                {
                    collision.GetComponent<ShieldManager>().ActivateShieldStun(0.01f);
                    collision.GetComponent<ShieldManager>().TakeDamage(Damage);
                    Destroy(gameObject);
                }

                if (collision.tag == "P2RockPillar")
                {
                    Destroy(gameObject);
                }
                break;

            case false:
                if (collision.tag == "Player1")
                {
                    GameManager.Instance.superMeter[1].value += meterGain;
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<SpriteRenderer>());
                    StartCoroutine(collision.GetComponent<GridMovementController>().Freeze(1f));
                }
                
                if (collision.tag == "P1Shield")
                {
                    collision.GetComponent<ShieldManager>().ActivateShieldStun(0.01f);
                    collision.GetComponent<ShieldManager>().TakeDamage(Damage);
                    Destroy(gameObject);
                }

                if (collision.tag == "P1RockPillar")
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
