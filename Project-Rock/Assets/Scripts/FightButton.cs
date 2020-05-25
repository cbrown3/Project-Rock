using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FightButton : Button
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        if(GameManager.Instance.playersReady[0] == true &&
            GameManager.Instance.playersReady[1] == true)
        {
            interactable = true;
        }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        interactable = false;
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);

        GameManager.Instance.LoadGame();
    }
}
