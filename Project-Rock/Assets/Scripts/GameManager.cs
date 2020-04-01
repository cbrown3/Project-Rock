using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game UI")]
    public TextMeshProUGUI timerText;
    private float timer = 99;
    public Toggle[] roundToggle;
    public GameObject gameOverPanel;
    public TextMeshProUGUI[] comboCounterText;

    [Header("Player Info")]
    public HealthManager[] healthManagers;
    public ShieldManager[] shieldManagers;
    public Tile[] startingTiles;
    //[System.NonSerialized]
    public int[] comboCounter;

    public enum GameState
    {
        Default,
        RoundStart,
        Playing,
        RoundEnd,
        GameOver
    }

    public GameState currentGameState;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += StartGame;
    }

    private void Start()
    {
        comboCounter = new int[2] { 0, 0 };
    }

    private void StartGame(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "GameScene")
        {
            StartCoroutine(StartRound());
        }
    }

    IEnumerator StartRound()
    {
        currentGameState = GameState.RoundStart;
        float countDown = 3;
        print("Ready...");
        
        while(countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }

        print("Fire!");
        currentGameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.Playing)
        {
            Timer();
            ComboCounter();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            GameWon(2);
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
        if(player == 1)
        {
            if(!roundToggle[0].isOn)
            {
                roundToggle[0].isOn = true;
                currentGameState = GameState.RoundEnd;
                ResetRound();
                StartCoroutine(StartRound());
            }
            else
            {
                roundToggle[1].isOn = true;
                GameWon(1);
            }
        }
        else if(player == 2)
        {
            if (!roundToggle[2].isOn)
            {
                roundToggle[2].isOn = true;
                currentGameState = GameState.RoundEnd;
                ResetRound();
                StartCoroutine(StartRound());
            }
            else
            {
                roundToggle[3].isOn = true;
                GameWon(2);
            }
        }
    }

    private void ResetRound()
    {
        for(int i = 0; i < 2; i++)
        {
            shieldManagers[i].ResetShield();
            healthManagers[i].ResetHealth();
            timer = 99;
        }
    }

    private void GameWon(int player)
    {
        currentGameState = GameState.GameOver;
        if (player == 1)
        {
            print("Congrats Player 1, You Win!");
        }
        else if (player == 2)
        {
            print("Congrats Player 2, You Win!");
        }

        gameOverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        print("resetting...");
        SceneManager.sceneLoaded -= StartGame;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void CloseGame()
    {
        print("closing...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
