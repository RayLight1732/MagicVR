using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHPCollback : MonoBehaviour
{
    private void Awake()
    {
        HP hp = gameObject.GetComponent<HP>();
        hp.OnChangeHandler += OnHPChange;
    }

    void OnHPChange(object target,double value)
    {
        if (value == 0) {
            Destroy(gameObject);
            Debug.Log(value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
