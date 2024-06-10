using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Private Variables */
    [SerializeField]
    private CanvasGroup startMenuCG;
    private float timeRemaining;
    private bool timerRunning = false;
    private float levelScore;
    private float timeSpent = 0f;

    /* Public Variables */
    private static GameManager _instance;
    public enum GameState
    {
        PAUSED,
        PLAYING,
        WIN,
        LOSE
    }

    public static GameState state;
    public CanvasGroup gameUICG;
    public TextMeshProUGUI timerValueText;
    public float levelMaxTime = 60f;
    

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.PAUSED;
        Time.timeScale = 0f;
        Utilities.EnableCG(startMenuCG);
        timeRemaining = levelMaxTime;
        levelScore = 0f;
        timeSpent = 0f;
        EventManager.gameWon.AddListener(HideGameUI);
        EventManager.obstacleHit.AddListener(ObstacleHit);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0f;
                timerRunning = false;
                EventManager.gameLost.Invoke();
            }
        }
        timeSpent += Time.deltaTime;
    }

    private void ObstacleHit()
    {
        timeRemaining -= 3f;
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerValueText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void HideGameUI()
    {
        Utilities.DisableCG(gameUICG);
    }

    public float GetScore()
    {
        levelScore = Mathf.Floor(timeRemaining) * 10;
        timeSpent = Mathf.Floor(timeSpent);
        float finalScore = Mathf.Max(0, levelMaxTime - timeSpent) * levelScore;
        return finalScore;
    }

    public void StartGame()
    {
        if (state == GameState.PAUSED)
        {
            state = GameState.PLAYING;
            Time.timeScale = 1f;
            Utilities.DisableCG(startMenuCG);
            EventManager.showOptionsMenuButton.Invoke();
            Utilities.EnableCG(gameUICG);
            timerRunning = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
