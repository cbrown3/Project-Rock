using Photon.Pun;
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
public class OnP1Super : UnityEvent { }
[System.Serializable]
public class OnP1Shield : UnityEvent<bool> { }
[System.Serializable]
public class OnP1Grab : UnityEvent { }

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
public class OnP2Super : UnityEvent { }
[System.Serializable]
public class OnP2Shield : UnityEvent<bool> { }
[System.Serializable]
public class OnP2Grab : UnityEvent { }

public class InputManager : MonoBehaviourPun
{
    public OnP1Movement onP1Movement;
    public OnP1BasicShot onP1BasicShot;
    public OnP1Ability1 onP1Ability1;
    public OnP1Ability2 onP1Ability2;
    public OnP1Ability3 onP1Ability3;
    public OnP1Super onP1Super;
    public OnP1Shield onP1Shield;
    public OnP1Grab onP1Grab;

    public OnP2Movement onP2Movement;
    public OnP2BasicShot onP2BasicShot;
    public OnP2Ability1 onP2Ability1;
    public OnP2Ability2 onP2Ability2;
    public OnP2Ability3 onP2Ability3;
    public OnP2Super onP2Super;
    public OnP2Shield onP2Shield;
    public OnP2Grab onP2Grab;
}
