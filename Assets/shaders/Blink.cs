using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
	public Material m;
	//[SerializeField] public float blink_val {
	//	set{ m.SetFloat("_Disolve", value); }
	//	get {return m.GetFloat("_Disolve"); }
	//}

	public void setBlink (float value) {
		m.SetFloat("_Disolve", value);
	}
	public float getBlink (){
		return m.GetFloat("_Disolve");
	}
}
