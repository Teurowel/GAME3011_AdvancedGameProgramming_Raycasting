using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifleScript : MonoBehaviour
{
    [SerializeField] LineRenderer lr;
    [SerializeField] TextMesh modeTxt;
    [SerializeField] TextMesh radiusTxt;
    bool mode = false; // Just a 2-mode toggle. 0 = raycast, 1 = spherecast
    string[] modeStrings = {"Mode: Raycast", "Mode: Spherecast"};
    float sphereSize = 3.0f;

    void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0.0f)
        {
            sphereSize += Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f;
            sphereSize = Mathf.Clamp(sphereSize, 1.0f, 10.0f);
            radiusTxt.text = "R:" + ((int)sphereSize).ToString();
        }
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Click");
            int layerMask = 1 << 6; // Here's the use of layerMask
            RaycastHit hit;
            if (mode == false)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    hit.transform.GetComponent<Renderer>().material.color = lr.startColor;
                }
            }
            else
            {
                Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
                RaycastHit[] hits = Physics.SphereCastAll(ray, sphereSize, Mathf.Infinity, layerMask);
                foreach (var h in hits)
                {
                    h.transform.GetComponent<Renderer>().material.color = lr.startColor;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "ColorSphere") // Just taggin' it this time instead of mask
                {
                    lr.startColor = hit.transform.GetComponent<Renderer>().material.color;
                    lr.endColor = hit.transform.GetComponent<Renderer>().material.color;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            mode = !mode;
            modeTxt.text = modeStrings[(mode?1:0)];
            radiusTxt.text = "R:" + sphereSize.ToString();
            radiusTxt.gameObject.SetActive(mode);
        }
    }
}
