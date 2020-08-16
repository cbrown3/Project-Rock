using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    public bool IsPlayer1 { get; set; }

    public int Damage { get; set; }

    public int icicleShardDamage;

    public float meterGain = 5f;

    private Animator anim;
    private GameObject icicleShardPrefab;
    private IcicleShard[] icicleShardInstance;

    private float lifetimeTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (IsPlayer1)
        {
            tag = "P1Icicle";
        }
        else
        {
            tag = "P2Icicle";
        }

        anim = GetComponent<Animator>();

        icicleShardPrefab = Resources.Load("Abilities/IcicleShard") as GameObject;
        icicleShardInstance = new IcicleShard[4];
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeTimer -= Time.deltaTime;

        if(lifetimeTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(IsPlayer1)
        {
            case true:
                if(collision.tag == "Player2")
                {
                    GameManager.Instance.superMeter[0].value += meterGain;
                    collision.GetComponent<HealthManager>().TakeDamage(Damage);
                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<SpriteRenderer>());
                    StartCoroutine(collision.GetComponent<GridMovementController>().Freeze(1f));
                }

                if (collision.tag == "P1BasicShot")
                {
                    Destroy(collision.gameObject);
                    StartCoroutine(Shatter());
                }

                if(collision.tag == "P2RockPillar")
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

                if (collision.tag == "P2BasicShot")
                {
                    Destroy(collision.gameObject);
                    StartCoroutine(Shatter());
                }

                if (collision.tag == "P1RockPillar")
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private IEnumerator Shatter()
    {
        for (int i = 0; i < 4; i++)
        {
            icicleShardInstance[i] = Instantiate(icicleShardPrefab, transform.position, Quaternion.identity).GetComponent<IcicleShard>();
            icicleShardInstance[i].Damage = icicleShardDamage;
            icicleShardInstance[i].Direction = i;
            icicleShardInstance[i].IsPlayer1 = IsPlayer1;
        }

        anim.Play("IcicleShatteringAnim");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
