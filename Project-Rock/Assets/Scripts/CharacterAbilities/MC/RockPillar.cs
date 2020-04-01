using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPillar : MonoBehaviour
{
    public bool IsPlayer1 { get; set; }

    public float pillarMaxHealth;
    private float currentPillarHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentPillarHealth = pillarMaxHealth;

        if(IsPlayer1)
        {
            tag = "P1RockPillar";
        }
        else
        {
            tag = "P2RockPillar";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(IsPlayer1)
        {
            case true:
                if(collision.tag == "P2BasicShot")
                {
                    TakeDamage(collision.GetComponent<BasicShot>().Damage);
                    Destroy(collision.gameObject);
                }

                if (collision.tag == "P2Icicle")
                {
                    TakeDamage(collision.GetComponent<Icicle>().Damage);
                    Destroy(collision.gameObject);
                }

                if (collision.tag == "P2IcicleShard")
                {
                    TakeDamage(collision.GetComponent<IcicleShard>().Damage);
                    Destroy(collision.gameObject);
                }

                if (collision.tag == "P2FireColumn")
                {
                    TakeDamage(collision.GetComponent<FireColumn>().Damage);
                    Destroy(collision.gameObject);
                }
                break;

            case false:

                if (collision.tag == "P1BasicShot")
                {
                    TakeDamage(collision.GetComponent<BasicShot>().Damage);
                    Destroy(collision.gameObject);
                }

                if (collision.tag == "P1Icicle")
                {
                    TakeDamage(collision.GetComponent<Icicle>().Damage);
                    Destroy(collision.gameObject);
                }

                if (collision.tag == "P1IcicleShard")
                {
                    TakeDamage(collision.GetComponent<IcicleShard>().Damage);
                    Destroy(collision.gameObject);
                }

                if (collision.tag == "P1FireColumn")
                {
                    TakeDamage(collision.GetComponent<FireColumn>().Damage);
                    Destroy(collision.gameObject);
                }
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        currentPillarHealth -= damage;

        if(currentPillarHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
