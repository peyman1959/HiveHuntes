using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRotater : MonoBehaviour
{
    public Transform target;

    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (FindObjectOfType<Camera>())
            {
                target = FindObjectOfType<Camera>().transform;
            }
        }
        else
        {
            tr.LookAt(tr.position + target.rotation * Vector3.forward, target.rotation * Vector3.up);
            
        }
    }
}
