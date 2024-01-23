using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> ignores;
    protected Rigidbody rb;
    private ProjectileManager projectileManager;
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private GameObject parent;

    public int speed;

    private bool fire = false;
    private bool collided = false;

    private void Awake()
    {
        transform.forward = transform.parent.forward;
        projectileManager = parent.GetComponent<ProjectileManager>();
        ignores = projectileManager.ignores;
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
            if (collided)
            {
                rb.velocity = Vector3.zero;
                checkCollision = false;
            }
            else
            {
                rb.velocity = transform.forward * speed;
            }
        }
    }

    private bool checkCollision = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!ignores.Contains(other.gameObject) &&checkCollision && other.gameObject.GetComponent<IgnoreCollider>() == null)
        {
            collided = true;
            HP hp = other.gameObject.GetComponent<HP>();
            if (hp)
            {
                hp.removeHP(5);
            }
            Instantiate(effect,transform.position,transform.rotation);
            GetComponent<ParticleSystem>().Stop(true,ParticleSystemStopBehavior.StopEmitting);
            projectileManager.DestroyItself(1);
        }
    }

    public void Fire()
    {
        fire = true;
        projectileManager.DestroyItself(5);
    }
}
