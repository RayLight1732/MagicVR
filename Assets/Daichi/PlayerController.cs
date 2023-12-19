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
    private SteamVR_Action_Vector2 joystick = SteamVR_Actions._default.Move;

    private Rigidbody rigidbody;

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
        }
    }

    private void OnDisable() {
        forward.action.Disable();
        back.action.Disable();
        left.action.Disable();
        right.action.Disable();
    }

    private void FixedUpdate() {

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
            moveDirection =  inputQuaternion*forwardVec * speed;
        }
        Debug.Log(moveDirection);
        moveDirection.y = rigidbody.velocity.y;
        rigidbody.velocity = moveDirection;
        //rigidbody.velocity = moveDirection * Time.deltaTime * 1000;
    }

}
