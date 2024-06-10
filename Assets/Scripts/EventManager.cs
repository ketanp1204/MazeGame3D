using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<float> initialMoveSpeedVal;
    public static UnityEvent<float> onMoveSpeedChanged;
    public static UnityEvent showOptionsMenuButton;
    public static UnityEvent gameWon;
    public static UnityEvent gameLost;
    public static UnityEvent obstacleHit;

    private void OnEnable()
    {
        initialMoveSpeedVal = new UnityEvent<float>();
        onMoveSpeedChanged = new UnityEvent<float>();
        showOptionsMenuButton = new UnityEvent();
        gameWon = new UnityEvent();
        gameLost = new UnityEvent();
        obstacleHit = new UnityEvent();
    }
}
