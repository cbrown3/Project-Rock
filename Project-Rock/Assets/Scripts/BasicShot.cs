using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour
{
    public bool IsPlayer1 { get; set; }

    public int Damage { get; set; }

    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        switch (IsPlayer1)
        {
            case true:
                rigid.AddForce(new Vector2(100.0f, 0), ForceMode2D.Impulse);
                break;

            case false:
                rigid.AddForce(new Vector2(-100.0f, 0), ForceMode2D.Impulse);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Damage = 10;
        if(transform.position.x > 10 ||
            transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(IsPlayer1)
        {
            case true:
                if (collision.tag == "Player2")
                {
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    Destroy(gameObject);
                }
                break;

            case false:
                if (collision.tag == "Player1")
                {
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    Destroy(gameObject);
                }
                break;
        }

        if(collision.tag == "Shield")
        {
            collision.GetComponent<ShieldManager>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
