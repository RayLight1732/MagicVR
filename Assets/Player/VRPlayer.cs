using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
using Valve.VR;
using Unity.XR.CoreUtils;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HP))]
[RequireComponent(typeof(MP))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMPCallback))]
[RequireComponent(typeof(ChargeManager))]
public class VRPlayerController : MonoBehaviour
{

    public float speed;
    [SerializeField]
    private float gravity;

    [SerializeField]
    private bool isSteamVR = false;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    [SerializeField]
    private GameObject steamVRPlayer;
#endif
    [SerializeField]
    private GameObject xrInteractionToolkitPlayer;

    
    [SerializeField]
    private InputActionReference moveController;
    //XR interacation toolkit
    [SerializeField]
    private InputActionReference triggerXR;
    [SerializeField]
    private InputActionReference chargeXR;

    //SteamVR
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    [SerializeField]
    private SteamVR_Action_Vector2 joystickSteamVR;
    [SerializeField]
    private SteamVR_Action_Boolean triggerSteamVR;
#endif

    [SerializeField]
    private InputActionReference triggerFallback;

    private CharacterController controller;
    private Animator animator;
    private MP mp;
    private ObjectGetter objectGetter;
    private ChargeManager chargeManager;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mp = GetComponent<MP>();
        GameObject child = null;
        if (isSteamVR)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            child = Instantiate(steamVRPlayer, transform, false);
#endif
        }
        else
        {
            child = Instantiate(xrInteractionToolkitPlayer, transform, false);
        }
        objectGetter = GetComponentInChildren<ObjectGetter>();
        GetComponent<PlayerMPCallback>().filter = objectGetter.GetFilter();
        chargeManager = GetComponent<ChargeManager>();
    }



    private void OnEnable()
    {
        if (isSteamVR)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (SteamVR.instance != null)
            {

            }
            else
            {
                moveController.action.Enable();
                moveController.action.performed += OnJoystickPerformed;
                moveController.action.canceled += OnJoystickCanceled;
                triggerXR.action.Enable();
            }
#endif
        }
        else
        {
            moveController.action.Enable();
            moveController.action.performed += OnJoystickPerformed;
            moveController.action.canceled += OnJoystickCanceled;
            triggerXR.action.Enable();

            chargeXR.action.Enable();
            chargeXR.action.performed += OnChargeButtonPerformed;
            chargeXR.action.canceled += OnChargeButtonCanceled;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    private void Update()
    {
        ProcessShoot();
        ProcessMove();
        ProcessCharge();

        Vector3 localPosition = objectGetter.GetHead().transform.localPosition;
        Vector3 worldPosition = objectGetter.GetHead().transform.position;
        worldPosition.y = transform.position.y;
        transform.position = worldPosition;
        objectGetter.GetCameraOffset().transform.localPosition = new Vector3(-localPosition.x, objectGetter.GetCameraOffset().transform.localPosition.y, -localPosition.z);

    }

    private void OnDisable()
    {
        if (isSteamVR)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (SteamVR.instance != null)
            {

            }
            else
            {
                moveController.action.Disable();
                moveController.action.performed -= OnJoystickPerformed;
                moveController.action.canceled -= OnJoystickCanceled;
                triggerFallback.action.Disable();
            }
#endif
        }
        else
        {
            moveController.action.Disable();
            moveController.action.performed -= OnJoystickPerformed;
            moveController.action.canceled -= OnJoystickCanceled;
            triggerXR.action.Disable();

            chargeXR.action.Disable();
            chargeXR.action.performed -= OnChargeButtonPerformed;
            chargeXR.action.canceled -= OnChargeButtonPerformed;
        }
    }

    private void ProcessMove() 
    {
       
        Vector3 moveDirection = Vector3.zero;
        //Quaternion forwardQuaternion = Quaternion.Euler(Vector3.Scale(transform.forward, new Vector3(1, 0, 1)));
        Vector3 forwardVec = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector2 inputVec = Vector2.zero;
        if (isSteamVR)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (SteamVR.instance != null)
            {
                inputVec = joystickSteamVR.GetLastAxis(SteamVR_Input_Sources.LeftHand);
            }
            else
            {
                inputVec = moveControllerInput;
            }
#endif
        } else
        {
            inputVec = moveControllerInput;
        }
        if (!inputVec.Equals(Vector2.zero))
        {
            Quaternion inputQuaternion = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y));
            moveDirection = inputQuaternion * forwardVec * speed;
        }


        if (!controller.isGrounded)
        {
            Vector3 vector = controller.velocity;
            vector.y -= (float) gravity*Time.deltaTime;
            controller.SimpleMove(vector);
        }
        controller.Move(moveDirection*Time.deltaTime);
        
        
    }

    private Vector2 moveControllerInput = Vector2.zero;

    private void OnJoystickPerformed(InputAction.CallbackContext context)
    {
        moveControllerInput = context.ReadValue<Vector2>();
    }

    private void OnJoystickCanceled(InputAction.CallbackContext context)
    {
        moveControllerInput = Vector2.zero;
    }


    private void ProcessShoot()
    {
        bool shoot = false;
        if (isSteamVR)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (SteamVR.instance != null)
            {
                shoot = triggerSteamVR.GetStateDown(SteamVR_Input_Sources.LeftHand);
            }
            else
            {
                shoot = triggerFallback.action.WasPressedThisFrame();
            }
#endif
        } 
        else
        {
            shoot = triggerXR.action.WasPressedThisFrame();
        }

        if (shoot && mp.GetMP() >= 1 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            mp.RemoveMP(1);
            animator.SetTrigger("Attack");
        }
    }


    public Transform GetShootTransform()
    {
        Transform t = objectGetter.GetLeftController().transform;
        t.position += t.forward*0.1f;
        return t;
    }


    private bool chargeButtonPressed = false;
    private Vector3 startPosition = Vector3.zero;

    private void OnChargeButtonPerformed(InputAction.CallbackContext context)
    {
        chargeButtonPressed = true;
        startPosition = gameObject.transform.position;
    }

    private void OnChargeButtonCanceled(InputAction.CallbackContext context)
    {
        chargeButtonPressed = false;
    }

    private void ProcessCharge()
    {
        if (chargeButtonPressed && !chargeManager.IsCharging())
        {
            chargeManager.StartCharge();
        }

        if (Vector3.Distance(startPosition,gameObject.transform.position) > 1)
        { 
            chargeManager.FailToCharge();
        }

        if (!chargeButtonPressed && chargeManager.IsCharging())
        {
            chargeManager.StopCharge();
        }

       
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(VRPlayerController))]
    public class VRPlayerControllerEditor : Editor
    {
        SerializedProperty gravity;

        SerializedProperty speed;

        SerializedProperty isSteamVR;

        //XR interacation toolkit
        SerializedProperty xrInteractionToolkitPlayer;
        SerializedProperty moveController;
        SerializedProperty triggerXR;
        SerializedProperty chargeXR;
        //SteamVR
        SerializedProperty steamVRPlayer;
        SerializedProperty joystickSteamVR;
        SerializedProperty triggerSteam;
        //SteamVR fallback
        SerializedProperty fallbackTrigger;


        private void OnEnable()
        {
            gravity = serializedObject.FindProperty(nameof(VRPlayerController.gravity));

            speed = serializedObject.FindProperty(nameof(VRPlayerController.speed));

            isSteamVR = serializedObject.FindProperty(nameof(VRPlayerController.isSteamVR));

            xrInteractionToolkitPlayer = serializedObject.FindProperty(nameof(VRPlayerController.xrInteractionToolkitPlayer));
            moveController = serializedObject.FindProperty(nameof(VRPlayerController.moveController));
            triggerXR = serializedObject.FindProperty(nameof(VRPlayerController.triggerXR));
            chargeXR = serializedObject.FindProperty(nameof(VRPlayerController.chargeXR));

            steamVRPlayer = serializedObject.FindProperty(nameof(VRPlayerController.steamVRPlayer));
            joystickSteamVR = serializedObject.FindProperty(nameof(VRPlayerController.joystickSteamVR));
            triggerSteam = serializedObject.FindProperty(nameof (VRPlayerController.triggerSteamVR));
            fallbackTrigger = serializedObject.FindProperty (nameof(VRPlayerController.triggerFallback));
        }

        public override void OnInspectorGUI()
        {
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);

            serializedObject.Update();
            EditorGUILayout.PropertyField(gravity);

            EditorGUILayout.PropertyField(speed);

            EditorGUILayout.PropertyField(isSteamVR);
            if (isSteamVR.boolValue)
            {
                if (steamVRPlayer.isExpanded = EditorGUILayout.Foldout(steamVRPlayer.isExpanded,"SteamVR"))
                {
                    EditorGUILayout.PropertyField(steamVRPlayer);
                    EditorGUILayout.PropertyField(joystickSteamVR);
                    EditorGUILayout.PropertyField(triggerSteam);
                    

                    EditorGUI.indentLevel++;
                    if (fallbackTrigger.isExpanded = EditorGUILayout.Foldout(fallbackTrigger.isExpanded, "Fallback"))
                    {
                        EditorGUILayout.PropertyField(fallbackTrigger);
                        EditorGUILayout.PropertyField(moveController);
                    }
                }
            }
            else
            {
                if (xrInteractionToolkitPlayer.isExpanded = EditorGUILayout.Foldout(xrInteractionToolkitPlayer.isExpanded, "XR Interaction Toolkit"))
                {
                    EditorGUILayout.PropertyField(xrInteractionToolkitPlayer);
                    EditorGUILayout.PropertyField(moveController);
                    EditorGUILayout.PropertyField(triggerXR);
                    EditorGUILayout.PropertyField(chargeXR);
                }

            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
