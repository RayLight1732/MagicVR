using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_wizard : MonoBehaviour
{

    [SerializeField]
    private GameObject darkball;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle_combat") && animator.GetCurrentAnimatorStateInfo(2).IsName("idle_warp"))
        {
            //‘Ò‹@’†
            Debug.Log("wait");
        }
    }

    public void ShootDarkBall()
    {

    }

    private void OnConnectedToServer()
    {
        
    }

    public void ProcessWarp()
    {
        Debug.Log("process");
    }

}
