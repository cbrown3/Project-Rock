using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject tutorialPanel;
    public GameObject creditsPanel;
    public GameObject charSelectPanel;

    public EventSystem p1EventSystem;
    public EventSystem p2EventSystem;

    public Selectable[] tutorialButtons;
    public Selectable[] creditsButtons;
    public Selectable[] charSelectButtons;
    
    private Selectable p1PreviousButton;
    private Selectable p2PreviousButton;
    [SerializeField] private GameObject p1DefaultButton;
    [SerializeField] private GameObject p2DefaultButton;
    [SerializeField] private GameObject p1CurrentSelected;
    [SerializeField] private GameObject p2CurrentSelected;

    private bool isPlayer2Present = false;

    // Start is called before the first frame update
    void Start()
    {
        if (p1DefaultButton != null)
        {
            p1EventSystem.SetSelectedGameObject(p1DefaultButton);
        }
        if (p2DefaultButton != null)
        {
            p2EventSystem.SetSelectedGameObject(p2DefaultButton);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayer2Present)
        {
            ProcessP1Inputs();
        }
        else
        {
            ProcessP1P2Inputs();
        }
        
        if (GameManager.Instance.playersReady[0] == true)
        {
            if (Input.GetAxisRaw("P1Cancel") > 0)
            {
                GameManager.Instance.playersReady[0] = false;
                p1EventSystem.SetSelectedGameObject(charSelectButtons[1].gameObject);
                GameManager.Instance.charSelected[0] = -1;
            }
        }
        
        if (GameManager.Instance.playersReady[1] == true)
        {
            if (Input.GetAxisRaw("P2Cancel") > 0)
            {
                GameManager.Instance.playersReady[1] = false;
                p2EventSystem.SetSelectedGameObject(charSelectButtons[1].gameObject);
                GameManager.Instance.charSelected[1] = -1;
            }
        }
    }

    private void ProcessP1P2Inputs()
    {
        p1CurrentSelected = p1EventSystem.currentSelectedGameObject;
        p2CurrentSelected = p2EventSystem.currentSelectedGameObject;

        if (p1CurrentSelected == null) return;
        if (p2CurrentSelected == null) return;

        Selectable p1SelectedAsButton = p1CurrentSelected.GetComponent<Selectable>();

        Selectable p2SelectedAsButton = p2CurrentSelected.GetComponent<Selectable>();

        if (p1SelectedAsButton != null && p1SelectedAsButton != p1PreviousButton)
        {
            if(!p1SelectedAsButton.interactable && p1SelectedAsButton.name != "FightButton")
            {
                p1EventSystem.SetSelectedGameObject(p1PreviousButton.gameObject);
                p1SelectedAsButton = p1PreviousButton;
            }

            P1HighlightButton(p1SelectedAsButton);
        }

        if (p1PreviousButton != null && p1PreviousButton != p1SelectedAsButton && p1PreviousButton)
        {
            UnHighlightButton(p1PreviousButton);

            if(p1PreviousButton == p2SelectedAsButton)
            {
                P2HighlightButton(p2SelectedAsButton);
            }
        }

        if (p2SelectedAsButton != null && p2SelectedAsButton != p2PreviousButton)
        {
            if (!p2SelectedAsButton.interactable && p1SelectedAsButton.name != "FightButton")
            {
                p2EventSystem.SetSelectedGameObject(p2PreviousButton.gameObject);
                p2SelectedAsButton = p2PreviousButton;
            }
            P2HighlightButton(p2SelectedAsButton);
        }

        if (p2PreviousButton != null && p2PreviousButton != p2SelectedAsButton)
        {
            UnHighlightButton(p2PreviousButton);

            if (p2PreviousButton == p1SelectedAsButton)
            {
                P1HighlightButton(p1SelectedAsButton);
            }
        }

        p1PreviousButton = p1SelectedAsButton;

        p2PreviousButton = p2SelectedAsButton;
    }

    private void ProcessP1Inputs()
    {
        p1CurrentSelected = p1EventSystem.currentSelectedGameObject;

        if (p1CurrentSelected == null) return;

        Selectable p1SelectedAsButton = p1CurrentSelected.GetComponent<Selectable>();

        if (p1SelectedAsButton != null && p1SelectedAsButton != p1PreviousButton)
        {
            P1HighlightButton(p1SelectedAsButton);
        }

        if (p1PreviousButton != null && p1PreviousButton != p1SelectedAsButton)
        {
            UnHighlightButton(p1PreviousButton);
        }
        
        p1PreviousButton = p1SelectedAsButton;
    }

    void OnDisable()
    {
        if (p1PreviousButton != null)
        {
            UnHighlightButton(p1PreviousButton);
        }
    }

    void P1HighlightButton(Selectable sel)
    {
        sel.image.color = Color.cyan;
    }

    void P2HighlightButton(Selectable sel)
    {
        sel.image.color = Color.red;
    }

    void UnHighlightButton(Selectable sel)
    {
        sel.image.color = Color.white;
    }

    public void TutorialPanelSwitch()
    {
        if(tutorialPanel.activeSelf)
        {
            tutorialPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(tutorialButtons[0].gameObject);
        }
        else
        {
            tutorialPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(tutorialButtons[1].gameObject);
        }
    }

    public void CreditsPanelSwitch()
    {
        if(creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(creditsButtons[0].gameObject);
        }
        else
        {
            creditsPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(creditsButtons[1].gameObject);
        }
    }

    public void CharSelectPanelSwitch()
    {
        if(charSelectPanel.activeSelf)
        {
            mainMenuPanel.SetActive(true);
            charSelectPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(charSelectButtons[0].gameObject);
            isPlayer2Present = false;
        }
        else
        {
            charSelectPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(charSelectButtons[1].gameObject);
            isPlayer2Present = true;
        }
    }
}
