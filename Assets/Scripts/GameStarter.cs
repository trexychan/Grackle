using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
	public GameObject main_scene;
	public GameObject settings_scene;
	public GameObject credits;
	public GameObject difficulty;

	void Update(){
		if (Cursor.visible == false ||  Cursor.lockState != CursorLockMode.None){
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void StartEasyGame(){
		Global.game_mode = 0;
		StartGame();
	}

	public void StartMediumGame(){
		Global.game_mode = 1;
		StartGame();
	}

	public void StartHardGame(){
		Global.game_mode = 2;
		StartGame();
	}

	public void StartGame(){
		SceneManager.LoadScene( "Opening Cut Scene" );
	}

	public void Settings(){
		main_scene.SetActive(false);
		credits.SetActive(false);
		settings_scene.SetActive(true);
		difficulty.SetActive(false);
	}

	public void DifficultySelect(){
		main_scene.SetActive(false);
		credits.SetActive(false);
		settings_scene.SetActive(false);
		difficulty.SetActive(true);
	}

	public void Main(){
		main_scene.SetActive(true);
		credits.SetActive(false);
		settings_scene.SetActive(false);
		difficulty.SetActive(false);
	}

	public void Credits(){
		main_scene.SetActive(false);
		credits.SetActive(true);
		settings_scene.SetActive(false);
		difficulty.SetActive(false);
	}

	public void Volume(){
		
	}

	public void ExitToDesktop(){
		Application.Quit();
	}


}
