using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamageHandler : MonoBehaviour
{

    private float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        HP hp = other.GetComponent<HP>();
        if (hp != null)
        {
            Debug.Log(speed);
            hp.removeHP((int)(speed*30));
        }
    }
}
