using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static void EnableCG(CanvasGroup cG)
    {
        cG.alpha = 1f;
        cG.interactable = true;
        cG.blocksRaycasts = true;
    }

    public static void DisableCG(CanvasGroup cG)
    {
        cG.alpha = 0f;
        cG.interactable = false;
        cG.blocksRaycasts = false;
    }
}
