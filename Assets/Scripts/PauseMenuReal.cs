using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuReal : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
	public GameObject crosshair;
	public System.String level = "Dungeon";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused) {
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
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
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
		Resume();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
        Debug.Log("Quitting to Menu.");
        SceneManager.LoadScene("MainMenu");
    }

	public void Restart(){
		Resume();
		SkullPickup.ResetSkullCount();
		Debug.Log("Restarting");
		Global.active_gun = 0;
		SceneManager.LoadScene( level );
	}

	public void ToggleCrossHair(){
		Debug.Log(crosshair.activeSelf);
		if (crosshair.activeSelf){
			crosshair.SetActive(false);
		} else {
			crosshair.SetActive(true);
		};
	}
}
