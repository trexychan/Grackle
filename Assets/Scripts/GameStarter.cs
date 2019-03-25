using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
	public GameObject main_scene;
	public GameObject settings_scene;

	void Update(){
		if (Cursor.visible == false ||  Cursor.lockState != CursorLockMode.None){
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene( "Dungeon" );
	}

	public void ShowSettings()
	{
		main_scene.SetActive(false);
		settings_scene.SetActive(true);
	}

	public void HideSettings()
	{
		main_scene.SetActive(true);
		settings_scene.SetActive(false);
	}


	public void ExitToDesktop()
	{
		Application.Quit();
	}
}
