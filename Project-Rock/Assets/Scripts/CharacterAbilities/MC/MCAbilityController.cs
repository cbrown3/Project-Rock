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

    public Slider icicleAbilitySlider;
    public float icicleCD;
    public int icicleDamage;

    private float icicleCurrentCD;
    private GameObject iciclePrefab;
    private Icicle icicleInstance;

    public Slider rockPillarAbilitySlider;
    public float rockPillarCD;

    private float rockPillarCurrentCD;
    private GameObject rockPillarPrefab;
    private RockPillar rockPillarInstance;

    private Vector3 spawnPos = new Vector3();
    private int spawnTileIndex = 0;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        fireColumnPrefab = Resources.Load("Abilities/FireColumn") as GameObject;

        fireColumnInstance = new FireColumn[4];

        fireColumnAbilitySlider.maxValue = fireColumnCD;
        fireColumnAbilitySlider.value = fireColumnCD;

        fireColumnCurrentCD = fireColumnCD;

        iciclePrefab = Resources.Load("Abilities/Icicle") as GameObject;

        icicleAbilitySlider.maxValue = icicleCD;
        icicleAbilitySlider.value = icicleCD;

        icicleCurrentCD = icicleCD;

        rockPillarPrefab = Resources.Load("Abilities/RockPillar") as GameObject;

        rockPillarAbilitySlider.maxValue = rockPillarCD;
        rockPillarAbilitySlider.value = rockPillarCD;

        rockPillarCurrentCD = rockPillarCD;

        if (isPlayer1)
        {
            iManager.onP1Ability1.AddListener(ShootFireColumns);
            iManager.onP1Ability2.AddListener(ShootIcicle);
            iManager.onP1Ability3.AddListener(ShootRockPillar);
        }
        else
        {
            iManager.onP2Ability1.AddListener(ShootFireColumns);
            iManager.onP2Ability2.AddListener(ShootIcicle);
            iManager.onP2Ability3.AddListener(ShootRockPillar);
        }
    }

    private void Update()
    {
        if(fireColumnCurrentCD < fireColumnCD)
        {
            fireColumnCurrentCD += Time.deltaTime;
        }

        fireColumnAbilitySlider.value = fireColumnCurrentCD;

        if(icicleCurrentCD < icicleCD)
        {
            icicleCurrentCD += Time.deltaTime;
        }

        icicleAbilitySlider.value = icicleCurrentCD;

        if(rockPillarCurrentCD < rockPillarCD)
        {
            rockPillarCurrentCD += Time.deltaTime;
        }

        rockPillarAbilitySlider.value = rockPillarCurrentCD;
    }

    public void ShootIcicle()
    {

        if (icicleCurrentCD >= icicleCD)
        {
            animator.Play("MCBasicShotAnim");

            if (isPlayer1)
            {
                spawnTileIndex = movementController.currentTile.GetTileIndex() + 4;
                spawnPos = GridManager.Instance.GetTile(spawnTileIndex).transform.position + new Vector3(0, 0.5f, 0);

                icicleInstance = Instantiate(iciclePrefab, spawnPos, Quaternion.identity).GetComponent<Icicle>();
                icicleInstance.IsPlayer1 = isPlayer1;
                icicleInstance.Damage = icicleDamage;
            }
            else
            {
                spawnTileIndex = movementController.currentTile.GetTileIndex() - 4;
                spawnPos = GridManager.Instance.GetTile(spawnTileIndex).transform.position + new Vector3(0, 0.5f, 0);

                icicleInstance = Instantiate(iciclePrefab, spawnPos, Quaternion.identity).GetComponent<Icicle>();
                icicleInstance.IsPlayer1 = isPlayer1;
                icicleInstance.Damage = icicleDamage;
            }

            icicleCurrentCD = 0;
        }
    }
    public void ShootRockPillar()
    {
        if (rockPillarCurrentCD >= rockPillarCD)
        {
            animator.Play("MCBasicShotAnim");

            if (isPlayer1)
            {
                spawnTileIndex = movementController.currentTile.GetTileIndex() + 1;
                spawnPos = GridManager.Instance.GetTile(spawnTileIndex).transform.position + new Vector3(0, 0.5f, 0);

                GridManager.Instance.GetTile(spawnTileIndex).SetIsTraversable(false);

                rockPillarInstance = Instantiate(rockPillarPrefab, spawnPos, Quaternion.identity).GetComponent<RockPillar>();
                rockPillarInstance.IsPlayer1 = isPlayer1;
            }
            else
            {
                spawnTileIndex = movementController.currentTile.GetTileIndex() - 1;
                spawnPos = GridManager.Instance.GetTile(spawnTileIndex).transform.position + new Vector3(0, 0.5f, 0);

                GridManager.Instance.GetTile(spawnTileIndex).SetIsTraversable(false);

                rockPillarInstance = Instantiate(rockPillarPrefab, spawnPos, Quaternion.identity).GetComponent<RockPillar>();
                rockPillarInstance.IsPlayer1 = isPlayer1;
            }

            rockPillarCurrentCD = 0;
        }
    }

    public void ShootFireColumns()
    {
        if (fireColumnCurrentCD >= fireColumnCD)
        {
            animator.Play("MCBasicShotAnim");

            if (isPlayer1)
            {
                spawnTileIndex = movementController.currentTile.GetTileIndex();

                for (int i = 0; i < 4; i++)
                {
                    spawnTileIndex++;
                    spawnPos = GridManager.Instance.GetTile(spawnTileIndex).transform.position + new Vector3(0, 0.5f, 0);

                    if (!GridManager.Instance.GetTile(spawnTileIndex).GetIsPlayer1())
                    {
                        fireColumnInstance[i] = Instantiate(fireColumnPrefab, spawnPos, Quaternion.identity).GetComponent<FireColumn>();
                        fireColumnInstance[i].IsPlayer1 = isPlayer1;
                        fireColumnInstance[i].Damage = fireColumnDamage;
                    }
                    else
                    {
                        i = -1;
                    }

                }
            }
            else
            {
                spawnTileIndex = movementController.currentTile.GetTileIndex();

                for (int i = 0; i < 4; i++)
                {
                    spawnTileIndex--;
                    spawnPos = GridManager.Instance.GetTile(spawnTileIndex).transform.position + new Vector3(0, 0.5f, 0);

                    if (GridManager.Instance.GetTile(spawnTileIndex).GetIsPlayer1())
                    {
                        fireColumnInstance[i] = Instantiate(fireColumnPrefab, spawnPos, Quaternion.identity).GetComponent<FireColumn>();
                        fireColumnInstance[i].IsPlayer1 = isPlayer1;
                        fireColumnInstance[i].Damage = fireColumnDamage;
                    }
                    else
                    {
                        i = -1;
                    }
                }
            }

            fireColumnCurrentCD = 0;
        }
    }
}
