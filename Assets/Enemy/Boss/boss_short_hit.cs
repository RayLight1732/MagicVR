using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_short_hit : MonoBehaviour
{
    [SerializeField]
    private AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<GameObject> objects = new List<GameObject>();

    private void OnTriggerEnter(Collider other) {
        if (!objects.Contains(other.gameObject)) {
            objects.Add(other.gameObject);
            HP hp = other.GetComponent<HP>();
            if (hp != null) {
                hitSound.Play();
                hp.removeHP(5);
            }
        }
    }

    private void OnDisable() {
        objects.Clear();
    }
}
