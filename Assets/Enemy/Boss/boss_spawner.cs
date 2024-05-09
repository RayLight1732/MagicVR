using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class boss_spawner : MonoBehaviour {
    [SerializeField]
    private GameObject quad;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private GameObject backGate;
    private Vector3 playerpos;
    private bool spawnflag = false;
    private bool destroyFlag = false;
    // Start is called before the first frame update

    private void Awake() {
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        MeshRenderer quadRenderer = quad.GetComponent<MeshRenderer>();
        Bounds quadBounds = quadRenderer.bounds;
        playerpos = player.transform.position;
        if (!spawnflag) {
            if (quadBounds.min.x <= playerpos.x && playerpos.x <= quadBounds.max.x
            && quadBounds.min.z <= playerpos.z && playerpos.z <= quadBounds.max.z) {
                spawnflag = true;
                boss.SetActive(true);
                backGate.SetActive(true);
                spawnflag = true;
            }
        } else {
            if (!destroyFlag && boss== null) {
                Destroy(gate);
            }
        }
    }
}
