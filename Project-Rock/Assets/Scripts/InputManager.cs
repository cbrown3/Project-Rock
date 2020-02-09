using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnP1Movement : UnityEvent<int> { }
[System.Serializable]
public class OnP1BasicShot : UnityEvent { }
[System.Serializable]
public class OnP1Ability1 : UnityEvent { }
[System.Serializable]
public class OnP1Ability2 : UnityEvent { }
[System.Serializable]
public class OnP1Ability3 : UnityEvent { }
[System.Serializable]
public class OnP1Ultimate : UnityEvent { }
[System.Serializable]
public class OnP1Shield : UnityEvent<bool> { }

[System.Serializable]
public class OnP2Movement : UnityEvent<int> { }
[System.Serializable]
public class OnP2BasicShot : UnityEvent { }
[System.Serializable]
public class OnP2Ability1 : UnityEvent { }
[System.Serializable]
public class OnP2Ability2 : UnityEvent { }
[System.Serializable]
public class OnP2Ability3 : UnityEvent { }
[System.Serializable]
public class OnP2Ultimate : UnityEvent { }
[System.Serializable]
public class OnP2Shield: UnityEvent<bool> { }

public class InputManager : MonoBehaviour
{
    public OnP1Movement onP1Movement;
    public OnP1BasicShot onP1BasicShot;
    public OnP1Ability1 onP1Ability1;
    public OnP1Ability2 onP1Ability2;
    public OnP1Ability3 onP1Ability3;
    public OnP1Ultimate onP1Ultimate;
    public OnP1Shield onP1Shield;

    public OnP2Movement onP2Movement;
    public OnP2BasicShot onP2BasicShot;
    public OnP2Ability1 onP2Ability1;
    public OnP2Ability2 onP2Ability2;
    public OnP2Ability3 onP2Ability3;
    public OnP2Ultimate onP2Ultimate;
    public OnP2Shield onP2Shield;

    private bool onCooldown = false;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckP1Shield();
        CheckP2Shield();

        CheckP1Movement();
        CheckP2Movement();


        if (!onCooldown)
        {
            CheckP1Attacks();
            CheckP2Attacks();
        }
    }

    private void CheckP1Movement()
    {
        if (Input.GetAxis("P1Horizontal") != 0 ||
            Input.GetAxis("P1Vertical") != 0)
        {
            if (!isMoving)
            {
                if (Input.GetAxisRaw("P1Horizontal") > 0)
                {
                    onP1Movement.Invoke(1);
                    //StartCoroutine(ActionCooldown(0.1f));
                }
                else if (Input.GetAxisRaw("P1Horizontal") < 0)
                {
                    onP1Movement.Invoke(3);
                    //StartCoroutine(ActionCooldown(0.1f));
                }
                if (Input.GetAxisRaw("P1Vertical") > 0)
                {
                    onP1Movement.Invoke(0);
                    //StartCoroutine(ActionCooldown(0.1f));
                }
                else if (Input.GetAxisRaw("P1Vertical") < 0)
                {
                    onP1Movement.Invoke(2);
                    //StartCoroutine(ActionCooldown(0.1f));
                }

                isMoving = true;
            }
        }
        if (Input.GetAxisRaw("P1Horizontal") == 0 &&
            Input.GetAxisRaw("P1Vertical") == 0)
        {
            isMoving = false;
        }
    }
    
    private void CheckP1Attacks()
    {
        if (Input.GetAxisRaw("P1BasicShot") > 0)
        {
            StartCoroutine(ActionCooldown(0.1f));
            onP1BasicShot.Invoke();
        }

        if (!onCooldown)
        {
            if (Input.GetAxisRaw("P1Ability1") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Ability1.Invoke();
            }
            else if (Input.GetAxisRaw("P1Ability2") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Ability2.Invoke();
            }
            else if (Input.GetAxisRaw("P1Ability3") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Ability3.Invoke();
            }
            else if (Input.GetAxisRaw("P1Ultimate") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP1Ultimate.Invoke();
            }
        }
    }

    private void CheckP2Movement()
    {
        if (Input.GetAxis("P2Horizontal") != 0 ||
            Input.GetAxis("P2Vertical") != 0)
        {
            if (!isMoving)
            {
                if (Input.GetAxisRaw("P2Horizontal") > 0)
                {
                    onP2Movement.Invoke(1);
                    //StartCoroutine(ActionCooldown(0.1f));
                }
                else if (Input.GetAxisRaw("P2Horizontal") < 0)
                {
                    onP2Movement.Invoke(3);
                    //StartCoroutine(ActionCooldown(0.1f));
                }
                if (Input.GetAxisRaw("P2Vertical") > 0)
                {
                    onP2Movement.Invoke(0);
                    //StartCoroutine(ActionCooldown(0.1f));
                }
                else if (Input.GetAxisRaw("P2Vertical") < 0)
                {
                    onP2Movement.Invoke(2);
                    //StartCoroutine(ActionCooldown(0.1f));
                }

                isMoving = true;
            }
        }
        if (Input.GetAxisRaw("P2Horizontal") == 0 &&
            Input.GetAxisRaw("P2Vertical") == 0)
        {
            isMoving = false;
        }
    }
    
    private void CheckP2Attacks()
    {
        if (Input.GetAxisRaw("P2BasicShot") > 0)
        {
            StartCoroutine(ActionCooldown(0.1f));
            onP2BasicShot.Invoke();
        }

        if (!onCooldown)
        {
            if (Input.GetAxisRaw("P2Ability1") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ability1.Invoke();
            }
            else if (Input.GetAxisRaw("P2Ability2") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ability2.Invoke();
            }
            else if (Input.GetAxisRaw("P2Ability3") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ability3.Invoke();
            }
            else if (Input.GetAxisRaw("P2Ultimate") > 0)
            {
                StartCoroutine(ActionCooldown(0.1f));
                onP2Ultimate.Invoke();
            }
        }
    }

    private void CheckP1Shield()
    {
        if (Input.GetAxisRaw("P1Shield") > 0)
        {
            onP1Shield.Invoke(true);
        }
        if (Input.GetAxisRaw("P1Shield") == 0)
        {
            onP1Shield.Invoke(false);
        }

    }

    private void CheckP2Shield()
    {
        if (Input.GetAxisRaw("P2Shield") > 0)
        {
            onP2Shield.Invoke(true);
        }
        if (Input.GetAxisRaw("P2Shield") == 0)
        {
            onP2Shield.Invoke(false);
        }
    }

    private IEnumerator ActionCooldown(float cooldown)
    {
        onCooldown = true;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        onCooldown = false;
    }

}
