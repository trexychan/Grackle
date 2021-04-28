using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endCreditScript : MonoBehaviour
{
	public float timer = 0f;
	public GameObject pic1;
	public GameObject pic2;
	public GameObject credits;
	public GameObject thank;
	public GameObject return_home;

    // Start is called before the first frame update
    void Start()
    {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		pic1.SetActive(true);
		pic2.SetActive(false);
		credits.SetActive(false);
		thank.SetActive(false);
		return_home.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if (timer > 4 && timer < 4.5) {
			pic1.SetActive(false);
			pic2.SetActive(true);
		}
		else if (timer > 6 && timer < 6.5) {
			pic2.SetActive(true);
			thank.SetActive(true);
		}
		else if (timer > 9 && timer < 9.5){
			thank.SetActive(false);
			credits.SetActive(true);
			return_home.SetActive(true);
		}
    }

	public void return_to_menu()
	{
		Debug.Log("Quitting to Menu.");
		SceneManager.LoadScene("MainMenu");
	}
}
