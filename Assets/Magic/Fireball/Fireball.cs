using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject holder;
    protected Rigidbody rb;
    private ProjectileManager projectileManager;
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private GameObject parent;

    public int speed;

    private bool fire = false;
    private bool collided = false;

    void Start()
    {
        transform.forward = transform.parent.forward;
        projectileManager = parent.GetComponent<ProjectileManager>();
        holder = projectileManager.holder;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (fire)
        {
            if (collided)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.velocity = transform.forward * speed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != holder && !collided)
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
