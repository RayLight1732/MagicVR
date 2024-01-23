using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterManager : ShooterManager
{

    private ObjectGetter objectGetter;

    public override Transform GetShootTransform()
    {
        Transform t = objectGetter.GetLeftController().transform;
        t.position += t.forward * 0.1f;
        return t;
    }


    protected void Awake()
    {
        objectGetter = GetComponentInChildren<ObjectGetter>();
        ignores.Add(gameObject);
        ignores.Add(objectGetter.GetSword());
        ignores.Add(Camera.main.gameObject);
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
