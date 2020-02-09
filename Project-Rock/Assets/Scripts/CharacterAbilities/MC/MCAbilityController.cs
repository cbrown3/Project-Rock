using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCAbilityController : AbilityController
{
    public Slider fireColumnAbilitySlider;
    public float fireColumnCD;
    public int fireColumnDamage;

    private float fireColumnCurrentCD;
    private GameObject fireColumnPrefab;
    private FireColumn[] fireColumnInstance;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        fireColumnPrefab = Resources.Load("Abilities/FireColumn") as GameObject;

        fireColumnInstance = new FireColumn[4];

        fireColumnAbilitySlider.maxValue = fireColumnCD;
        fireColumnAbilitySlider.value = fireColumnCD;

        fireColumnCurrentCD = fireColumnCD;

        if (isPlayer1)
        {
            iManager.onP1Ability1.AddListener(ShootFireColumns);
        }
        else
        {
            iManager.onP2Ability1.AddListener(ShootFireColumns);
        }
    }

    private void Update()
    {
        if(fireColumnCurrentCD < fireColumnCD)
        {
            fireColumnCurrentCD += Time.deltaTime;
        }

        fireColumnAbilitySlider.value = fireColumnCurrentCD;
    }

    public void ShootFireColumns()
    {
        animator.Play("MCBasicShotAnim");

        if (fireColumnCurrentCD >= fireColumnCD)
        {
            if (isPlayer1)
            {
                Vector3 spawnPos = new Vector3(0.64f, transform.position.y + 0.5f, 0);

                for (int i = 0; i < 4; i++)
                {
                    fireColumnInstance[i] = Instantiate(fireColumnPrefab, spawnPos + new Vector3((1.25f) * i, 0), Quaternion.identity).GetComponent<FireColumn>();
                    fireColumnInstance[i].IsPlayer1 = isPlayer1;
                    fireColumnInstance[i].Damage = fireColumnDamage;
                }
            }
            else
            {
                Vector3 spawnPos = new Vector3(-0.64f, transform.position.y + 0.5f, 0);

                for (int i = 0; i < 4; i++)
                {
                    fireColumnInstance[i] = Instantiate(fireColumnPrefab, spawnPos - new Vector3((1.25f) * i, 0), Quaternion.identity).GetComponent<FireColumn>();
                    fireColumnInstance[i].IsPlayer1 = isPlayer1;
                    fireColumnInstance[i].Damage = fireColumnDamage;
                }
            }

            fireColumnCurrentCD = 0;
        }
    }
}
