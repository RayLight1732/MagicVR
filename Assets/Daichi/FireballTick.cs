using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class FireballTick : MonoBehaviour
{

    protected Vector3 forward;
    protected Rigidbody rb;
    private ProjectileManager projectileManager;
    [SerializeField]
    private GameObject parent;
    public GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        projectileManager = parent.GetComponent<ProjectileManager>();
        Destroy(this.gameObject,5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = forward * 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != projectileManager.holder) {
            
            HP hp = other.gameObject.GetComponent<HP>();
            if(hp) {
                hp.removeHP(5);
            }
            var effect =Instantiate(Effect);
            effect.transform.position = transform.position;
            projectileManager.DestroyItself();
        }
    }

    public void setForward(Vector3 forward) {
        this.forward = forward;
    }
}
