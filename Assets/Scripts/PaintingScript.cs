using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingScript : MonoBehaviour
{
    [SerializeField] List<Renderer> quads = new List<Renderer>(); // Unnecessary but doesn't hurt to have a list
 
    void Start()
    {
        foreach (Transform child in transform) // Could just do this in Update, but as above, doesn't hurt
        {
            quads.Add(child.GetComponent<Renderer>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("c")) // Clear the canvas
        {
            foreach (var q in quads)
            {
                q.material.color = Color.white;
            }
        }
    }
}
