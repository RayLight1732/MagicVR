using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShooterManager : MonoBehaviour
{

    public List<GameObject> ignores { get; private set; } = new List<GameObject>();


    public abstract Transform GetShootTransform();

}
