using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushGenerator : MonoBehaviour {

	[System.Serializable]
	public struct Bush {
		public GameObject bush;
		public int weight;
	}

	public Bush[] bushes;

	public float length;

    void Start() {
		int totalWeight = 0;
		foreach (Bush bush in bushes) {
			totalWeight += bush.weight;
		}

		float z = -length / 2;
        while (true) {
			float size = Random.Range(0.5F, 0.7F) * Random.Range(0.5F, 0.7F);

			if (z + size / 2 > length / 2) break;

			int bushIndex = Random.Range(0, totalWeight);

			GameObject bush = null;
			int total = 0;
			foreach (Bush b in bushes) {
				if ((total += b.weight) > bushIndex) {
					bush = Instantiate(b.bush, this.transform);
					break;
				}
			}

			bush.transform.localScale = size * Vector3.one;

			bush.transform.localPosition = new Vector3(0, Random.value - 0.5F, z + size / 2);

			bush.transform.rotation = Quaternion.Euler(0, Random.Range(0F, 360F), 0);

			z += 2 * size;
		}
    }

}
