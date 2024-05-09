using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private AudioSource swingSound;

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
    private bool sound = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (lastPos is Vector3 lastPos_)
        {
            Vector3 speed = lastPos_ - pos;
            float speedValue = speed.magnitude/Time.deltaTime;
            handler.SetSpeed(speedValue);
            if (speedValue > 6 && !sound) {
                sound = true;
                swingSound.Play();
            } else if (speedValue < 2 && sound) {
                sound= false;
            }
        }
        lastPos = pos;
    }
}
