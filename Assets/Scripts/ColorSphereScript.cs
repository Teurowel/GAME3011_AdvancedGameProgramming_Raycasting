using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSphereScript : MonoBehaviour
{
    [SerializeField] Color col;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.SetColor("_Color", col);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
