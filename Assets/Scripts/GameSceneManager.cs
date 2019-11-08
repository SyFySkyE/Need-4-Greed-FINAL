using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject pauseCanvas;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(0); // TODO spice up?
#endif
    }

    private void Victory()
    {
        winCanvas.SetActive(true);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !winCanvas.activeInHierarchy)
            {
                TogglePauseScreen();
            }
        }
    }

    private void TogglePauseScreen()
    {
        if (!pauseCanvas.activeInHierarchy)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // 0 is Splash Screen
    }
}
