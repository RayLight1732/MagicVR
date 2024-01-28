using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPCallback : MonoBehaviour
{
    private bool dead = false;

    private void Awake()
    {
        HP mp = gameObject.GetComponent<HP>();
        mp.OnChangeHandler += OnHPChange;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnHPChange(object target, double value)
    {
        if (!dead && value <= 0)
        {
            dead = true;
            GetComponent<Animator>().SetTrigger("dead");
        }
    }
}
