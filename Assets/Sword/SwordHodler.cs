using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;

    private SwordDamageHandler handler;

    private void Awake()
    {
        handler = sword.GetComponent<SwordDamageHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3? lastPos = null;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (lastPos is Vector3 lastPos_)
        {
            Vector3 speed = lastPos_ - pos;
            handler.SetSpeed(speed.magnitude);
            
        }
        lastPos = pos;
    }
}
