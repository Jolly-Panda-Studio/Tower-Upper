using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> splashScreens; 
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private float fadeOutDuration = 1.5f;
    [SerializeField] private string sceneToLoad = "MainScene";

    private void Start()
    {
        foreach (CanvasGroup canvas in splashScreens)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
                canvas.alpha = 0f;
            }
        }

        StartCoroutine(PlaySplashSequence());
    }

    private IEnumerator PlaySplashSequence()
    {
        foreach (CanvasGroup canvasGroup in splashScreens)
        {
            if (canvasGroup == null)
                continue;

            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(true);

            // Fade In
            float timer = 0f;
            while (timer < fadeInDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);
                timer += Time.unscaledDeltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1f;

            // Wait
            yield return new WaitForSecondsRealtime(holdDuration);

            // Fade Out
            timer = 0f;
            while (timer < fadeOutDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);
                timer += Time.unscaledDeltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(false);
        }

        // Load next scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
