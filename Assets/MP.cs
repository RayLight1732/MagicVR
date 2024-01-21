using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP : MonoBehaviour
{

    public double maxMP;

    private double _mp;
    private double mp
    {
        set {
            _mp = value;
            if (OnChangeHandler != null)
            {
                OnChangeHandler(this, value);
            }
        }
        get => _mp;
    }

    public event EventHandler<double> OnChangeHandler;

    private void OnEnable()
    {
        _mp = maxMP;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMP(double mp)
    {
        if (mp <= maxMP)
        {
            this.mp = mp;
        }
    }

    public double GetMP()
    {
        return mp;
    }

    public void AddMP(double value)
    {
        mp = Math.Min(mp + value, maxMP);
    }

    public void RemoveMP(double value)
    {
        mp = Math.Max(mp - value, 0);
    }

    public double GetMaxMP() { return maxMP; }
    
    public void SetMaxMP(double maxMP)
    {
        this.maxMP = maxMP;
        if (mp > maxMP) { mp = maxMP; }
    }
}
