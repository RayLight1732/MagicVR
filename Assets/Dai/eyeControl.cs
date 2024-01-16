using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeControl : MonoBehaviour
{

    GameObject eye;
    Enemy2 eyeControlScript;

    // Start is called before the first frame update
    void Start()
    {
        eye = transform.parent.gameObject;
        eyeControlScript = eye.GetComponent<Enemy2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        eyeControlScript.FindEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        eyeControlScript.FindExit(other);
    }
}
