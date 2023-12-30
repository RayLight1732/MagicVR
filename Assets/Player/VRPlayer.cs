using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
using Valve.VR;
#endif
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(HP))]
[RequireComponent(typeof(MP))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMPCallback))]
public class VRPlayerController : MonoBehaviour
{

    public float speed;

    [SerializeField]
    private bool isSteamVR = false;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    [SerializeField]
    private GameObject steamVRPlayer;
#endif
    [SerializeField]
    private GameObject xrInteractionToolkitPlayer;

    
    //XR interacation toolkit
    [SerializeField]
    private InputActionReference joystickXR;
    [SerializeField]
    private InputActionReference triggerXR;
    //SteamVR
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    [SerializeField]
    private SteamVR_Action_Vector2 joystickSteamVR;
    [SerializeField]
    private SteamVR_Action_Boolean triggerSteamVR;
#endif
    //SteamVR fallback
    [SerializeField]
    private InputActionReference forward;
    [SerializeField]
    private InputActionReference back;
    [SerializeField]
    private InputActionReference left;
    [SerializeField]
    private InputActionReference right;
    [SerializeField]
    private InputActionReference triggerFallback;

    private Rigidbody componentRigidbody;
    private Animator animator;
    private MP mp;

    private void Awake()
    {
        componentRigidbody = GetComponent<Rigidbody>();
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
        GetComponent<PlayerMPCallback>().filter = child.GetComponent<FilterGetter>().GetFilter();
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
                forward.action.Enable();
                back.action.Enable();
                left.action.Enable();
                right.action.Enable();
                triggerFallback.action.Enable();
            }
#endif
        }
        else
        {
            joystickXR.action.Enable();
            joystickXR.action.performed += OnJoystickPerformed;
            joystickXR.action.canceled += OnJoystickCanceled;
            triggerXR.action.Enable();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        ProcessMove();
    }

    private void Update()
    {
        ProcessShoot();
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
                forward.action.Disable();
                back.action.Disable();
                left.action.Disable();
                right.action.Disable();
                triggerFallback.action.Disable();
            }
#endif
        }
        else
        {
            joystickXR.action.Disable();
            joystickXR.action.performed -= OnJoystickPerformed;
            joystickXR.action.canceled -= OnJoystickCanceled;
            triggerXR.action.Disable();
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
                if (forward.action.IsPressed())
                {
                    inputVec.y = 1;
                }
                if (back.action.IsPressed())
                {
                    inputVec.y = -1;
                }
                if (right.action.IsPressed())
                {
                    inputVec.x = 1;
                }
                if (left.action.IsPressed())
                {
                    inputVec.x = -1;
                }
            }
#endif
        } else
        {
            inputVec = joystickXRInput;
        }
        if (!inputVec.Equals(Vector2.zero))
        {
            Quaternion inputQuaternion = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y));
            moveDirection = inputQuaternion * forwardVec * speed;
        }
        moveDirection.y = componentRigidbody.velocity.y;
        componentRigidbody.velocity = moveDirection;
        
    }

    private Vector2 joystickXRInput = Vector2.zero;

    private void OnJoystickPerformed(InputAction.CallbackContext context)
    {
        joystickXRInput = context.ReadValue<Vector2>();
        Debug.Log(joystickXRInput);
    }

    private void OnJoystickCanceled(InputAction.CallbackContext context)
    {
        joystickXRInput = Vector2.zero;
        Debug.Log(joystickXRInput);
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

        if (shoot && mp.GetMP() >= 1)
        {
            mp.RemoveMP(1);
            animator.SetTrigger("Attack");
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(VRPlayerController))]
    public class VRPlayerControllerEditor : Editor
    {

        SerializedProperty speed;

        SerializedProperty isSteamVR;

        //XR interacation toolkit
        SerializedProperty xrInteractionToolkitPlayer;
        SerializedProperty joystickXR;
        SerializedProperty triggerXR;
        //SteamVR
        SerializedProperty steamVRPlayer;
        SerializedProperty joystickSteamVR;
        SerializedProperty triggerSteam;
        //SteamVR fallback
        SerializedProperty fallbackTrigger;
        SerializedProperty forward;
        SerializedProperty back;
        SerializedProperty left;
        SerializedProperty right;


        private void OnEnable()
        {
            speed = serializedObject.FindProperty(nameof(VRPlayerController.speed));

            isSteamVR = serializedObject.FindProperty(nameof(VRPlayerController.isSteamVR));
    
            xrInteractionToolkitPlayer = serializedObject.FindProperty(nameof(VRPlayerController.xrInteractionToolkitPlayer));
            joystickXR = serializedObject.FindProperty(nameof(VRPlayerController.joystickXR));
            triggerXR = serializedObject.FindProperty(nameof(VRPlayerController.triggerXR));

            steamVRPlayer = serializedObject.FindProperty(nameof(VRPlayerController.steamVRPlayer));
            joystickSteamVR = serializedObject.FindProperty(nameof(VRPlayerController.joystickSteamVR));
            triggerSteam = serializedObject.FindProperty(nameof (VRPlayerController.triggerSteamVR));
            forward = serializedObject.FindProperty(nameof(VRPlayerController.forward));
            back = serializedObject.FindProperty(nameof(VRPlayerController.back));
            left = serializedObject.FindProperty(nameof(VRPlayerController.left));
            right = serializedObject.FindProperty(nameof(VRPlayerController.right));
            fallbackTrigger = serializedObject.FindProperty (nameof(VRPlayerController.triggerFallback));
        }

        public override void OnInspectorGUI()
        {
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);

            serializedObject.Update();

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
                        EditorGUILayout.PropertyField(forward);
                        EditorGUILayout.PropertyField(back);
                        EditorGUILayout.PropertyField(left);
                        EditorGUILayout.PropertyField(right);
                    }
                }
            }
            else
            {
                if (xrInteractionToolkitPlayer.isExpanded = EditorGUILayout.Foldout(xrInteractionToolkitPlayer.isExpanded, "XR Interaction Toolkit"))
                {
                    EditorGUILayout.PropertyField(xrInteractionToolkitPlayer);
                    EditorGUILayout.PropertyField(joystickXR);
                    EditorGUILayout.PropertyField(triggerXR);
                }

            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
