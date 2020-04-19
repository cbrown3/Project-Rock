using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private TextMeshProUGUI[] playersUsername;
    private TextMeshProUGUI[] playersCharName;

    private TextMeshProUGUI roundInfoText;

    [SerializeField]
    private TextMeshProUGUI timerText;
    private float timer = 99;
    [SerializeField]
    private Toggle[] roundToggle;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI[] comboCounterText;

    [SerializeField]
    private HealthManager[] healthManagers;
    [SerializeField]
    private ShieldManager[] shieldManagers;

    //[System.NonSerialized]
    public int[] comboCounter;
    
    public int[] charSelected = { -1, -1 };
    public bool[] playersReady = {false,false};

    public enum GameState
    {
        Default,
        MainMenu,
        RoundStart,
        Playing,
        RoundEnd,
        GameOver
    }

    public GameState currentGameState;

    public enum GameMode
    {
        None = 0,
        VS = 1,
        Practice = 2
    }
    
    public GameMode currentGameMode = GameMode.None;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playersCharName = new TextMeshProUGUI[2];
        playersUsername = new TextMeshProUGUI[2];

        //roundStartText = new TextMeshProUGUI();

        roundToggle = new Toggle[4];
        comboCounterText = new TextMeshProUGUI[2];

        healthManagers = new HealthManager[2];
        shieldManagers = new ShieldManager[2];

        comboCounter = new int[2] { 0, 0 };

        currentGameState = GameState.Default;

        SceneManager.sceneLoaded += StartGameEvent;
    }
    
    public GameMode GetGameMode()
    {
        return currentGameMode;
    }

    public void SetGameMode(int mode)
    {
        switch(mode)
        {
            case 0:
                currentGameMode = GameMode.None;
                break;
            case 1:
                currentGameMode = GameMode.VS;
                break;
            case 2:
                currentGameMode = GameMode.Practice;
                break;
            default:
                currentGameMode = GameMode.None;
                break;
        }
    }

    public IEnumerator LoadObjects()
    {
        int objectsLoaded = 0;

        Scene gameScene = SceneManager.GetSceneByName("GameScene");

        GameObject[] gameObjects = gameScene.GetRootGameObjects();

        while(objectsLoaded < 19)
        {
            //objectsLoaded = 0;

            foreach (GameObject go in gameObjects)
            {
                if (go.name == "Canvas")
                {
                    Transform[] canvasGOs = go.GetComponentsInChildren<Transform>();
                    foreach (Transform transform in canvasGOs)
                    {
                        if (transform.name == "MatchTimer")
                        {
                            timerText = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "P1CharName")
                        {
                            playersCharName[0] = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "P2CharName")
                        {
                            playersCharName[1] = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "P1Username")
                        {
                            playersUsername[0] = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "P2Username")
                        {
                            playersUsername[1] = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "RoundStartText")
                        {
                            roundInfoText = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "P1WinCounter")
                        {
                            roundToggle[0] = transform.GetComponentsInChildren<Toggle>()[0];
                            objectsLoaded++;
                            roundToggle[1] = transform.GetComponentsInChildren<Toggle>()[1];
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "P2WinCounter")
                        {
                            roundToggle[2] = transform.GetComponentsInChildren<Toggle>()[0];
                            objectsLoaded++;
                            roundToggle[3] = transform.GetComponentsInChildren<Toggle>()[1];
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "GameOverPanel")
                        {
                            gameOverPanel = transform.gameObject;
                            objectsLoaded++;
                            gameOverPanel.SetActive(false);
                            continue;
                        }

                        if (transform.name == "P2ComboCounter")
                        {
                            comboCounterText[0] = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }

                        if (transform.name == "P1ComboCounter")
                        {
                            comboCounterText[1] = transform.GetComponent<TextMeshProUGUI>();
                            objectsLoaded++;
                            continue;
                        }
                    }
                }

                if (go.name == "Player1")
                {
                    healthManagers[0] = go.GetComponent<HealthManager>();
                    objectsLoaded++;
                    shieldManagers[0] = go.GetComponentInChildren<ShieldManager>();
                    objectsLoaded++;
                    continue;
                }

                if (go.name == "Player2")
                {
                    healthManagers[1] = go.GetComponent<HealthManager>();
                    objectsLoaded++;
                    shieldManagers[1] = go.GetComponentInChildren<ShieldManager>();
                    objectsLoaded++;
                    continue;
                }
            }
            yield return null;
        }

        if (currentGameMode == GameMode.VS)
        {
            playersUsername[0].text = "";
            playersUsername[1].text = "";
        }

        playersCharName[0].text = "Char " + charSelected[0].ToString();
        playersCharName[1].text = "Char " + charSelected[1].ToString();

        StartCoroutine(StartRound());
    }

    private void StartGameEvent(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "GameScene")
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        StartCoroutine(LoadObjects());
    }

    IEnumerator StartRound()
    {
        currentGameState = GameState.RoundStart;
        float countDown = 3;
        roundInfoText.GetComponent<Animator>().Play("FadeOut");
        roundInfoText.text = "Ready...";
        
        while(countDown > 0)
        {
            if(currentGameState == GameState.RoundStart)
            {
                countDown -= Time.deltaTime;
            }
            yield return null;
        }

        roundInfoText.text = "Shoot!";
        currentGameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.Playing)
        {
            Timer();
            ComboCounter();

            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(GameWon(2));
            }
        }
    }

    private void Timer()
    {
        timer -= Time.deltaTime;

        timerText.text = timer.ToString("00");

        if (timer <= 0)
        {
            timerText.text = "00";

            currentGameState = GameState.RoundEnd;
        }
    }

    private void ComboCounter()
    {
        if(comboCounter[0] > 1)
        {
            comboCounterText[0].enabled = true;
            comboCounterText[0].text = comboCounter[0].ToString() + " Hits!";
        }
        else
        {
            comboCounterText[0].enabled = false;
        }

        if(comboCounter[1] > 1)
        {
            comboCounterText[1].enabled = true;
            comboCounterText[1].text = comboCounter[1].ToString() + " Hits!";
        }
        else
        {
            comboCounterText[1].enabled = false;
        }
    }

    public void RoundWon(int player)
    {
        if (player == 1)
        {
            if (!roundToggle[0].isOn)
            {
                roundToggle[0].isOn = true;
                currentGameState = GameState.RoundEnd;
                roundInfoText.text = "Cease Fire!";
                StartCoroutine(ResetRound());
            }
            else
            {
                roundToggle[1].isOn = true;
                StartCoroutine(GameWon(1));
            }
        }
        else if (player == 2)
        {
            if (!roundToggle[2].isOn)
            {
                roundToggle[2].isOn = true;
                StartCoroutine(ResetRound());
            }
            else
            {
                roundToggle[3].isOn = true;
                StartCoroutine(GameWon(2));
            }
        }
    }

    IEnumerator ResetRound()
    {
        float countDown = 5;
        currentGameState = GameState.RoundEnd;
        roundInfoText.text = "Cease Fire!";
        roundInfoText.GetComponent<Animator>().Play("Show");

        while (countDown > 3)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }

        shieldManagers[0].ResetShield();
        shieldManagers[1].ResetShield();
        healthManagers[0].ResetHealth();
        healthManagers[1].ResetHealth();
        timer = 99;
        timerText.text = timer.ToString();
        currentGameState = GameState.RoundStart;
        roundInfoText.GetComponent<Animator>().Play("FadeOut");
        roundInfoText.text = "Ready...";

        while(countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }

        roundInfoText.text = "Shoot!";
        currentGameState = GameState.Playing;
    }

    private IEnumerator GameWon(int player)
    {
        float cooldown = 2f;
        currentGameState = GameState.GameOver;
        if (player == 1)
        {
            roundInfoText.text = "Duel Winner: Player 1!";
        }
        else if (player == 2)
        {
            roundInfoText.text = "Duel Winner: Player 2!";
        }
        roundInfoText.GetComponent<Animator>().Play("Show");

        while(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        gameOverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        Debug.Log("resetting...");

        SceneManager.sceneLoaded -= StartGameEvent;
        SceneManager.UnloadSceneAsync("GameScene");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        SceneManager.UnloadSceneAsync("MainMenuScene");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }

    public void CloseGame()
    {
        Debug.Log("closing...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
