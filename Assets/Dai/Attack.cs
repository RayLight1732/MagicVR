using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "Main Camera")
        {
            player = GameObject.Find("Player");
            //Debug.Log("succes");
            HP hP = player.GetComponent<HP>();
            if(hP){
                hP.removeHP(5);
            }
        }
    }
}
