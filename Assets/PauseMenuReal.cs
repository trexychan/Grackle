using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuReal : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    public string MainMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused) {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            } else {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Pause();
            }
        }
    }
    public void Resume() {
        pauseMenuUI.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1f;
    }
    void Pause() {
        pauseMenuUI.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void QuitToDesktop() {
        Debug.Log("Quitting to Desktop..");
        Application.Quit();
    }
    public void QuitToMenu() {
        Debug.Log("Quitting to Menu.");
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }
}
