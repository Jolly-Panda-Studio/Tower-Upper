using MJUtilities.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    
    
    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void RestartGame()
    {
        UIManager.instance.CloseEverything();
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
    
}