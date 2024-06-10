using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    /* Public Variables */
    public TextMeshProUGUI gameResultText;
    public CanvasGroup gameResultCG;
    public TextMeshProUGUI scoreValueText;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.gameLost.AddListener(GameLost);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RollSphere")
        {
            gameResultText.text = "YOU WIN!";
            scoreValueText.text = GameManager.Instance.GetScore().ToString();
            Utilities.EnableCG(gameResultCG);
            Time.timeScale = 0f;
            GameManager.state = GameManager.GameState.WIN;
            EventManager.gameWon.Invoke();
        }
    }

    private void GameLost()
    {
        gameResultText.text = "YOU LOSE!";
        scoreValueText.text = GameManager.Instance.GetScore().ToString();
        Utilities.EnableCG(gameResultCG);
        Time.timeScale = 0f;
        GameManager.state = GameManager.GameState.LOSE;
    }
}
