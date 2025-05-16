using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private float fadeOutDuration = 1.5f;
    [SerializeField] private string sceneToLoad = "MainScene";

    private void Start()
    {
        StartCoroutine(PlaySplashSequence());
    }

    private IEnumerator PlaySplashSequence()
    {
        canvasGroup.alpha = 0f;

        // Fade In
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Wait at full opacity
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

        // Load main scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
