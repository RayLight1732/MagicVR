using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private Rigidbody rigidbody;

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        forward.action.Enable();
        back.action.Enable();
        left.action.Enable();
        right.action.Enable();
    }

    private void OnDisable() {
        forward.action.Disable();
        back.action.Disable();
        left.action.Disable();
        right.action.Disable();
    }

    private void FixedUpdate() {
        
        Vector3 moveDirection = Vector3.zero;
        Vector3 forwardVec = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized*speed;
        Vector3 rightVec = Vector3.Scale(transform.right, new Vector3(1, 0, 1)).normalized*speed;

        if (forward.action.IsPressed()) {
            moveDirection += forwardVec;
        }
        if (back.action.IsPressed()) {
            moveDirection -= forwardVec;
        }
        if (right.action.IsPressed()) {
            moveDirection += rightVec;
        }
        if (left.action.IsPressed()) {
            moveDirection -= rightVec;
        }
        /*
        if (!controller.isGrounded) {
            moveDirection.y += Physics.gravity.y;
        }*/
        moveDirection.y = rigidbody.velocity.y;
        rigidbody.velocity = moveDirection;
        //rigidbody.velocity = moveDirection * Time.deltaTime * 1000;
    }

}
