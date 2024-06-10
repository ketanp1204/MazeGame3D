using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    /* Private Variables */
    private Animator animator;
    private Vector3 timePenaltyPopupOffset = new Vector3(0, 1f, 0f);
    private RectTransform penaltyText;
    private CanvasGroup penaltyPopupCG;

    /* Public Variables */
    public GameObject timePenaltyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Flash");
        EventManager.obstacleHit.Invoke();
        GameObject popup = Instantiate(timePenaltyPrefab, transform.position + timePenaltyPopupOffset, Quaternion.identity);
        penaltyText = popup.GetComponentInChildren<RectTransform>();
        AnimatePopup(popup);
    }

    private void AnimatePopup(GameObject popup)
    {
        penaltyPopupCG = popup.GetComponent<CanvasGroup>();
        StartCoroutine(FadeCG(penaltyPopupCG, 0f, 1f, 0.6f));
        StartCoroutine(MoveAndDestroyPopup(0.6f));
    }

    private IEnumerator MoveAndDestroyPopup(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            float newY = Mathf.Lerp(0f, 40f, t / duration);
            penaltyText.anchoredPosition.Set(0f, newY);

            t += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeCG(penaltyPopupCG, 1f, 0f, 0.3f));
        yield return new WaitForSeconds(0.3f);
        Destroy(penaltyPopupCG.gameObject);
    }

    private IEnumerator FadeCG(CanvasGroup cG, float startAlpha, float endAlpha, float duration, float startDelay = 0f, bool enableInteraction = false)
    {
        if (startDelay > 0f)
        {
            yield return new WaitForSeconds(startDelay);
        }

        float t = 0f;
        while (t < duration)
        {
            cG.alpha = Mathf.Lerp(startAlpha, endAlpha, t / duration);

            t += Time.deltaTime;
            yield return null;
        }

        cG.alpha = endAlpha;

        if (enableInteraction)
        {
            cG.interactable = true;
            cG.blocksRaycasts = true;
        }
        else
        {
            cG.interactable = false;
            cG.blocksRaycasts = false;
        }
    }
}
