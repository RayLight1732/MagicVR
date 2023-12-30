using UnityEngine;
using Valve.VR.InteractionSystem;

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

    private bool fire = false;
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
            rb.velocity = transform.forward * 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on trigger");
        if (other.gameObject != holder)
        {

            HP hp = other.gameObject.GetComponent<HP>();
            if (hp)
            {
                hp.removeHP(5);
            }
            Instantiate(effect,transform);
            projectileManager.DestroyItself();
        }
    }

    public void Fire()
    {
        fire = true;
        projectileManager.DestroyItself(5);
    }
}
