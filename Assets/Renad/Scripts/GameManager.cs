//ابيه ينتقل بين السينز و يتعامل مع البلير في فوق 


using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
   public static GameManager Instance;

    [Header("Game State")]
    public bool isPaused;
    public bool isGameOver;


    private void Awake()
    {
                if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        isPaused = false;
        isGameOver = false;
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0f;

        SceneManager.LoadSceneAsync(2);
    }

    public void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;

        SceneManager.LoadSceneAsync(1);
    }

    public void LoseGame()
    {
        isGameOver = true;

        Time.timeScale = 1f;

        SceneManager.LoadSceneAsync(3);
    }


    public void WinGame()
    {
        isGameOver = true;

        Time.timeScale = 1f;

        SceneManager.LoadSceneAsync(4);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        isPaused = false;
        isGameOver = false;

        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("exiting...");
    }
}