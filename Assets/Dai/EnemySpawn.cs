using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    public GameObject enemy;
    public GameObject quad;
    private bool spawnflag;
    private Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other){
        Debug.Log(other.gameObject.name);
        if(!spawnflag && other.gameObject.name == "Main Camera"){
            Debug.Log("succes");
            vector = quad.transform.position;
            vector.y += 1;
            transform.LookAt(other.transform);
            Instantiate(enemy,vector,Quaternion.identity);
            spawnflag = true;
        }
    }
}
