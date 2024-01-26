using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1;
    [SerializeField]
    private GameObject enemy2;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject field1;
    [SerializeField]
    private GameObject field2;
    [SerializeField]
    private GameObject parent;
    private bool spawnflag;
    private Vector3 vector1;
    private Vector3 vector2;
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
        if(!spawnflag && other.gameObject.name == "Player"){
            Debug.Log("succes");
            vector1 = field1.transform.position;
            vector1.y += 1;
            vector2 = field2.transform.position;
            vector2.y += 1;
            if(parent.name == "Stage1"){
                Instantiate(enemy1,vector1,Quaternion.identity);
                spawnflag = true;
            }
            else if(parent.name == "Stage2"){
                Instantiate(enemy2,vector1,Quaternion.identity);
                Instantiate(enemy2,vector2,Quaternion.identity);
                spawnflag = true;
            }
            else if(parent.name == "Stage3"){
                Instantiate(enemy1,vector1,Quaternion.identity);
                Instantiate(enemy2,vector2,Quaternion.identity);
                spawnflag = true;
            }
            else if(parent.name == "Boss"){
                Instantiate(boss,vector1,Quaternion.identity);
                spawnflag = true;
            }
        }
    }
}
