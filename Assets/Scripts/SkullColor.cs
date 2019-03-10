using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SkullColor : MonoBehaviour
{
    public Light pointL;
    public Light spotL;
    public Material skullMat;

    public static Color MainColor { get; set; }

    [SerializeField]
    private Color mainColor;

    private Material privMat;
    private float originalHeight;

    // Start is called before the first frame update
    void Start()
    {
        privMat = new Material(skullMat);
        GetComponent<MeshRenderer>().material = privMat;
        privMat.SetColor("_Color", mainColor);
        privMat.SetColor("_EmissionColor", mainColor / 4f);
    }

    // Update skull color in editor whenever changed in inspector
    void OnValidate()
    {
        if (privMat == null)
        {
            privMat = new Material(skullMat);
            GetComponent<MeshRenderer>().material = privMat;
        }

        MainColor =  mainColor;
        pointL.color = MainColor;
        spotL.color = MainColor;
        privMat.SetColor("_Color", MainColor);
        privMat.SetColor("_EmissionColor", MainColor / 4f);
    }  
}
