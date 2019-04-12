using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endCreditScript : MonoBehaviour
{
	public float timer = 0f;
	public GameObject pic1;
	public GameObject pic2;
	public GameObject credits;
	public GameObject thank;

    // Start is called before the first frame update
    void Start()
    {
		pic1.SetActive(true);
		pic2.SetActive(false);
		credits.SetActive(false);
		thank.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if (timer > 4 && timer < 6) {
			pic1.SetActive(false);
			pic2.SetActive(true);
		}
		else if (timer > 6 && timer < 9) {
			pic2.SetActive(true);
			thank.SetActive(true);
		}
		else if (timer > 9){
			thank.SetActive(false);
			credits.SetActive(true);
		}
    }
}
