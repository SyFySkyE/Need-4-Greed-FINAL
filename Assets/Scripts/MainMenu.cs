using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Canvases")]
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject creditsCanvas;

    [Header("Credit Objects")]
    [SerializeField] GameObject credits1;
    [SerializeField] GameObject credits2;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void ViewCredits()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        credits1.SetActive(true);
        credits2.SetActive(false);
    }

    public void NextCredits()
    {
        credits1.SetActive(false);
        credits2.SetActive(true);
    }

    public void PreviousCredits()
    {
        credits1.SetActive(true);
        credits2.SetActive(false);
    }

    public void BackToMenu()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(0); // TODO spice up?
#endif
    }

    public void StartGame()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
