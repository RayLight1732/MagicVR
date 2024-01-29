using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class boss_wizard : MonoBehaviour {

    [SerializeField]
    private GameObject darkball;
    [SerializeField]
    private GameObject darkball_anchor;
    [SerializeField]
    private AudioSource warpSound;
    [SerializeField]
    private AudioSource windSound;
   

    public float moveSpeed = 1;
    public float chargeSpeed = 3;

    private Animator animator;
    private GameObject target;

    public GameObject fieldQuad;

    private void Awake() {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        warpPosition = transform.position;
        warpRotation = transform.rotation;
    }


    // Start is called before the first frame update
    void Start() {

    }

    private double delayTimer = 0;
    private int motion = -1;//0->shoot fireball 1 -> warp 2->melee

    private Vector3 warpPosition;
    private Quaternion warpRotation;

    private const float rotationLimitOriginal = 60;
    private float rotationLimit = rotationLimitOriginal;

    private bool changeState = false;

    // Update is called once per frame
    void Update() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle_combat") && animator.GetCurrentAnimatorStateInfo(2).IsName("idle_warp")) {
            if (!changeState)//when state was not changed
            {
                rotationLimit = rotationLimitOriginal;
                if (delayTimer > 0)
                {
                    delayTimer -= Time.deltaTime;
                } else
                {
                    motion = Random.Range(0, 3);
                    onEnter(motion);
                }
            }
        } 
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            
        }
        else {
            changeState = false;
            motionTick(motion);
        }
        
        var targetPos = target.transform.position;
        targetPos.y = 0;
        var rotation = Quaternion.LookRotation(targetPos - transform.position);
        if (rotation != null) {
            rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationLimit * Time.deltaTime);
            transform.rotation = rotation;
        }
    }

    private void onEnter(int motionId)
    {
        if (motionId == 0)
        {
            animator.SetTrigger("attack_magic_001");
            delayTimer = 3;
            changeState = true;
        }
        else if (motionId == 1)
        {
            animator.SetTrigger("warp");
            GameObject target = GameObject.FindWithTag("Player");
            warpPosition = target.transform.position;
            warpRotation = target.transform.rotation;

            MeshRenderer quadRenderer = fieldQuad.GetComponent<MeshRenderer>();
            Bounds quadBounds = quadRenderer.bounds;


            warpPosition = new Vector3(
                Random.Range(quadBounds.min.x, quadBounds.max.x),
                target.transform.position.y,
                Random.Range(quadBounds.min.z, quadBounds.max.z)
            );

            var targetPos = target.transform.position;
            targetPos.y = 0;
            warpRotation = Quaternion.LookRotation(targetPos - warpPosition);
            delayTimer = 0.3;
            changeState = true;
        }
        else if (motionId == 2)
        {
            animator.SetBool("charge", true);
            changeState = true;
            rotationLimit = rotationLimitOriginal * chargeSpeed / moveSpeed;
        }
    }

    private void motionTick(int motionId)
    {
        if (motionId == 2)
        {
            var targetPos = target.transform.position;
            targetPos.y = 0;
            if (Vector3.Distance(transform.position, targetPos) < 1)
            {
                animator.SetBool("charge", false);
                this.motion = -1;
            }
            else
            {
                transform.position += transform.forward * (float)(Time.deltaTime * chargeSpeed);
            }
        }
    }


    private void ThreeFireBall() {
    }

    public void ShootDarkBall() {

        Transform shootTransform = darkball_anchor.transform;
        GameObject bullet = Instantiate(darkball, shootTransform.position, shootTransform.rotation, darkball_anchor.transform);


        ProjectileManager projectileManager = bullet.GetComponent<ProjectileManager>();
        projectileManager.ignores.Add(gameObject);
        projectileManager.forwardGetter = () => {
            return transform.forward;
        };

        bullet.transform.forward = shootTransform.forward;
    }

    private bool doWarp = false;

    public void ProcessWarp() {
        doWarp = true;
    }

    private void LateUpdate() {
        
        //transform.position = warpPosition;
        if (doWarp) {
            transform.SetPositionAndRotation(warpPosition, warpRotation);
            doWarp = false;
        }
    }

    private void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

    private void PlayWarpSound()
    {
        warpSound.Play();
    }

    private void PlayWindSound()
    {
        windSound.Play();
    }
}
