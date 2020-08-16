using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockBlastController : AbilityController
{
    private Slider rockBlastSuperMeter;
    private GameObject rockBlastPrefab;
    private RockBlast rockBlastInstance;

    private MCAbilityController abilityController;

    private bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        rockBlastPrefab = Resources.Load("Abilities/RockBlast") as GameObject;

        abilityController = GetComponent<MCAbilityController>();

        if(isPlayer1)
        {
            iManager.onP1Super.AddListener(ActivateRockBlast);
            rockBlastSuperMeter = GameManager.Instance.superMeter[0];
        }
        else
        {
            iManager.onP2Super.AddListener(ActivateRockBlast);
            rockBlastSuperMeter = GameManager.Instance.superMeter[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated && rockBlastSuperMeter.value > 0)
        {
            rockBlastSuperMeter.value -= 0.1f;
        }
        else if(rockBlastSuperMeter.value == 0)
        {
            isActivated = false;
        }
    }

    void ActivateRockBlast()
    {
        if(rockBlastSuperMeter.value == rockBlastSuperMeter.maxValue)
        {
            isActivated = true;
            animator.Play("MCSuperAnim");
        }
        else if(isActivated)
        {
            int blastSource = abilityController.currentPillars.Count + 1;
            for (int i = 0; i < blastSource; i++)
            {
                if (i == blastSource - 1)
                {
                    rockBlastInstance = Instantiate(rockBlastPrefab, transform.position, Quaternion.identity).GetComponent<RockBlast>();
                    rockBlastInstance.IsPlayer1 = isPlayer1;
                }
                else
                {
                    if(abilityController.currentPillars[i] != null)
                    {
                        rockBlastInstance = Instantiate(rockBlastPrefab, abilityController.currentPillars[i].transform.position, Quaternion.identity).GetComponent<RockBlast>();
                        rockBlastInstance.IsPlayer1 = isPlayer1;
                    }
                }
            }
        }
    }
}
