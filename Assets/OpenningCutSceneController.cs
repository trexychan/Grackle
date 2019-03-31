using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenningCutSceneController : MonoBehaviour
{
	public Blink blink;
	public GameObject camera;
	private float timer = 0f;
	public Vector3 startPos;
	public Quaternion startRot;

    void Start()
    {
		blink.setBlink(1);
		startPos = camera.transform.position;
		startRot = camera.transform.rotation;
    }

	void Update(){
		timer += Time.deltaTime;
		startRot = camera.transform.rotation;
		/*
		if (  timer > 1 && timer < 3 ){
			blink.setBlink(1 - (timer - 1)/3);
		}
		if ( timer >= 3 && timer < 6 ){
			blink.setBlink((timer - 3)/4 );
		}
		if ( timer >= 6.5f && timer < 8.5 ){
			blink.setBlink(1 - ((timer - 6.5f)/1.5f) );
		}
		if ( timer >= 8.5f && timer < 10.5 ){
			blink.setBlink((timer - 8.5f)/1.5f );
		}
		if ( timer >= 11 && timer < 13){
			blink.setBlink(1 -(timer - 11)/1.5f );
		}
		*/
		if ( timer >= 0 && timer < 3){
			blink.setBlink(-1);
			float counter = timer;
			camera.transform.position = new Vector3( startPos.x + 7*Mathf.Sin( Mathf.PI * 2 * counter / 360f), startPos.y + 7*Mathf.Sin( Mathf.PI * counter / 180f), startPos.z );
			//camera.transform.rotation.Set( startRot[0], startRot[1], startRot[2] - (.22f * (counter/3)), startRot[3]  );
		}
	}
		
}
