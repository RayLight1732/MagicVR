using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class RotationInputManager : MonoBehaviour
{

    [SerializeField]
    private InputActionProperty action;

    private Quaternion targetRotation;

    private bool rotationBound = false;
    public float delay = 0;
    [SerializeField, Tooltip("value that is 0 or under means script will not limit rotation")]
    public float rotationLimit = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            StartCoroutine("SetLocalTransformAfter", targetRotation);
        }
        else
        {
            SetLocalTransForm(targetRotation);
        }
    }

    private void OnEnable()
    {
        BindRotation();
    }

    private void OnDisable()
    {
        UnbindRotation();
    }

    void BindRotation()
    {
        if (rotationBound)
            return;
        var action = this.action.action;
        action.performed += OnRotationPerformed;
        action.canceled += OnRotationCanceled;
        rotationBound = true;

    }

    void UnbindRotation()
    {
        if (!rotationBound)
            return;

        //var action = input.action;

        var action = this.action.action;
        action.performed -= OnRotationPerformed;
        action.canceled -= OnRotationCanceled;
        rotationBound = false;
    }

    void OnRotationPerformed(InputAction.CallbackContext context)
    {
        targetRotation = context.ReadValue<Quaternion>();
    }

    void OnRotationCanceled(InputAction.CallbackContext context)
    {
        targetRotation = Quaternion.identity;
    }

    protected virtual IEnumerator SetLocalTransformAfter(Quaternion targetRotation)
    {
        yield return new WaitForSeconds(delay);
        SetLocalTransForm(targetRotation);
    }

    protected virtual void SetLocalTransForm(Quaternion targetRotation)
    {
        if (rotationLimit <= 0)
        {
            transform.localRotation = targetRotation;
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationLimit * Time.deltaTime);
        }

    }


}
