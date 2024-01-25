using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject quad;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy1;
    [SerializeField]
    private GameObject enemy2;
    [SerializeField]
    private GameObject gate;
    private Vector3 playerpos;
    public float x;
    public float y;
    public float z;
    private bool spawnflag;
    private GameObject[] enemyBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        MeshRenderer quadRenderer = quad.GetComponent<MeshRenderer>();
        Bounds quadBounds = quadRenderer.bounds;
        playerpos = player.transform.position;
        Vector3 enemypos = new Vector3(x,y,z);
        Vector3 v = new Vector3(5,0,0);
        if(!spawnflag){
            if(quadBounds.min.x <= playerpos.x && playerpos.x <= quadBounds.max.x 
            && quadBounds.min.z <= playerpos.z && playerpos.z <= quadBounds.max.z){
                Instantiate(enemy1,enemypos,Quaternion.identity);
                if(enemy2 != null){
                    Instantiate(enemy2,enemypos + v,Quaternion.identity);
                }
                spawnflag = true;
                bool allnull = enemyBox.All(item => item == null);
                if(allnull){
                    Destroy(gate);
                }
            }
        }
    }
}
