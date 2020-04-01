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
public class OnP2Shield : UnityEvent<bool> { }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
