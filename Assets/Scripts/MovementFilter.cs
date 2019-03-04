using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementFilter : MonoBehaviour
{
    public Transform target;
    public Transform[] backArr;
    private Transform back;
    private int backIndex = 0;
    public int w = 3;
    private int cur = 0;
    private bool full = false;
    private Vector3[] positions;

    private RaycastHit left;
    private RaycastHit right;
    private int layerMask = ~(1 << 8);
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

                back = backArr[backIndex];
                backIndex++;
                if (backIndex >= backArr.Length) {
                    backIndex = 0;
                }
                back.position = transform.position - transform.forward;
                back.rotation = transform.rotation;
                bool hit = Physics.Raycast(back.position, transform.TransformDirection(-Vector3.right), out left, Mathf.Infinity, layerMask);
                hit = hit && Physics.Raycast(back.position, transform.TransformDirection(Vector3.right), out right, Mathf.Infinity, layerMask);
                if (hit) {
                    back.position += (right.distance - left.distance)/2 * back.transform.right;
                    back.GetComponent<NavMeshObstacle>().size = new Vector3(Mathf.Min((right.distance + left.distance) / 1.9f, (right.distance + left.distance) - 3), .5f, .1f);
                }

                positions[0] = target.position;
                cur = 0;
            }
            
        }
        
    }
}
