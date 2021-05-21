using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharSelection : Button
{
    public Animator p1PortraitAnimator;
    public Animator p2PortraitAnimator;
    
    public Button fightButton;
    
    public int charNum;

    private RuntimeAnimatorController loadedAnimator;
    private Image charImage;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        charImage = GetComponentsInChildren<Image>()[1];

        Object obj;
        switch(charNum)
        {
            case 0:
                obj = Resources.Load("Animation/MainCharacterCSS");
                loadedAnimator = obj as RuntimeAnimatorController;
                break;
            case 1:
                obj = Resources.Load("Animation/MainCharacterCSS");
                loadedAnimator = obj as RuntimeAnimatorController;
                break;
            case 2:
                obj = Resources.Load("Animation/MainCharacterCSS");
                loadedAnimator = obj as RuntimeAnimatorController;
                break;
            case 3:
                obj = Resources.Load("Animation/MainCharacterCSS");
                loadedAnimator = obj as RuntimeAnimatorController;
                break;
            case 4:
                obj = Resources.Load("Animation/MainCharacterCSS");
                loadedAnimator = obj as RuntimeAnimatorController;
                break;
            case 5:
                obj = Resources.Load("Animation/MainCharacterCSS");
                loadedAnimator = obj as RuntimeAnimatorController;
                break;
            default:
                obj = Resources.Load<Sprite>("Sprites/LockedCharacterIcon");
                charImage.sprite = obj as Sprite;
                interactable = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);

        if (eventData.currentInputModule.gameObject.name == "P1EventSystem")
        {
            eventData.selectedObject = fightButton.gameObject;
            GameManager.Instance.playersReady[0] = true;
            GameManager.Instance.charSelected[0] = charNum;
        }
        else if (eventData.currentInputModule.gameObject.name == "P2EventSystem")
        {
            eventData.selectedObject = fightButton.gameObject;
            GameManager.Instance.playersReady[1] = true;
            GameManager.Instance.charSelected[1] = charNum;
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        if(eventData == null)
        {
            return;
        }
        else if(eventData.currentInputModule == null)
        {
            return;
        }

        if (eventData.currentInputModule.gameObject.name == "P1EventSystem")
        {
            p1PortraitAnimator.runtimeAnimatorController = loadedAnimator;
        }
        else if (eventData.currentInputModule.gameObject.name == "P2EventSystem")
        {
            p2PortraitAnimator.runtimeAnimatorController = loadedAnimator;
        }
    }
}
