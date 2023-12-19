using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private InputActionReference forward;
    [SerializeField]
    private InputActionReference back;
    [SerializeField]
    private InputActionReference left;
    [SerializeField] 
    private InputActionReference right;
    [SerializeField] 
    private InputActionReference shootButon;
    [SerializeField]
    private SteamVR_Action_Vector2 joystick;
    [SerializeField]
    private SteamVR_Action_Boolean trigger;

    private Rigidbody rigidbody;
    private Animator animator;
    private MP mp;

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        if (SteamVR.instance != null) {

        } else {
            forward.action.Enable();
            back.action.Enable();
            left.action.Enable();
            right.action.Enable();
            shootButon.action.Enable();
        }

        animator = GetComponent<Animator>();
        mp = GetComponent<MP>();

    }

    private void OnDisable() {
        forward.action.Disable();
        back.action.Disable();
        left.action.Disable();
        right.action.Disable();
        shootButon.action.Disable();
    }

    private void FixedUpdate() {
        ProcessMove();
    }

    private void Update() {
        ProcessShoot();
    }

    private void ProcessMove() {
        Vector3 moveDirection = Vector3.zero;
        //Quaternion forwardQuaternion = Quaternion.Euler(Vector3.Scale(transform.forward, new Vector3(1, 0, 1)));
        Vector3 forwardVec = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector2 inputVec;
        if (SteamVR.instance != null) {
            inputVec = joystick.GetLastAxis(SteamVR_Input_Sources.LeftHand);
        } else {
            inputVec = Vector2.zero;
            if (forward.action.IsPressed()) {
                inputVec.y = 1;
            }
            if (back.action.IsPressed()) {
                inputVec.y = -1;
            }
            if (right.action.IsPressed()) {
                inputVec.x = 1;
            }
            if (left.action.IsPressed()) {
                inputVec.x = -1;
            }

            /*
            if (!controller.isGrounded) {
                moveDirection.y += Physics.gravity.y;
            }*/
        }
        if (!inputVec.Equals(Vector2.zero)) {
            Quaternion inputQuaternion = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y));
            moveDirection = inputQuaternion * forwardVec * speed;
        }
        moveDirection.y = rigidbody.velocity.y;
        rigidbody.velocity = moveDirection;
    }

    private void ProcessShoot() {
        bool shoot;
        if (SteamVR.instance != null) {
            shoot = trigger.GetStateDown(SteamVR_Input_Sources.LeftHand);
        } else {
            shoot = shootButon.action.WasPressedThisFrame();
        }


        if (shoot && mp.GetMP() >= 1) {
            mp.RemoveMP(1);
            animator.SetTrigger("Attack");
        }

    }

}
