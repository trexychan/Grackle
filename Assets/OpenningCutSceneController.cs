using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class OpenningCutSceneController : MonoBehaviour
{
	public Blink blink;
	public GameObject camera;
	private float timer = 0f;
	private float y;
	public Vector3 startPos;
	public Quaternion startRot;

	public AudioSource creaky_door;

	public GameObject closedDoor;
	public GameObject openDoor;

    void Start()
    {
		blink.setBlink(1);
		startPos = camera.transform.position;
		startRot = camera.transform.rotation;
    }

	void Update(){
		timer += Time.deltaTime;
		startRot = camera.transform.rotation;
		if (  timer > 1 && timer < 3 ){
			blink.setBlink(1 - (timer - 1)/3);
		}
		else if ( timer >= 3 && timer < 6 ){
			blink.setBlink((timer - 3)/4 );
			if (timer > 5.5) {
				creaky_door.Play();
			}
		}
		else if ( timer >= 6f && timer < 6.5 ){
			closedDoor.SetActive(false);
			openDoor.SetActive(true);
		}
		else if ( timer >= 6.5f && timer < 8.5 ){
			blink.setBlink(1 - ((timer - 6.5f)/1.5f) );
		}
		else if ( timer >= 8.5f && timer < 10.5 ){
			blink.setBlink((timer - 8.5f)/1.5f );
		}
		else if ( timer >= 11 && timer < 13){
			blink.setBlink(1 -(timer - 11)/1.5f );
		}
		else if ( timer >= 13 && timer < 15){
			float counter = timer - 13;
			camera.transform.position = new Vector3( startPos.x + 9*Mathf.Sin( Mathf.PI * counter / 180f), startPos.y + 6*Mathf.Sin( Mathf.PI * counter / 180f), startPos.z );
			camera.transform.Rotate( 0, 0, - ( Time.deltaTime * 25.29f / 2.0f ) );
			//camera.transform.Rotate( 0, 0, - (.34f * (counter/2)));

			y = camera.transform.position.y;
		}
		else if ( timer >= 15 && timer < 16){
			float counter = timer -15;
			camera.transform.position = new Vector3( camera.transform.position.x , y + counter/2, camera.transform.position.z );
		}
		else if ( timer >= 16 && timer < 17){
			float counter = timer -16;
			camera.transform.position = new Vector3( camera.transform.position.x, camera.transform.position.y, startPos.z  + counter );
		}
		else if (timer >= 17){
			SceneManager.LoadScene("Tutorial");
		}
	}
		
}
