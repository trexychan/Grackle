using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushGenerator : MonoBehaviour {

	public Dictionary<GameObject, int> bushes;

	public float length;

    void Start() {
		int totalWeight = 0;
		foreach (KeyValuePair<GameObject, int> kvp in bushes) {
			totalWeight += kvp.Value;
		}

		float z = -length / 2;
        while (true) {
			float size = Random.Range(0.5F, 0.7F) * Random.Range(0.5F, 0.7F);

			if (z + size / 2 > length / 2) break;

			GameObject bush = Instantiate(bushes[Random.Range(0, bushes.Length)], this.transform);

			bush.transform.localScale = size * Vector3.one;

			bush.transform.localPosition = new Vector3(0, Random.value - 0.5F, z + size / 2);

			bush.transform.rotation = Quaternion.Euler(0, Random.Range(0F, 360F), 0);

			z += 2 * size;
		}
    }

}
