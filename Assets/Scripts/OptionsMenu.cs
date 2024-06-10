using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{

    /* Private Variables */
    [SerializeField]
    private CanvasGroup slidersCG;
    private CanvasGroup optionsMenuCG;
    private bool optionsMenuOpen = false;


    /* Public Variables */
    public Slider moveSpeedSlider;

    public TextMeshProUGUI moveSpeedValueText;


    // Start is called before the first frame update
    void Start()
    {
        optionsMenuCG = GetComponent<CanvasGroup>();
        EventManager.showOptionsMenuButton.AddListener(ShowOptionsButton);
        EventManager.gameWon.AddListener(HideOptionsButton);

        moveSpeedSlider.onValueChanged.AddListener((val) => {
            moveSpeedValueText.text = val.ToString();
            EventManager.onMoveSpeedChanged.Invoke(val);
        });

        EventManager.initialMoveSpeedVal.AddListener((val) => {
            moveSpeedSlider.value = val;
            moveSpeedValueText.text = 50f.ToString();
        });
    }

    private void ShowOptionsButton()
    {
        Utilities.EnableCG(optionsMenuCG);
    }

    private void HideOptionsButton()
    {
        Utilities.DisableCG(optionsMenuCG);
    }

    public void ToggleSliderMenu()
    {
        if (optionsMenuOpen)
        {
            // Close options
            slidersCG.alpha = 0f;
            slidersCG.blocksRaycasts = false;
            slidersCG.interactable = false;
            optionsMenuOpen = false;

            // Unpause the game
            if (GameManager.state == GameManager.GameState.PAUSED)
            {
                Time.timeScale = 1f;
                GameManager.state = GameManager.GameState.PLAYING;
            }
            
        }
        else
        {
            // Open options
            slidersCG.alpha = 1f;
            slidersCG.blocksRaycasts = true;
            slidersCG.interactable = true;
            optionsMenuOpen = true;

            // Pause the game
            if (GameManager.state == GameManager.GameState.PLAYING)
            {
                Time.timeScale = 0f;
                GameManager.state = GameManager.GameState.PAUSED;
            }
        }
    }
}
