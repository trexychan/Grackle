using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFilter : MonoBehaviour
{
    public Transform target;
    public int w = 3;
    private int cur = 0;
    private bool full = false;
    private Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[w];
        positions[0] = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((target.position - positions[cur]).sqrMagnitude > 1) {
            cur++;
            positions[cur] = target.position;
            if (cur == w - 1) {
                Vector3 V = new Vector3(0,0,0);
                for (int i = 1; i < w; i++) {
                    V += positions[i] - positions[i-1];
                }
                V /= w;
                V.y = 0;
                transform.position = positions[cur];
                transform.rotation = Quaternion.LookRotation(V, Vector3.up);
                positions[0] = target.position;
                cur = 0;
            }
        }
    }
}
