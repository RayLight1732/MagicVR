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
    private GameObject[] enemies;
    [SerializeField]
    private Vector3[] enepyPositions;
    [SerializeField]
    private Vector3 rotation;
    [SerializeField]
    private GameObject gate;
    private Vector3 playerpos;
    private bool spawnflag = false;
    private bool destroyFlag = false;
    private GameObject[] enemyBox;
    // Start is called before the first frame update

    private void Awake() {
        enemyBox = new GameObject[enemies.Length];
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer quadRenderer = quad.GetComponent<MeshRenderer>();
        Bounds quadBounds = quadRenderer.bounds;
        playerpos = player.transform.position;
        if(!spawnflag){
            if(quadBounds.min.x <= playerpos.x && playerpos.x <= quadBounds.max.x 
            && quadBounds.min.z <= playerpos.z && playerpos.z <= quadBounds.max.z){
                spawnflag= true;
                Quaternion rotation = Quaternion.Euler(this.rotation);
                for (int i = 0;i < enemies.Length; i++) {
                    enemyBox[i] = Instantiate(enemies[i], enepyPositions[i],rotation);
                }
                spawnflag = true;
            }
        } else {
            if (!destroyFlag && enemyBox.All(item => item == null)) {
                Destroy(gate);
            }
        }
    }
}
